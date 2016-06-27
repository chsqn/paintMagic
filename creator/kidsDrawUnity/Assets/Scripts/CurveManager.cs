using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CurveManager : MonoBehaviour 
{

	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public AudioSource			audioSource;
	public AudioSource			audioSourceFinished;
	public AudioClip			audioPointTurnOnSFX;
	public AudioClip			audioFinishedSFX;
	public List<DrawingLine> 	unlockOrder;
	public Text					errorText;
	public ParticleSystem		finishedPSystem;
	public Animator				treasureChestAnim;
	public GameObject			star1Obj;
	public GameObject			star2Obj;
	public GameObject			star3Obj;

	#endregion publicParameter

	
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private int currentIndex = 0;
	private int	mErrorCount;

	private int mID_TRIGGER_treasurechest_next = Animator.StringToHash("TRIGGER_next");
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
	public void addErrorValue(int value)
	{
		mErrorCount += value;

		errorText.text = "Error Count: " + mErrorCount.ToString();
		
	}
	/// <summary>
	/// Called when the user pressed the homebtn
	/// </summary>
	public void loadMainUI()
	{
		Application.LoadLevel("mainUI");
	}

	public void unlockNext(DrawingLine line)
	{
		//if we still have to show a curve we show the next
		if(currentIndex < unlockOrder.Count -1)
		{

			StartCoroutine(unlockNextAfter(line));

		} else
		{
			//here we would show the final unlock
			StartCoroutine(hideAfter(line));

			//plays the level finsihed sequence
			StartCoroutine(levelFinishedSequence());
		}
	}

	/// <summary>
	/// Plays the Level the finished sequence.
	/// </summary>
	IEnumerator levelFinishedSequence()
	{
		audioSourceFinished.PlayOneShot(audioFinishedSFX);

		//play some particles
		finishedPSystem.gameObject.SetActive(true);

		//wait a bit
		yield return new WaitForSeconds(0.5f);

		//show the treasure chest with the stars on it
		treasureChestAnim.gameObject.SetActive(true);

		//wait a bit
		yield return new WaitForSeconds(2.3f);

		//trigger the first star
		star1Obj.SetActive(true);
		treasureChestAnim.SetTrigger(mID_TRIGGER_treasurechest_next);

		//wait a bit
		yield return new WaitForSeconds(0.5f);
		star2Obj.SetActive(true);
		treasureChestAnim.SetTrigger(mID_TRIGGER_treasurechest_next);

		//wait a bit
		yield return new WaitForSeconds(0.5f);
		star3Obj.SetActive(true);
		treasureChestAnim.SetTrigger(mID_TRIGGER_treasurechest_next);

		//wait a bit
		yield return new WaitForSeconds(0.5f);

		treasureChestAnim.SetTrigger(mID_TRIGGER_treasurechest_next);
	}

	/// <summary>
	/// Shows the next line 
	/// </summary>
	IEnumerator unlockNextAfter(DrawingLine line)
	{
		

		//hide the curve
		unlockOrder[currentIndex].gameObject.SetActive(false);
		//show the elements
		foreach(GameObject obj in line.allObjectsToShow)
			obj.SetActive(true);

		currentIndex += 1;

		//wait a bit before we show the next curve
		yield return new WaitForSeconds(0.1f);
		unlockOrder[currentIndex].gameObject.SetActive(true);
	}

	IEnumerator hideAfter(DrawingLine line)
	{
		yield return new WaitForSeconds(0.0f);
		unlockOrder[currentIndex].gameObject.SetActive(false);
		//show the elements
		foreach(GameObject obj in line.allObjectsToShow)
			obj.SetActive(true);
	}

	public void resetDrawingCurves()
	{
		foreach(DrawingLine line in unlockOrder)
		{
			line.gameObject.SetActive(false);

			foreach(GameObject obj in line.allObjectsToShow)
			{
				obj.SetActive(false);
			}

		}

		unlockOrder[0].gameObject.SetActive(true);

		currentIndex = 0;
	}
	#endregion

	
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
