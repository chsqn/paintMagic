using UnityEngine;
using System.Collections;

public class GlowWormBehaviour : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public Transform 	rotTrans;

	public Vector2 		verticalSpeed;
	public Vector2		rotationSpeed;
	public Vector2		rotOffset;
	public float		size = 0.5f;
	public float		trailLength = 0.3f;


	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private bool mIsMoving = false;

	private float mVerticalSpeed;
	private float mRotationSpeed;
	private float mRotOffset;
	private TrailRenderer mTrailRenderer;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		mTrailRenderer 				= rotTrans.GetComponent<TrailRenderer>();

		mTrailRenderer.startWidth 	= size;
		rotTrans.localScale 		= new Vector3(size, size, size);
		mTrailRenderer.time 		= trailLength;
	}

	void OnEnable()
	{
		mVerticalSpeed 		= Random.Range(verticalSpeed.x, verticalSpeed.y);
		mRotationSpeed 		= Random.Range(rotationSpeed.x, rotationSpeed.y);
		mRotOffset			= Random.Range(rotOffset.x, rotOffset.y);
		mIsMoving 			= true;

		rotTrans.position 	= new Vector3(this.transform.position.x + mRotOffset, this.transform.position.y, this.transform.position.z);
	}

	void OnDisable()
	{
		mIsMoving = false;
	}

	void Update()
	{
		if(mIsMoving == false)
			return;

		//move it up
		this.transform.Translate(Vector3.up * mVerticalSpeed * Time.deltaTime);

		//rotate the child
		//rotTrans.RotateAround(this.transform.position, Vector3.up, this.transform.rotation.eulerAngles.y + (mRotationSpeed * Time.deltaTime));
		this.transform.Rotate(0.0f, mRotationSpeed * Time.deltaTime, 0.0f);

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
