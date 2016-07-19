using UnityEngine;
using System.Collections;

public class DEBUG_JellySprite : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public JellySprite jellySprite;
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
		if(Input.GetKeyUp(KeyCode.B))
		{
			print("now adding force");
			jellySprite.AddForceAtPosition(Vector2.up * 6000.0f, new Vector2(this.transform.position.x - 40.0f, this.transform.position.y));
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

	#endregion
}
