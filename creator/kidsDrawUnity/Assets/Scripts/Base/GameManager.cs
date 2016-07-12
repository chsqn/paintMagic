using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;

public class GameManager : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	// Static singleton property
	public static GameManager Instance { get; private set; }	

	public LevelSettings[] 	allLevelSettings;

	public int				currentLevelIndex;
	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	[HideInInspector] public string saveFileName = "";

	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		////////////////////////////////////////////////////////////////////
		/// SINGLETON //////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		} else
		{
			Destroy(this.gameObject);
		}
		////////////////////////////////////////////////////////////////////
		/// SINGLETON //////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////

		saveFileName = Application.persistentDataPath + "/SavesDir/saveData.sav";

		//load the data on awake IF the file exists
		if(ES2.Exists(saveFileName))
		{
			print ("------- LOADING DATA FROM FILE --------");
			loadData();
		} 

	}

	void OnApplicationQuit()
	{
		saveData();
	}
	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	public GameObject getCollectibleObj()
	{
		return allLevelSettings[currentLevelIndex].collectibleObj;
	}

	/// <summary>
	/// Called when the user pressed the homebtn
	/// </summary>
	public void loadMainUI()
	{
		print("now loading mainUI");
		Application.LoadLevel("mainUI");
	}

	/// <summary>
	/// Saves the data.
	/// </summary>
	public void saveData()
	{
		using(ES2Writer writer = ES2Writer.Create(saveFileName))
		{
			for(int i = 0; i < allLevelSettings.Length; i++)
			{
				print("now saving level " + i);
				allLevelSettings[i].saveData(i, writer);
			}

			// Remember to save when we're done.
			writer.Save();
		}
	}

	/// <summary>
	/// Loads the data.
	/// </summary>
	public void loadData()
	{
		using(ES2Reader reader = ES2Reader.Create(GameManager.Instance.saveFileName))
		{
			for(int i = 0; i < allLevelSettings.Length; i++)
			{
				allLevelSettings[i].loadData(i, reader);
			}
		}
	}

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}

[Serializable]
public class LevelSettings
{
	public GameObject	collectibleObj;   //the object that should be unlocked in the level
	public bool			isLocked = true;
	public Sprite		unlockedTexture;
	public Sprite 		lockedTexture;



	public void saveData(int index, ES2Writer writer)
	{
		// Write our data to the file.
		writer.Write(isLocked, "isLocked_" + index.ToString());
	}

	public void loadData(int index, ES2Reader reader)
	{
		// Read data from the file.
		isLocked			= reader.Read<bool>("isLocked_" + index.ToString());
	}

	/// <summary>
	/// Unlocks the level.
	/// </summary>
	public void unlockLevel()
	{
		isLocked = false;
	}
}
