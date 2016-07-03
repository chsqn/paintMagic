using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour 
{

	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public Vector3 	rotAxis;
	public float	rotSpeed = 5.0f;
	#endregion publicParameter

	
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember

	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Update()
	{
		this.transform.RotateAround(this.transform.position, rotAxis, rotSpeed * Time.deltaTime);
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
