using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheKnightsOfUnity.LineRendererPro;

public class DrawingLine : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	[Header("-----SETUP----------")]
	public Material				lineMaterial;
	public List<LinePoint>		allPoints;
	public List<GameObject>		allObjectsToShow;
	public float				addPitch = 0.2f;

	[Header("-----SETTINGS-------")]
	public float				maxDistanceToSpline = 20.0f;
	[Range(0.5f, 10.0f)]
	public float				lineWidth = 4.0f;
	//public List<GameObject>		objectsToShow;
	//public List<GameObject>		objectsToHide;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	public int				mLastPointID 	= 0;
	private LevelManager	mLevelManager;
	private bool			mIsDrawing 		= false;
	private bool			mHasError 		= false;

	private LineRenderer mLineRenderer;

	private int				mDrawingDirection = 1;
	private float			mCurrentPitch = 1.0f;

	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		this.transform.parent.gameObject.GetComponent<LevelManager>();

		mLineRenderer = GetComponent<LineRenderer>();
		mLevelManager = GameObject.FindObjectOfType<LevelManager>();
	}

	void OnEnable()
	{
		//we create the line again on start which sets the correct ids at the points
		createLine();
	}

	void Update()
	{
		//only do something if we are drawing
		if(mIsDrawing == false)
			return;

		//if we have a error we don't do anything
		if(mHasError == true)
			return;

		//this will turn on mHasError if the drawing distance is to far
		checkLineDistance();

		if(Input.GetMouseButtonUp(0))
		{
			resetDrawing();

			foreach(LinePoint p in allPoints)
			{
				p.turnOff();
			}
		}
			
	}

	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	/// <summary>
	/// Called when the "createPoints" button was pressed
	/// </summary>
	public void createLine()
	{
		int indexCounter = 0;
		mLineRenderer = GetComponent<LineRenderer>();

		//set the material
		mLineRenderer.material = lineMaterial;

		//empty all the current linepoints first
		allPoints.Clear();

		//get all the line points
		LinePoint[] allCurrentLinePoints = GetComponentsInChildren<LinePoint>();
		foreach(LinePoint lp in allCurrentLinePoints)
			allPoints.Add(lp);

		//first step through the line and set the vertex count
		mLineRenderer.SetVertexCount(allPoints.Count);


		//set all the line renderer positions
		for(int i = 0; i < allPoints.Count; i++)
		{
			mLineRenderer.SetPosition(i, new Vector3(allPoints[i].transform.position.x, 
													 allPoints[i].transform.position.y, 
													 allPoints[i].transform.position.z + 0.2f));
			allPoints[i].idText.text 	= (i + 1).ToString();
			allPoints[i].ID				= i+1;
			allPoints[i].mDrawingLine 	= this;
		}

		//set the line width
		mLineRenderer.SetWidth(lineWidth, lineWidth);

	}

	/// <summary>
	/// Called when the mouse is inside a point
	/// </summary>
	public void pointTouched(LinePoint point)
	{

		//don't do anything if we have a error
		if(mHasError)
			return;

		//ONLY continue if the mouse is down
		if(Input.GetMouseButton(0) == false)
			return;

		//don't do anything if the point has been touched already
		if(point.currentPointState == LinePoint.pointState.isOn)
			return;

		//if we are not drawing already we get the drawing direction
		if(mIsDrawing == false)
		{
			mLastPointID 		= 0;
			mDrawingDirection 	= 1;

			// THIS PART IS FOR FORWARD/BACKWARD TREATMENT
			/*
			if(point.ID == 1)
			{
				mLastPointID 		= 0;
				mDrawingDirection 	= 1;
			}

			if(point.ID == allPoints.Count)
			{
				mLastPointID	  = allPoints.Count + 1;
				mDrawingDirection = -1;
			}
			*/
		}

		//if the provided ID is not +1 then exit
		if(point.ID != mLastPointID + mDrawingDirection )
		{
			print("call error because mlastPointid is " + mLastPointID);
			callError();
			return;

		}

		mCurrentPitch += addPitch;
		mLevelManager.audioSource.pitch = mCurrentPitch;
		mLevelManager.audioSource.PlayOneShot(mLevelManager.audioPointTurnOnSFX);


		//turn the point on


		//turn on is drawing
		mIsDrawing = true;

		//onlie add to the lastPointID IF the selected point isn't the same
		if(point.ID != mLastPointID)
			mLastPointID = mLastPointID + mDrawingDirection;

		//if we have the last point or the first point drawn we unlock the image
		if(mLastPointID + mDrawingDirection > allPoints.Count || 
			mLastPointID + mDrawingDirection <= 0)
		{
			point.turnOn(true);
			mLevelManager.unlockNext(this);
			resetDrawing();
		} else
		{
			point.turnOn();
		}
	}





	/// <summary>
	/// Called when the mouse is inside the collider for the drawing line
	/// We check the distance to the line here
	/// </summary>
	public void checkLineDistance()
	{
		//dn't do anything if we are not drawing
		if(mIsDrawing ==  false)
			return;

		//don't do anything if we have a error
		if(mHasError)
			return;
		
		//get a ray
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//shot a ray onto the plane
		RaycastHit rayHit;

		if(Physics.Raycast(ray, out rayHit, 9999999.9f))
		{
			//call Error if he hit object wasn't this
			if(rayHit.collider.gameObject != this.gameObject &&
				rayHit.collider.gameObject.name.Contains("point") == false)
			{
				print("now calling error because we touch collider " + rayHit.collider.name);
				callError();
			}

			/*
			float nearestTF = spline.GetNearestPointTF(rayHit.point);

			float distToSpline = (spline.transform.TransformPoint(spline.Interpolate(nearestTF)) - rayHit.point).magnitude;

			if(distToSpline > maxDistanceToSpline)
			{
				print("ERROR: dist to spline is " + distToSpline);
				callError();
			}
			*/
		} else
		{
			print("now calling error because we didn't touch anything");
			//if we didn't hit anything we call error
			callError();
		}
	

	}



	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI
	void callError()
	{
		mLevelManager.addErrorValue(1);
		mHasError = true;
		StartCoroutine(blinkPoints());
	}

	IEnumerator blinkPoints()
	{
		int counter = 0;
		yield return new WaitForSeconds(0.1f);
		turnAllPoints(false);
		yield return new WaitForSeconds(0.1f);
		turnAllPoints(true);
		yield return new WaitForSeconds(0.1f);
		turnAllPoints(false);
		yield return new WaitForSeconds(0.1f);
		turnAllPoints(true);
		yield return new WaitForSeconds(0.1f);
		turnAllPoints(false);
		yield return new WaitForSeconds(0.1f);
		turnAllPoints(true);
		yield return new WaitForSeconds(0.1f);
		turnAllPoints(false);
		yield return new WaitForSeconds(0.1f);
		turnAllPoints(true);


		//can draw again
		resetDrawing();



	}

	void turnAllPoints(bool on)
	{
		foreach(LinePoint p in allPoints)
		{
			if(on == false)
			{
				p.turnError();
			} else
			{
				p.turnOff();
			}
		}
	}

	void resetDrawing()
	{
		mIsDrawing 		= false;
		mLastPointID 	= 0;
		mHasError		= false;
		mCurrentPitch 	= 1.0f;
	}
	#endregion

}
