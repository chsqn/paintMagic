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
	void OnEnable()
	{
		button.onClick.AddListener(() => showCollectible());
	}

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
		UIManager.Instance.showCollectible(indexID, this.transform.position);

		//play the click sound and the woosh sound
		UIManager.Instance.audioSourceSFX.PlayOneShot(UIManager.Instance.clickSFX);
		UIManager.Instance.audioSourceSFX.PlayOneShot(UIManager.Instance.swipeSFX);
	}

	public void UpdateButton()
	{
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
