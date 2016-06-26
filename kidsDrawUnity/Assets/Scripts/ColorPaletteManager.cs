using UnityEngine;
using System.Collections;

public class ColorPaletteManager : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	public static ColorPaletteManager Instance { get; private set; }

	#region publicParameter
	public Color[] paletteColors;

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
	void Awake()
	{
		////////////////////////////////////////////////////////////////////
		/// SINGLETON //////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////
		// First we check if there are any other instances conflicting
		if(Instance != null && Instance != this)
		{
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}

		// Here we save our singleton instance
		Instance = this;


		////////////////////////////////////////////////////////////////////
		/// SINGLETON //////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////



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
