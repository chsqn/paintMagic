using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Medvedya.SpriteDeformerTools;

public class AttachToPoints : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public SpriteDeformerAnimation spriteDeformerAnimation;
	public string pointName;


	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember

	private List<SpritePoint> 		mPointsToFollow 		= new List<SpritePoint>();
	private Vector3					mOffset;
	private Vector3					mCenterPos;

	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Start()
	{
		//get all the points
		foreach (SpritePoint point in spriteDeformerAnimation.points)
		{
			if (point.name == pointName)
			{
				mPointsToFollow.Add(point);
			}
		}

		//get the center pos from all the points
		mCenterPos = getCenterPos(mPointsToFollow);

		//get the offset
		mOffset = mCenterPos - this.transform.position;
	}

	void Update()
	{
		mCenterPos = getCenterPos(mPointsToFollow);

		this.transform.position = mCenterPos - mOffset;

		spriteDeformerAnimation.dirty_offset = true;
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
	Vector3 getCenterPos(List<SpritePoint> pointList)
	{
		float 	counter 	= 0.0f;
		Vector3 returnValue = Vector3.zero;

		foreach(SpritePoint point in pointList)
		{
			returnValue += spriteDeformerAnimation.SpritePositionToGlobal(point.spritePosition + point.offset2d);
			counter += 1.0f;
		}

		return returnValue / counter;
	}

	#endregion
}
