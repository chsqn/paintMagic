// =====================================================================
// Copyright 2013-2015 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using FluffyUnderware.DevTools.Extensions;
using System.Reflection;


namespace FluffyUnderware.DevToolsEditor
{
    [InitializeOnLoad]
    public static class DT
    {
        public const string VERSION = "1.0.0";
        /// <summary>
        /// Global override for float precision rounding (e.g. DTPropertyAttribute.Precision)
        /// </summary>
        public static bool _UseSnapValuePrecision;


        public delegate void Callback();

        public static List<DTProject> Projects
        {
            get
            {
                return mProjects.Values.ToList<DTProject>();
            }
        }

        public static DTProject Project(string identifier)
        {
            DTProject prj = null;
            if (!mProjects.TryGetValue(identifier, out prj))
            {
                LoadProjects();
                if (!mProjects.TryGetValue(identifier, out prj))
                    Debug.LogError("[DevTools] Unable to find project '" + identifier + "' !");
            }

            return prj;
        }

        static object mClipboardData;
        static Dictionary<Type, IDTClipboardHandler> mClipboardHandlers = new Dictionary<Type, IDTClipboardHandler>();
        static Dictionary<string, DTProject> mProjects = new Dictionary<string, DTProject>();

        static bool _compiling;

        static DT()
        {
            EditorApplication.update += delayedInitialize;
            EditorApplication.update -= compileCheck;
            EditorApplication.update += compileCheck;
            
        }

        public static void Clear()
        {
            foreach (var prj in DT.Projects)
                prj.Clear();
            DT.Projects.Clear();
        }

        public static void ReInitialize(bool loadProjects=true)
        {
            if (loadProjects)
                LoadProjects();

            DTToolbar.Initialize();
            HandleProjectsKeyBindings();
        }

        static void delayedInitialize()
        {
            EditorApplication.update -= delayedInitialize;
            LoadPreferences();
            DTSelection.Initialize();
            ReInitialize();
            // Register Clipboard-Handlers
            RegisterClipboardType(typeof(Vector2), new DTVector2Clipboard());
            RegisterClipboardType(typeof(Vector3), new DTVector3Clipboard());
            RegisterClipboardType(typeof(AnimationCurve), new DTAnimationCurveClipboard());
        }
        
        static void compileCheck()
        {
            if (!_compiling)
            {
                _compiling = EditorApplication.isCompiling;
                if (_compiling)
                    Clear();
            }
            else
                _compiling = EditorApplication.isCompiling;
        }

        #region ### Clipboard ###
        public static void RegisterClipboardType(Type dataType, IDTClipboardHandler handler, bool overwriteExisting = false)
        {
            if (mClipboardHandlers.ContainsKey(dataType))
            {
                if (overwriteExisting)
                    mClipboardHandlers[dataType] = handler;
            }
            else
                mClipboardHandlers.Add(dataType, handler);
        }

        public static void ClipboardCopy(object data)
        {
            var dataType = data.GetType();
            IDTClipboardHandler handler;
            if (mClipboardHandlers.TryGetValue(dataType, out handler))
            {
                handler.Copy(data);
            }
            else
                Debug.LogError("DevTools: No ClipboardHandler for data type '" + data.GetType().Name + "' found!");
        }

        public static T ClipboardPaste<T>()
        {
            IDTClipboardHandler handler;
            if (mClipboardData != null && mClipboardHandlers.TryGetValue(typeof(T), out handler))
            {
                if (handler.CanPasteFrom(mClipboardData.GetType()))
                {
                    return (T)handler.Paste(mClipboardData);
                }
            }
            return default(T);
        }

        public static bool ClipboardCanPasteTo<T>()
        {
            IDTClipboardHandler handler;
            if (mClipboardData != null && mClipboardHandlers.TryGetValue(typeof(T), out handler))
            {
                return handler.CanPasteFrom(mClipboardData.GetType());
            }
            return false;
        }

        internal static void ClipBoardSet(object data)
        {
            mClipboardData = data;
        }

        #endregion

        #region ### Project Management ###

