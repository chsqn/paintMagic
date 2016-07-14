using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_LevelButton : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------
	#region publicParameter
	[Header("----- SETUP ---- ")]
	public GameObject 	playButton;
	public GameObject 	lockIcon;

	public Image		levelBGImg;
	public Image		levelBGBorderImg;
	public Image		collectibleBGImg;
	public Image		collectibleBGBorderImg;

	public GameObject	greyedOutLevelObj;
	public GameObject	greyedOutCollectibleObj;

	public Image		levelImg;
	public Image		collectibleImg;

	[Header("----- SETTINGS ---- ")]
	public Color		levelBGColorLocked;
	public Color		levelBGFrameColorLocked;
	public Color		levelBGColorUnlocked;
	public Color		levelBGFrameColorUnlocked;
	public Color		collectibleBGColorLocked;
	public Color		collectibleBGFrameColorLocked;
	public Color		collectibleBGColorUnlocked;
	public Color		collectibleBGFrameColorUnlocked;

	[Header("----- SET BY CODE ---- ")]
	public int 			levelIndex;
	#endregion publicParameter
	
	
	// -----------------------------
	//	private datamember
	// -----------------------------
	#region privateMember
	
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------
	#region MonoBehaviour

	#endregion MonoBehaviour
	
	
	// -----------------------------
	//	public api
	// -----------------------------
	#region publicAPI
	public void playButtonPressed()
	{
		Application.LoadLevel("level_" + (levelIndex+1).ToString("00"));	
	}

	/// <summary>
	/// Get's called when we create the levelCardBtn and it is locked
	/// </summary>
	public void setToLocked()
	{
		greyedOutLevelObj.SetActive(true);
		greyedOutCollectibleObj.SetActive(true);
		playButton.SetActive(false);
		lockIcon.SetActive(true);

		//set the colors
		levelBGImg.color 				= levelBGColorLocked;
		levelBGBorderImg.color 			= levelBGFrameColorLocked;
		collectibleImg.color			= collectibleBGColorLocked;
		collectibleBGBorderImg.color 	= collectibleBGFrameColorLocked;
	}

	/// <summary>
	/// Get's called when we create the levelCardBtn and it is playable
	/// also set the collectible here
	/// </summary>
	public void setToPlayable(bool hasCollectibleAchieved)
	{
		greyedOutLevelObj.SetActive(false);
		playButton.SetActive(true);
		lockIcon.SetActive(false);

		//set the colors
		levelBGImg.color 				= levelBGColorUnlocked;
		levelBGBorderImg.color 			= levelBGFrameColorUnlocked;

		if(hasCollectibleAchieved)
		{
			collectibleBGImg.color			= collectibleBGColorUnlocked;
			collectibleBGBorderImg.color 	= collectibleBGFrameColorUnlocked;
			greyedOutCollectibleObj.SetActive(false);
		} else
		{
			collectibleBGImg.color			= collectibleBGColorLocked;
			collectibleBGBorderImg.color 	= collectibleBGFrameColorLocked;
			greyedOutCollectibleObj.SetActive(true);

		}

	}

	/// <summary>
	/// Called when the player pressed the collecible button
	/// </summary>
	public void showCollectible()
	{
		UIManager.Instance.showCollectible(levelIndex, collectibleBGBorderImg.transform.position);
	}
	#endregion
	
	
	// -----------------------------
	//	private api
	// -----------------------------
	#region privateAPI

	#endregion
}
