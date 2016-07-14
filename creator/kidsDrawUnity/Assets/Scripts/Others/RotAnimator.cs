using UnityEngine;
using System.Collections;

public class RotAnimator : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public AnimationCurve 	animCurve;
	public float			animSpeed;
	public float			rotValue = 1.0f;
	#endregion publicParameter

	
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private float mLerpValue = 0.0f;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Update()
	{
		mLerpValue += (Time.deltaTime * animSpeed);
		//this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, animCurve.Evaluate(mLerpValue) * rotValue));
		this.transform.Rotate( new Vector3(0.0f, 0.0f, animCurve.Evaluate(mLerpValue) * rotValue));
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
