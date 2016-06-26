using UnityEngine;
using System.Collections;

public class DEBUG_drawCube : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public Color 	inputcolor;
	public float	size;

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

	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	void OnDrawGizmos() 
	{
		Gizmos.color = inputcolor;
		Gizmos.DrawCube(transform.position, new Vector3(size,size,size));
	}
	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion


}
