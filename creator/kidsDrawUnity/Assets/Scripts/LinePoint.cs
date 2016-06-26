using UnityEngine;
using System.Collections;

public class LinePoint : MonoBehaviour 
{


	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public int 				ID;
	public SpriteRenderer 	bgSprite;
	public GameObject		unlockEffect;
	public Color			onColor;
	public Color			offColor;
	public Color			errorColor = Color.red;
	public AudioSource		unlockSFX;

	public enum pointState
	{
		isOff 	= 0,
		isOn 	= 1,
		isError = 2
	};

	public pointState currentPointState;

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
		//set the point to be off at the start
		turnOff();
	}

	#endregion MonoBehaviour

	
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	public void turnOn()
	{
		bgSprite.color 		= onColor;
		currentPointState 	= pointState.isOn;
		unlockEffect.gameObject.SetActive(true);
		Instantiate(unlockEffect, this.transform.position, Quaternion.identity);
	}

	public void turnOff()
	{
		bgSprite.color 		= offColor;
		currentPointState	= pointState.isOff;
		//unlockEffect.gameObject.SetActive(false);
	}

	public void turnError()
	{
		bgSprite.color 		= errorColor;
		currentPointState 	= pointState.isError;
	}

	#endregion

	
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