        static void LoadProjects()
        {
            mProjects.Clear();
            var types = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                         from t in asm.GetTypes()
                         where t.IsSubclassOf(typeof(DTProject))
                         select t).ToArray();
            foreach (var t in types)
            {
                DTProject newProject = (DTProject)Activator.CreateInstance(t);
                mProjects.Add(newProject.Identifier, newProject);
            }
            
        }

        static void HandleProjectsKeyBindings()
        {
            foreach (var prj in Projects)
                if (!prj.CheckKeyBindingNamesAreUnique())
                    return;
                else
                    prj.LoadKeyBindingRemappings();
        }

        #endregion

        #region ### EditorPrefs-Helpers ###

        static void LoadPreferences()
        {
            // Upgrade?
            string ver = GetEditorPrefs<string>("FluffyUnderware.DevTools.Version", VERSION);
            if (ver != VERSION)
            {
                UpgradeDevTools(ver);
                SavePreferences();
            }
        }

        static void SavePreferences()
        {
            SetEditorPrefs<string>("FluffyUnderware.DevTools.Version", VERSION);
        }

        /// <summary>
        /// Add code to handle upgrading (delete old pref-keys etc...) here
        /// </summary>
        /// <param name="oldVersion">the version stored in the EditorPrefs</param>
        static void UpgradeDevTools(string oldVersion)
        {
            Debug.Log("[DevTools] Upgrading settings from " + oldVersion + " to " + VERSION);
        }

        public static void SetEditorPrefs<T>(string key, T value)
        {
            var tt = typeof(T);
            if (tt.IsEnum)
            {
                EditorPrefs.SetInt(key, Convert.ToInt32(Enum.Parse(typeof(T), value.ToString()) as Enum));
            }
            else if (tt.IsArray)
            {
                throw new System.NotImplementedException();
            }
            else if (tt.Matches(typeof(int), typeof(Int32)))
                EditorPrefs.SetInt(key, (value as int?).Value);
            else if (tt == typeof(string))
                EditorPrefs.SetString(key, (value as string));
            else if (tt == typeof(float))
                EditorPrefs.SetFloat(key, (value as float?).Value);
            else if (tt == typeof(bool))
                EditorPrefs.SetBool(key, (value as bool?).Value);
            else if (tt == typeof(Color))
                EditorPrefs.SetString(key, (value as Color?).Value.ToHtml());
            else
                Debug.LogError("[DevTools.SetEditorPrefs] Unsupported datatype: " + tt.Name);
        }

        public static T GetEditorPrefs<T>(string key, T defaultValue)
        {
            if (EditorPrefs.HasKey(key))
            {
                var tt = typeof(T);
                try
                {
                    if (tt.IsEnum || tt.Matches(typeof(int), typeof(Int32)))
                    {
                        return (T)(object)EditorPrefs.GetInt(key, (int)(object)defaultValue);
                    }
                    else if (tt.IsArray)
                    {
                        throw new System.NotImplementedException();
                    }
                    else if (tt == typeof(string))
                        return (T)(object)EditorPrefs.GetString(key, defaultValue.ToString());
                    else if (tt == typeof(float))
                        return (T)(object)EditorPrefs.GetFloat(key, (float)(object)defaultValue);
                    else if (tt == typeof(bool))
                        return (T)(object)EditorPrefs.GetBool(key, (bool)(object)defaultValue);
                    else if (tt == typeof(Color))
                        return (T)(object)EditorPrefs.GetString(key, ((Color)(object)defaultValue).ToHtml()).ColorFromHtml();
                    else
                        Debug.LogError("[DevTools.SetEditorPrefs] Unsupported datatype: " + tt.Name);
                }
                catch 
                {
                    return defaultValue;
                }
            }
            

            return defaultValue;
        }

        #endregion

        #region ### Utilities ###

        public static void OpenPreferencesWindow()
        {
            var asm = Assembly.GetAssembly(typeof(EditorWindow));
            var T = asm.GetType("UnityEditor.PreferencesWindow");
            var M = T.GetMethod("ShowPreferencesWindow", BindingFlags.NonPublic | BindingFlags.Static);
            M.Invoke(null, null);
        }

        #endregion
    }


}