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
	public GameObject		geoObj;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private bool mIsSpawned = false;
	private bool mTouched 	= false;

	private RaycastHit 	hit;
	private Ray			ray;
	private Collider 	coll;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		coll = GetComponent<Collider>();
	}

	void OnEnable()
	{
		mIsSpawned = false;

	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			if(coll.Raycast(ray, out hit, 100000.0f))
			{
				if(hit.collider == coll)
				{
					touchPSystem.gameObject.SetActive(true);
					touchPSystem.Play();
					print("got raycast");
				}
			}
		}

		geoObj.transform.RotateAround(geoObj.transform.position, Vector3.up, 15.0f * Time.deltaTime);
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


		mTouched = true;

		touchPSystem.gameObject.SetActive(true);
		touchPSystem.Play();
		sparklePSystem.gameObject.SetActive(false);
	
		
	}

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
