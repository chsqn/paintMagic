using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
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
	public GameObject			collectibleContainerObj;
	public Transform			collectibleSpawnPosTrans;
	public Transform			collectibleJumpPosTrans;
	public Transform			collectibleGroundPosTrans;
	public Transform			collectibleCamPosTrans;
	public Transform			collectibleHomeBtnTrans;
	public GameObject			bgEffectObj;
	public ParticleSystem		homeBtnParticleEffect;




	#endregion publicParameter

	
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private int currentIndex = 0;
	private int	mErrorCount;

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
	/// Called when the user pressed the homebtn
	/// </summary>
	public void loadMainUI()
	{
		print("now loading mainUI");
		Application.LoadLevel("mainUI");
	}


	public void addErrorValue(int value)
	{
		mErrorCount += value;
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


		//wait a bit
		yield return new WaitForSeconds(0.5f);

		//show the bg effect
		bgEffectObj.SetActive(true);

		int currentLevelIndex = GameManager.Instance.currentLevelIndex;

		//unlock the collectible in the gameManager
		GameManager.Instance.allLevelSettings[currentLevelIndex].unlockCollectible();

		//instantiate the collectible and parent it to the collectible container
		GameObject newCollectible = Instantiate(GameManager.Instance.getCollectibleObj(currentLevelIndex), collectibleSpawnPosTrans.position, Quaternion.identity) as GameObject;
		newCollectible.transform.SetParent(collectibleContainerObj.transform);

		//scale the collectible
		TweenHelper.unhideAndScale(newCollectible, Vector3.zero, new Vector3(1,1,1), 0.8f, iTween.EaseType.easeOutBack, "none", this.gameObject);

		//move the collectible to the jump pos
		TweenHelper.moveWorld(newCollectible, collectibleJumpPosTrans.position, 0.4f, iTween.EaseType.easeOutQuad, "none", this.gameObject);

		//wait until we arrived at the jump pos
		yield return new WaitForSeconds(0.4f);

		//move the object to the ground
		TweenHelper.moveWorld(newCollectible, collectibleGroundPosTrans.position, 0.5f, iTween.EaseType.easeOutBounce, "none", this.gameObject);

		//let the collectible rest on the ground a bit
		yield return new WaitForSeconds(2.0f);

		//move the collectible to the camera
		TweenHelper.moveWorld(newCollectible, collectibleCamPosTrans.position, 1.5f, iTween.EaseType.easeInCubic, "none", this.gameObject);

		//wait a bit
		yield return new WaitForSeconds(3.0f);

		//move the collectible to the homebtn
		TweenHelper.unhideAndScale(newCollectible, newCollectible.transform.localScale, Vector3.zero, 1.5f, iTween.EaseType.easeInBack, "none", this.gameObject);
		TweenHelper.moveWorld(newCollectible, collectibleHomeBtnTrans.position, 1.5f, iTween.EaseType.easeInCubic, "none", this.gameObject);

		//wait a bit and then play the particle effect and hide the collectible
		yield return new WaitForSeconds(1.5f);
		homeBtnParticleEffect.gameObject.SetActive(true);
		newCollectible.SetActive(false);
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
	public void moveTweenObj(GameObject objToMove, Vector3 position, float moveTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		iTween.MoveTo(objToMove, iTween.Hash("position", position, "islocal", false, "time", moveTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}

	#endregion
}
