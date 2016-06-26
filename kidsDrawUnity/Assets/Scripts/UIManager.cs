using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------
	#region publicParameter
	// Static singleton property
	public static UIManager Instance { get; private set; }

	public Transform	panelMoverTrans;
	public Transform 	playButtonTrans;
	public Transform	leftButtonTrans;

	public float[]		panelPositions;

	#endregion publicParameter
	
	
	// -----------------------------
	//	private datamember
	// -----------------------------
	#region privateMember
	private int 		mCurrentWorldMapIndex = 0;
	public 	string 		mLevelToLoad;
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
		// First we check if there are any other instances conflicting
		if(Instance != null && Instance != this)
		{
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}
		
		// Here we save our singleton instance
		Instance = this;
		
		
		////////////////////////////////////////////////////////////////////
		/// SINGLETON //////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////
		

		
	}

	#endregion MonoBehaviour

	// -----------------------------
	//	button presses
	// -----------------------------
	#region Button Presses
	/// <summary>
	/// Called when the play button get's pressed
	/// </summary>
	public void playButtonPressed()
	{
		//IF the play button doesn't have a level we show the worldmap
		if(mLevelToLoad == "")
		{
			showWorldMap();
		} else
		{
			//else load the level
			Application.LoadLevel(mLevelToLoad);
		}
	}
	
	
	/// <summary>
	/// Called when the level button get's pressed
	/// </summary>
	public void levelToggleButtonPressed(UI_LevelButton levelBtn)
	{
		if(levelBtn.toggleBtn.isOn == true)
		{
			//only show the button again if it isn't already there
			if(playButtonTrans.gameObject.activeInHierarchy == false)
				showPlayButton(true, levelBtn.levelToLoad);
			
		} else
		{
			hidePlayButton();
		}
		
		
	}

	#endregion
	
	// -----------------------------
	//	public api
	// -----------------------------
	#region publicAPI
	public void showMainMenu()
	{
		moveMenu(panelMoverTrans.gameObject, new Vector3(panelPositions[0], panelMoverTrans.localPosition.y, panelMoverTrans.localPosition.z), 0.5f, iTween.EaseType.easeOutBack, "none", this.gameObject);
	}


	/// <summary>
	/// Shows the world map.
	/// </summary>
	public void showWorldMap()
	{
		moveMenu(panelMoverTrans.gameObject, new Vector3(panelPositions[1], panelMoverTrans.localPosition.y, panelMoverTrans.localPosition.z), 0.5f, iTween.EaseType.easeOutBack, "none", this.gameObject);
	}

	public void showLevelMap()
	{
		moveMenu(panelMoverTrans.gameObject, new Vector3(panelPositions[2], panelMoverTrans.localPosition.y, panelMoverTrans.localPosition.z), 0.5f, iTween.EaseType.easeOutBack, "none", this.gameObject);
	}

	/// <summary>
	/// Moves the world map. Called when the side buttons are pressed
	/// </summary>
	public void moveWorldMap(int direction)
	{
		/*
		UI_worldMap[] allWorldMaps = worldMapTrans.GetComponentsInChildren<UI_worldMap>();

		//check if we can move
		if(direction == 1)
		{
			if(mCurrentWorldMapIndex == 0)
				return;

		}

		if(direction == -1)
		{
			if(mCurrentWorldMapIndex == allWorldMaps.Length -1)
				return;
		}


		//get the distance tot he next worldmap
		float distance = Mathf.Abs(allWorldMaps[mCurrentWorldMapIndex].transform.localPosition.x - allWorldMaps[mCurrentWorldMapIndex + (direction * -1)].transform.localPosition.x);

		//move the worldmap
		moveMenu(worldMapTrans.gameObject, new Vector3(worldMapTrans.transform.localPosition.x + (distance * direction),
		                                               worldMapTrans.transform.localPosition.y,
		                                               worldMapTrans.transform.localPosition.z), 0.5f, iTween.EaseType.easeOutBack, "none", this.gameObject);

		//increase the index
		mCurrentWorldMapIndex += (direction * -1);

		*/
	}



	#endregion
	
	
	// -----------------------------
	//	private api
	// -----------------------------
	#region privateAPI
	/// <summary>
	/// Shows the play button.
	/// </summary>
	/// <param name="isAnimated">If set to <c>true</c> is animated.</param>
	/// <param name="levelName">Level name.</param>
	void showPlayButton(bool isAnimated, string levelName)
	{
		if(isAnimated)
		{
			unhideAndScale(playButtonTrans.gameObject, new Vector3(2,2,2), Vector3.one, 0.3f, iTween.EaseType.easeOutBack, "none", this.gameObject);
		} else
		{
			playButtonTrans.gameObject.SetActive(true);
		}

		//set the level to load
		mLevelToLoad = levelName;
	}

	/// <summary>
	/// Hides the play button.
	/// </summary>
	void hidePlayButton()
	{
		playButtonTrans.gameObject.SetActive(false);
		mLevelToLoad = "";
	}
	#endregion


	#region GENERIC HELPERS
	//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
	//-------------------------------------------------------------------------------------------------
	//----------------itween helper stuff--------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------
	//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX	
	public void scaleMenu(GameObject menuToScale, Vector3 scaleAmount, float scaleTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		iTween.ScaleTo(menuToScale, iTween.Hash("scale", scaleAmount, "islocal", true, "time", scaleTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}
	
	public void moveMenu(GameObject menuToMove, Vector3 position, float moveTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		iTween.MoveTo(menuToMove, iTween.Hash("position", position, "islocal", true, "time", moveTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}
	
	public void unhideAndScale(GameObject inputObj, Vector3 scaleFrom, Vector3 scaleTo, float scaleTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		inputObj.SetActive(true);
		inputObj.transform.localScale = scaleFrom;
		iTween.ScaleTo(inputObj, iTween.Hash("scale", scaleTo, "islocal", true, "time", scaleTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}
	#endregion

}
