using UnityEngine;
using System.Collections;

public class CrystalCtrl : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public float 			distToCam = 50.0f;
	public ParticleSystem 	touchPSystem;
	public ParticleSystem	sparklePSystem;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private bool mIsSpawned = false;
	private bool mTouched 	= false;

	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void OnEnable()
	{
		mIsSpawned = false;

		TweenHelper.unhideAndScale(this.gameObject, Vector3.zero, new Vector3(5,5,5), 0.8f, iTween.EaseType.easeOutBack, "none", this.gameObject);

		//set the default position first
		this.transform.position = new Vector3(this.transform.position.x, 80.0f, this.transform.position.z);

		TweenHelper.moveWorld(this.gameObject, new Vector3(this.transform.position.x, 115.0f, this.transform.position.z), 0.4f, iTween.EaseType.easeOutQuad, "moveToGround", this.gameObject);
	}

	void Update()
	{
		if(mTouched)
		{
			this.transform.RotateAround(this.transform.position, Vector3.up, 15.0f * Time.deltaTime);
		}
	}


	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	/// <summary>
	/// Called when the crystal is touched
	/// </summary>
	public void touched()
	{
		print("got touched and isSpawned is " + mIsSpawned);

		//can only touch if it is spawned
		if(mIsSpawned == false)
			return;
		
		//don't do anything if touched already
		if(mTouched)
			return;

		mTouched = true;

		touchPSystem.gameObject.SetActive(true);
		sparklePSystem.gameObject.SetActive(false);

		//move the crystal infront of the camera
		StartCoroutine(removeCrystal());

		
	}

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI
	IEnumerator removeCrystal()
	{
		//move the crystal to the camera
		TweenHelper.moveWorld(this.gameObject, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + distToCam), 1.5f, iTween.EaseType.easeInBack, "none", this.gameObject);

		yield return new WaitForSeconds(3.0f);

		//move the crystal to the home button
		TweenHelper.unhideAndScale(this.gameObject, this.transform.localScale, Vector3.zero, 1.5f, iTween.EaseType.easeInBack, "hideCrystal", this.gameObject);
		TweenHelper.moveWorld(this.gameObject, new Vector3(-15.0f, 150.0f, this.transform.position.z), 1.5f, iTween.EaseType.easeInBack, "none", this.gameObject);

	}

	void hideCrystal()
	{
		this.gameObject.SetActive(false);
	}

	void moveToGround()
	{
		TweenHelper.moveWorld(this.gameObject, new Vector3(this.transform.position.x, 94.0f, this.transform.position.z), 0.5f, iTween.EaseType.easeOutBounce, "setToSpawned", this.gameObject);
	}

	void setToSpawned()
	{
		mIsSpawned = true;

	}

	#endregion
}
