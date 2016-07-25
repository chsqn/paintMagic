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
	public GameObject	mainPanelObj;
	public GameObject	levelSelectPanelObj;

	[Header("-------LEVEL CARDS -------")]
	public GameObject	levelCardPrefab;
	public GameObject	levelCardContainer;

	[Header("===---ALL WORLD PANEL ----")]
	public GameObject[] allWorldPanels;

	[Header("-------COLLECTIBLES -------")]
	public GameObject 	collectibleBackgroundObj;

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



	#endregion MonoBehaviour

	
	// -----------------------------
	//	public api
	// -----------------------------
	#region publicAPI
	public void moveWorldPanels(int moveValue)
	{
		//hide the curent world index panel
		allWorldPanels[mCurrentWorldMapIndex].SetActive(false);

		//add to the worldIndex
		mCurrentWorldMapIndex += moveValue;

		//wrap the index around
		if(mCurrentWorldMapIndex < 0)
			mCurrentWorldMapIndex = allWorldPanels.Length -1;

		if(mCurrentWorldMapIndex > allWorldPanels.Length-1)
			mCurrentWorldMapIndex = 0;

		//show the next worldmap
		allWorldPanels[mCurrentWorldMapIndex].SetActive(true);


	}

	/// <summary>
	/// Called when the users hit's the home button from the levelSelectPanel
	/// </summary>
	public void showMainMenu()
	{
		audioSourceSFX.PlayOneShot(clickSFX);

		levelSelectPanelObj.SetActive(false);
		mainPanelObj.SetActive(true);

	}

	/// <summary>
	/// Called when the users hit's the play butto from the main menue
	/// </summary>
	public void showLevelsMenu()
	{
		audioSourceSFX.PlayOneShot(clickSFX);
		levelSelectPanelObj.SetActive(true);
		mainPanelObj.SetActive(false);

		//this generates all the level cards
		createAllLevelCards();
	}

	/// <summary>
	/// Called when a collectible button was pressed in the level select menu
	/// </summary>
	public void showCollectible(int index, Vector3 startPos)
	{
		print("now showing collecitble " + index);

		//show the black background
		collectibleBackgroundObj.SetActive(true);

		//instantiate the collectible
		mCurrentCollectible =  Instantiate(GameManager.Instance.getCollectibleObj(index), startPos, Quaternion.identity) as GameObject;

		TweenHelper.unhideAndScale(mCurrentCollectible, Vector3.zero, Vector3.one, 0.4f, iTween.EaseType.easeOutBack, "none", this.gameObject);
		TweenHelper.moveWorld(mCurrentCollectible, new Vector3(Camera.main.transform.position.x, 0.0f, Camera.main.transform.position.z + 50.0f), 0.4f, iTween.EaseType.easeOutBack, "none", this.gameObject);

		//activate the collectible so we can play with it
		CollectibleCtrl collectCtrl = mCurrentCollectible.GetComponentInChildren<CollectibleCtrl>(true);

		collectCtrl.gameObject.SetActive(true);

	}

	/// <summary>
	/// Hides the collectible.
	/// </summary>
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
	/// Creates all the level cards.
	/// </summary>
	public void createAllLevelCards()
	{
		//only create all the level cards if the level select panel is visible
		if(levelSelectPanelObj.activeInHierarchy == false)
			return;

		//first we make sure that there are no levelcards any more
		UI_LevelButton[] allExistingLevelButtons = levelCardContainer.GetComponentsInChildren<UI_LevelButton>(true);
		foreach(UI_LevelButton lb in allExistingLevelButtons)
		{
			Destroy(lb.gameObject);
		}

		//now create a level button for each level
		for(int i = 0; i < GameManager.Instance.allLevelSettings.Length; i++)
		{
			//instantiate the new card
			GameObject newLevelCard = Instantiate(levelCardPrefab, Vector3.zero, Quaternion.identity) as GameObject;

			//parent the new card to the container
			newLevelCard.transform.SetParent(levelCardContainer.transform);
			newLevelCard.transform.localScale = Vector3.one;
			newLevelCard.transform.localPosition = new Vector3(newLevelCard.transform.position.x, newLevelCard.transform.position.y, 0.0f);

			//get the ui_levelButton script from it
			UI_LevelButton levelCardScript = newLevelCard.GetComponent<UI_LevelButton>();

			//set the settings
			levelCardScript.levelIndex = i;

			//set the container size
			GridLayoutGroup gridLayout 			= levelCardContainer.GetComponent<GridLayoutGroup>();
			RectTransform	gridLayoutTrans 	= gridLayout.GetComponent<RectTransform>();
			float 			scrollSize			= (gridLayout.cellSize.x + gridLayout.spacing.x) * GameManager.Instance.allLevelSettings.Length; 
			gridLayoutTrans.sizeDelta = new Vector2(scrollSize, gridLayoutTrans.sizeDelta.y);

			//set the Container postition
			levelCardContainer.transform.localPosition = new Vector3(scrollSize/2.0f, levelCardContainer.transform.localPosition.y, levelCardContainer.transform.localPosition.z);


			//set the locked if locked
			if(GameManager.Instance.allLevelSettings[i].isLocked)
			{
				levelCardScript.setToLocked();
			} else
			{
				//set the playable with collectible visible or not
				if(GameManager.Instance.allLevelSettings[i].hasCollectibleAchieved)
				{
					levelCardScript.setToPlayable(true);
				} else
				{
					levelCardScript.setToPlayable(false);
				}
			}

		}

	}
	#endregion
	
	
	// -----------------------------
	//	private api
	// -----------------------------
	#region privateAPI

	#endregion


	#region GENERIC HELPERS
	#endregion

}
