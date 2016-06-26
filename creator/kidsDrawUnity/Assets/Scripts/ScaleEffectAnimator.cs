using UnityEngine;
using System.Collections;

public class ScaleEffectAnimator : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public float 			speed = 1.0f;
	public AnimationCurve 	animCurve;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private float 	mLerpValue 	= 0.0f;
	private bool	isAnimating = false;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void OnEnable()
	{
		isAnimating = true;
		mLerpValue = 0.0f;
	}

	void Update()
	{
		if(isAnimating == false)
			return;

		mLerpValue += Time.deltaTime * speed;

		transform.localScale = new Vector3(Mathfx.UnclampedLerp(0.0f, 1.0f, animCurve.Evaluate(mLerpValue)),
										   Mathfx.UnclampedLerp(0.0f, 1.0f, animCurve.Evaluate(mLerpValue)),
										   Mathfx.UnclampedLerp(0.0f, 1.0f, animCurve.Evaluate(mLerpValue)));



		if(mLerpValue >= 1.0f)
		{
			mLerpValue 	= 0.0f;
			isAnimating = false;
		}
		
	}

	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
