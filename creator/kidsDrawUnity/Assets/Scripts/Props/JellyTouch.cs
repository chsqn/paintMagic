using UnityEngine;
using System.Collections;

public class JellyTouch : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public float force = 2000.0f;

	public GameObject[] objsToHideWhenTouched;
	public GameObject[] objsToShowWhenTouched;
	public GenericEyeController eyeCtrl;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private BoxCollider 	coll;
	private RaycastHit 		hit;
	private JellySprite		mJellySprite;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		coll 			= GetComponent<BoxCollider>();	
		mJellySprite 	= GetComponent<JellySprite>();
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(coll.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3.0f)), out hit, 99999.9f))
			{
				StopAllCoroutines();
				StartCoroutine(showTouchedObjsFor());
				//mJellySprite.AddForceAtPosition((new Vector2(this.transform.position.x, this.transform.position.y) - new Vector2(hit.point.x, hit.point.y)) * force , new Vector2(hit.point.x, hit.point.y));
				//mJellySprite.AddForceAtPosition(Vector2.right * force , new Vector2(hit.point.x, hit.point.y));

				if(hit.point.x < this.transform.position.x)
				{
					mJellySprite.AddForceAtPosition(new Vector2(0.7f, 0.3f) * force , new Vector2(hit.point.x, hit.point.y));
				} else
				{
					mJellySprite.AddForceAtPosition(new Vector2(-0.7f, 0.3f) * force , new Vector2(hit.point.x, hit.point.y));

				}


			}
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
	IEnumerator showTouchedObjsFor()
	{
		foreach(GameObject obj in objsToHideWhenTouched)
		{
			obj.SetActive(false);
		}

		foreach(GameObject obj in objsToShowWhenTouched)
		{
			obj.SetActive(true);
		}

		//turn off the eye controller
		if(eyeCtrl != null)
			eyeCtrl.enabled = false;

		yield return new WaitForSeconds(1.0f);

		//turn on the eye controller
		if(eyeCtrl != null)
			eyeCtrl.enabled = true;


		foreach(GameObject obj in objsToShowWhenTouched)
		{
			obj.SetActive(false);
		}

		foreach(GameObject obj in objsToHideWhenTouched)
		{
			obj.SetActive(true);
		}
	}
	#endregion
}
