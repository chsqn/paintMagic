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
	public float reCenterPupilAfter		= 1.0f;
	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private float mCurrentBlinkStartTime;
	private float mCurrentPupilStartMoveTime;
	private Vector3 mPupilLeftCenterPos;
	private Vector3 mPupilRightCenterPos;
	private RaycastHit hit;

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

		/*
		if(Time.time > mCurrentPupilStartMoveTime)
		{
			movePupil();
			mCurrentPupilStartMoveTime = Time.time + Random.Range(pupilStartMoveTimeMin, pupilStartMovetimeMax);
		}
		*/
	}

	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	public void setEyeToTouchDirection(Collider coll)
	{
		//get the touched point
		if(coll.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3.0f)), out hit, 99999.9f))
		{
			Vector3 leftEyeLookDir = (openEyeLeft.transform.position - hit.point).normalized * eyeRadius * -1.0f;
			Vector3 rightEyeLookDir = (openEyeRight.transform.position - hit.point).normalized * eyeRadius * -1.0f;

			pupilLeft.transform.position 	= new Vector3(mPupilLeftCenterPos.x + leftEyeLookDir.x, mPupilLeftCenterPos.y + leftEyeLookDir.y, pupilLeft.transform.position.z);
			pupilRight.transform.position	= new Vector3(mPupilRightCenterPos.x + rightEyeLookDir.x, mPupilRightCenterPos.y + rightEyeLookDir.y, pupilRight.transform.position.z);

			StopAllCoroutines();
			StartCoroutine(resetPupilPosAfter());
		}
	}
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

	IEnumerator resetPupilPosAfter()
	{
		yield return new WaitForSeconds(reCenterPupilAfter);
		pupilLeft.transform.position 	= new Vector3(openEyeLeft.transform.position.x, openEyeLeft.transform.position.y, openEyeLeft.transform.position.z - 2.0f);
		pupilRight.transform.position 	= new Vector3(openEyeRight.transform.position.x, openEyeRight.transform.position.y, openEyeRight.transform.position.z - 2.0f);
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
