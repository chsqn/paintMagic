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
	public int				lockedLevelFrom = 12;

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
	/// <summary>
	/// This will delete all the IN APP PURCHASES. Mainly used for debugging
	/// </summary>
	public void deleteIAPs()
	{
		Directory.Delete(Path.Combine (Path.Combine (Application.persistentDataPath, "Unity"), "UnityPurchasing"), true);

		//step through all the levels and set them to locked
		for(int i = lockedLevelFrom; i < allLevelSettings.Length; i++)
		{
			allLevelSettings[i].isLocked = true;
			allLevelSettings[i].hasCollectibleAchieved = false;
		}

		//save the data
		saveData();

		//update the level icons IF the level select panel is visible
		UIManager.Instance.createAllLevelCards();
	}

	/// <summary>
	/// Get's called when all levels are purchased
	/// </summary>
	public void unlockAllLevels()
	{
		//step through all the levels and set them to unlocked
		foreach(LevelSettings ls in allLevelSettings)
		{
			ls.isLocked = false;
		}

		//save the data
		saveData();

		//update the level icons IF the level select panel is visible
		UIManager.Instance.createAllLevelCards();
	}

	/// <summary>
	/// Returns the Collectible GameObject of the given index
	/// </summary>
	public GameObject getCollectibleObj(int index)
	{
		return allLevelSettings[index].collectibleObj;
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
	public bool			isLocked 				= true;
	public bool			hasCollectibleAchieved 	= false;
	public Sprite		unlockedTexture;
	public Sprite 		lockedTexture;



	public void saveData(int index, ES2Writer writer)
	{
		// Write our data to the file.
		writer.Write(isLocked, "isLocked_" + index.ToString());
		writer.Write(hasCollectibleAchieved, "hasCollectibleAchieved_" + index.ToString());
	}

	public void loadData(int index, ES2Reader reader)
	{
		// Read data from the file.
		isLocked				= reader.Read<bool>("isLocked_" + index.ToString());
		hasCollectibleAchieved	= reader.Read<bool>("hasCollectibleAchieved_" + index.ToString());
	}

	/// <summary>
	/// Unlocks the level.
	/// </summary>
	public void unlockLevel()
	{
		isLocked = false;
	}

	/// <summary>
	/// Unlocks the collectible.
	/// </summary>
	public void unlockCollectible()
	{
		hasCollectibleAchieved = true;
	}
}
