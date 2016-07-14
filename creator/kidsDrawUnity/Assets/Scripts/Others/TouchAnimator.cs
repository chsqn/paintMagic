using UnityEngine;
using System.Collections;

public class TouchAnimator : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter

	public Vector3 		rotateAroundAmount;
	public GameObject	rotateAroundEffect;
	public AnimationCurve shakeRotAnimCurve;
	public float		shakeRotSpeed = 1.0f;
	public Vector3 		punchScaleAmount;

	public enum AnimType
	{
		rotateAround 	= 0,
		punchScale 		= 1,
		shakeRotation 	= 2
	};

	public AnimType currentAnimType;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private bool 	mIsAnimating = false;
	private float	mLerpValue = 0.0f;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Update()
	{
		if(mIsAnimating == false)
			return;

		mLerpValue += (Time.deltaTime * shakeRotSpeed);

		this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, shakeRotAnimCurve.Evaluate(mLerpValue)));


		if(mLerpValue >= 1.0f)
		{
			mLerpValue = 0.0f;
			mIsAnimating = false;
		}
	}

	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	public void Touched()
	{
		//don't do anything if we are already animating
		if(mIsAnimating)
			return;

		//don't do anything if there is a itween object on it
		if(GetComponent<iTween>() != null)
			return;

		switch(currentAnimType)
		{
		case AnimType.rotateAround:
			TweenHelper.rotateAdd(this.gameObject, rotateAroundAmount, 1.0f, iTween.EaseType.easeOutBack, "none", this.gameObject);
			GameObject newParticles = Instantiate(rotateAroundEffect, this.transform.position, Quaternion.identity) as GameObject;
			newParticles.transform.SetParent(this.transform);
			break;
		case AnimType.punchScale:
			TweenHelper.punchScale(this.gameObject, punchScaleAmount, 1.0f, "none", this.gameObject);
			break;
		case AnimType.shakeRotation:
			mIsAnimating = true;
			break;
		}

	}



	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
