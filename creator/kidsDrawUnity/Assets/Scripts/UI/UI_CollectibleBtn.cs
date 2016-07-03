using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_CollectibleBtn : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public int 		indexID;
	public Button 	button;

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
	/// <summary>
	/// Called when the button is pressed
	/// </summary>
	public void showCollectible()
	{
		
	}

	public void UpdateButton()
	{
		//load the data from that level
		GameManager.Instance.allLevelSettings[indexID].loadData(indexID);

		//check if it is locked
		if(GameManager.Instance.allLevelSettings[indexID].isLocked)
		{
			setToLocked();
		} else
		{
			setToUnlocked();
		}

	}


	public void setToLocked()
	{
		button.image.sprite			= GameManager.Instance.allLevelSettings[indexID].lockedTexture;
		button.interactable 		= false;
	}

	public void setToUnlocked()
	{
		button.image.sprite 	= GameManager.Instance.allLevelSettings[indexID].unlockedTexture;

	}

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
