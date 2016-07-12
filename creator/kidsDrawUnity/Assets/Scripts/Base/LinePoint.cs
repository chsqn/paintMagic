using UnityEngine;
using System.Collections;

public class LinePoint : MonoBehaviour 
{


	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public int 				ID;
	public TextMesh			idText;
	public SpriteRenderer 	bgSprite;
	public GameObject		unlockEffect;
	public Color			onColor;
	public Color			offColor;
	public Color			errorColor = Color.red;
	public AudioSource		unlockSFX;

	public enum pointState
	{
		isOff 	= 0,
		isOn 	= 1,
		isError = 2
	};

	public pointState currentPointState;

	public DrawingLine mDrawingLine;

	#endregion publicParameter

	
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private Ray ray;
	private Collider coll;

	//shot a ray onto the plane
	RaycastHit rayHit;

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
		//set the point to be off at the start
		turnOff();
	}

	void Update()
	{
		if(Input.GetMouseButton(0))
		{
			//get a ray
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out rayHit, 9999999.9f))
			{
				//call Error if he hit object wasn't this
				if(rayHit.collider == coll)
				{
					pointTouched();
				}
			}
		}
	}

	#endregion MonoBehaviour

	
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	/// <summary>
	/// Called when the point was touched
	/// </summary>
	public void pointTouched()
	{
		//mDrawingLine = this.transform.parent.GetComponent<DrawingLine>();
		mDrawingLine.pointTouched(this);
	}


	public void turnOn(bool hasEffect = false)
	{
		bgSprite.color 		= onColor;
		currentPointState 	= pointState.isOn;
		unlockEffect.gameObject.SetActive(true);

		//scale up the point a bit and back
		TweenHelper.scaleLocal(this.gameObject, new Vector3(1.5f,1.5f,1.5f), 0.1f, iTween.EaseType.easeOutCubic, "scalePointBack", this.gameObject);

		if(hasEffect)
			Instantiate(unlockEffect, this.transform.position, Quaternion.identity);
	}

	void scalePointBack()
	{
		TweenHelper.scaleLocal(this.gameObject, Vector3.one, 0.1f, iTween.EaseType.easeInCubic, "none", this.gameObject);
	}

	public void turnOff()
	{
		bgSprite.color 		= offColor;
		currentPointState	= pointState.isOff;
		//unlockEffect.gameObject.SetActive(false);
	}

	public void turnError()
	{
		bgSprite.color 		= errorColor;
		currentPointState 	= pointState.isError;
	}

	#endregion

	
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
