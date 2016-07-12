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

	[Header("--------SOUNDS----------")]
	public AudioSource	audioSourceSFX;
	public AudioClip	clickSFX;
	public AudioClip	swipeSFX;

	[Header("----------MAIN----------")]
	public Transform	panelMoverTrans;
	public Transform 	playButtonTrans;
	public Transform	leftButtonTrans;

	public float[]		panelPositions;

	[Header("-------COLLECTIBLE--------")]
	public Transform	collectibleCardsContainer;
	public Transform	collectibleCardBtnPrefab;
	public GameObject	collectibleBackgroundObj;

	#endregion publicParameter
	
	
	// -----------------------------
	//	private datamember
	// -----------------------------
	#region privateMember
	private int 		mCurrentWorldMapIndex = 0;
	public 	string 		mLevelToLoad;
	private GameObject	mCurrentCollectible;
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

	void OnEnable()
	{
		//create all the collectible cards at start
		for(int i = 0; i < GameManager.Instance.allLevelSettings.Length; i++)
		{
			Transform newCollectible = Instantiate(collectibleCardBtnPrefab, Vector3.zero, Quaternion.identity) as Transform;

			newCollectible.SetParent(collectibleCardsContainer, false);

			UI_CollectibleBtn collectibleBtn = newCollectible.GetComponent<UI_CollectibleBtn>();

			collectibleBtn.indexID = i;
			collectibleBtn.UpdateButton();

		}
	}

	#endregion MonoBehaviour

	// -----------------------------
	//	button presses
	// -----------------------------
	#region Button Presses
	/// <summary>
	/// Shows the collectibel along with the black background
	/// </summary>
	public void showCollectible(int levelIndex, Vector3 startPos)
	{
		//show the black background
		collectibleBackgroundObj.SetActive(true);

		//instantiate the collectible
		mCurrentCollectible =  Instantiate(GameManager.Instance.getCollectibleObj(), startPos, Quaternion.identity) as GameObject;

		TweenHelper.unhideAndScale(mCurrentCollectible, Vector3.zero, Vector3.one, 0.4f, iTween.EaseType.easeOutBack, "none", this.gameObject);
		TweenHelper.moveWorld(mCurrentCollectible, new Vector3(Camera.main.transform.position.x, 0.0f, Camera.main.transform.position.z + 50.0f), 0.4f, iTween.EaseType.easeOutBack, "none", this.gameObject);

		//activate the collectible so we can play with it
		CollectibleCtrl collectCtrl = mCurrentCollectible.GetComponentInChildren<CollectibleCtrl>(true);

		collectCtrl.gameObject.SetActive(true);
	}

	public void hideCollectible()
	{
		//play the click sound
		audioSourceSFX.PlayOneShot(clickSFX);

		//hide the black background
		collectibleBackgroundObj.SetActive(false);

		//destroy the collectible
		if(mCurrentCollectible != null)
			Destroy(mCurrentCollectible);

	}

	/// <summary>
	/// Called when the play button get's pressed
	/// </summary>
	public void playButtonPressed()
	{
		//IF the play button doesn't have a level we show the worldmap
		if(mLevelToLoad == "")
		{
			print("now playing click sound");
			audioSourceSFX.PlayOneShot(clickSFX);
			audioSourceSFX.PlayOneShot(swipeSFX);
			showWorldMap();
		} else
		{
			audioSourceSFX.PlayOneShot(clickSFX);
			//else load the level
			Application.LoadLevel(mLevelToLoad);
		}
	}
	
	
	/// <summary>
	/// Called when the level button get's pressed
	/// </summary>
	public void levelToggleButtonPressed(UI_LevelButton levelBtn)
	{
		//play the click button sound
		audioSourceSFX.PlayOneShot(clickSFX);

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
		audioSourceSFX.PlayOneShot(clickSFX);
		audioSourceSFX.PlayOneShot(swipeSFX);
		moveMenu(panelMoverTrans.gameObject, new Vector3(panelPositions[0], panelMoverTrans.localPosition.y, panelMoverTrans.localPosition.z), 0.5f, iTween.EaseType.easeOutBack, "none", this.gameObject);
	}

	public void showCollectibleMenu()
	{
		audioSourceSFX.PlayOneShot(clickSFX);
		audioSourceSFX.PlayOneShot(swipeSFX);
		moveMenu(panelMoverTrans.gameObject, new Vector3(panelPositions[1], panelMoverTrans.localPosition.y, panelMoverTrans.localPosition.z), 0.5f, iTween.EaseType.easeOutBack, "none", this.gameObject);
	}

	/// <summary>
	/// Shows the world map.
	/// </summary>
	public void showWorldMap()
	{
		audioSourceSFX.PlayOneShot(clickSFX);
		audioSourceSFX.PlayOneShot(swipeSFX);
		moveMenu(panelMoverTrans.gameObject, new Vector3(panelPositions[2], panelMoverTrans.localPosition.y, panelMoverTrans.localPosition.z), 0.5f, iTween.EaseType.easeOutBack, "none", this.gameObject);
	}

	public void showLevelMap()
	{
		audioSourceSFX.PlayOneShot(clickSFX);
		audioSourceSFX.PlayOneShot(swipeSFX);
		moveMenu(panelMoverTrans.gameObject, new Vector3(panelPositions[3], panelMoverTrans.localPosition.y, panelMoverTrans.localPosition.z), 0.5f, iTween.EaseType.easeOutBack, "none", this.gameObject);
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
