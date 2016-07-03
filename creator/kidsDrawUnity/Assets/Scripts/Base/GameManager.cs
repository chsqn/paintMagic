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

		ES2.Save(123, saveFileName);
	}


	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI

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
	public GameObject	levelObj;   //the object that should be unlocked in the level
	public bool			isLocked = true;
	public Sprite		unlockedTexture;
	public Sprite 		lockedTexture;



	public void saveData(int index)
	{
		using(ES2Writer writer = ES2Writer.Create(GameManager.Instance.saveFileName))
		{
			// Write our data to the file.
			writer.Write(isLocked, "isLocked_" + index.ToString());
		}

	}

	public void loadData(int index)
	{
		using(ES2Reader reader = ES2Reader.Create(GameManager.Instance.saveFileName))
		{
			if(ES2.Exists(GameManager.Instance.saveFileName))
			{
				// Read data from the file.
				if(ES2.Exists("isLocked_" + index.ToString()))
				{
					isLocked			= reader.Read<bool>("isLocked_" + index.ToString());
				}
			}
		}

	}
}
