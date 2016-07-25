using UnityEngine;
using System.Collections;

public class BubbleMoveCtrl : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public float 			moveUpSpeed 		= 1.0f;
	public AnimationCurve 	sideMoveCurve;
	public float 			sideMoveSpeed 		= 1.0f;
	public float 			sideMoveStrength 	= 1.0f;
	public float			maxHeight 			= 600.0f;
	public GameObject		touchEffekt;
	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private float 	mLerpValue = 0.0f;
	private Vector3 mStartPos;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void OnEnable()
	{
		mStartPos = this.transform.position;
	}


	void Update()
	{
		mLerpValue += (Time.deltaTime * sideMoveSpeed);

		this.transform.position = new Vector3(mStartPos.x + (sideMoveCurve.Evaluate(mLerpValue) * sideMoveStrength),
			transform.position.y + (moveUpSpeed * Time.deltaTime), transform.position.z);

		if(this.transform.position.y > maxHeight)
			Destroy(this.gameObject);
	}

	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	/// <summary>
	/// Called when bubble get's touched instantiates bubble particles and destroys the bubble itself
	/// </summary>
	public void touched()
	{
		Instantiate(touchEffekt, this.transform.position, Quaternion.identity);

		Destroy(this.gameObject);
	}
	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
