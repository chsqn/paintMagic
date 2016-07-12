using UnityEngine;
using System.Collections;

public class FireflyCtrl : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private ParticleSystem 	psystem;
	private bool			mTouched = false;
	private Vector3			mOrigPos;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		psystem = GetComponent<ParticleSystem>();
	}

	void Start()
	{
		mOrigPos = this.transform.position;
	}

	void Update()
	{
		if(mTouched == false)
			return;

		transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (15.0f * Time.deltaTime), this.transform.position.z);

		if(transform.position.y > 200.0f)
		{
			mTouched = false;
			transform.position = mOrigPos;
		}

	}
	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	public void touched()
	{
		mTouched = true;


	}

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
