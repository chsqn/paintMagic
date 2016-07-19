using UnityEngine;
using System.Collections;

public class GenericEyeController : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	[Header("------SETUP-------")]
	public GameObject	closedEyeLeft;
	public GameObject	closedEyeRight;
	public GameObject	pupilLeft;
	public GameObject	pupilRight;
	public GameObject	openEyeLeft;
	public GameObject	openEyeRight;

	[Header("------SETTINGS-------")]
	public float blinkStartTimeMin = 1.0f;
	public float blinkStartTimeMax = 2.0f;
	public float eyeRadius = 10.0f;
	public float pupilStartMoveTimeMin = 2.0f;
	public float pupilStartMovetimeMax = 4.0f;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private float mCurrentBlinkStartTime;
	private float mCurrentPupilStartMoveTime;
	private Vector3 mPupilLeftCenterPos;
	private Vector3 mPupilRightCenterPos;

	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Start()
	{
		mCurrentBlinkStartTime 		= Random.Range(blinkStartTimeMin, blinkStartTimeMax);	
		mCurrentPupilStartMoveTime 	= Time.time + Random.Range(pupilStartMoveTimeMin, pupilStartMovetimeMax);
		mPupilLeftCenterPos			= pupilLeft.transform.position;
		mPupilRightCenterPos		= pupilRight.transform.position;
	}

	void Update()
	{
		if(Time.time > mCurrentBlinkStartTime)
		{
			StartCoroutine(closeEyesFor());
			mCurrentBlinkStartTime = Time.time + Random.Range(blinkStartTimeMin, blinkStartTimeMax);
		}

		if(Time.time > mCurrentPupilStartMoveTime)
		{
			movePupil();
			mCurrentPupilStartMoveTime = Time.time + Random.Range(pupilStartMoveTimeMin, pupilStartMovetimeMax);
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
	void movePupil()
	{
		//get a random direction
		Vector3 randDir = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, pupilLeft.transform.position.z) * eyeRadius;

		pupilLeft.transform.position 	= new Vector3(mPupilLeftCenterPos.x + randDir.x, mPupilLeftCenterPos.y + randDir.y, pupilLeft.transform.position.z);
		pupilRight.transform.position	= new Vector3(mPupilRightCenterPos.x + randDir.x, mPupilRightCenterPos.y + randDir.y, pupilRight.transform.position.z);


	}

	IEnumerator closeEyesFor()
	{
		closeEye();
		yield return new WaitForSeconds(0.1f);
		openEye();
	}

	void closeEye()
	{
		openEyeLeft.SetActive(false);
		openEyeRight.SetActive(false);
		pupilLeft.SetActive(false);
		pupilRight.SetActive(false);

		closedEyeLeft.SetActive(true);
		closedEyeRight.SetActive(true);
		
	}

	void openEye()
	{
		openEyeLeft.SetActive(true);
		openEyeRight.SetActive(true);
		pupilLeft.SetActive(true);
		pupilRight.SetActive(true);

		closedEyeLeft.SetActive(false);
		closedEyeRight.SetActive(false);

	}
	#endregion
}
