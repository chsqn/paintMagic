using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurveManager : MonoBehaviour 
{

	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public List<DrawingLine> unlockOrder;

	#endregion publicParameter

	
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private int currentIndex = 0;
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
	/// <summary>
	/// Called when the user pressed the homebtn
	/// </summary>
	public void loadMainUI()
	{
		Application.LoadLevel("mainUI");
	}

	public void unlockNext(DrawingLine line)
	{
		//if we still have to show a curve we show the next
		if(currentIndex < unlockOrder.Count -1)
		{

			StartCoroutine(unlockNextAfter(line));

		} else
		{
			//here we would show the final unlock
			StartCoroutine(hideAfter(line));
		}
	}

	IEnumerator unlockNextAfter(DrawingLine line)
	{
		

		//hide the curve
		unlockOrder[currentIndex].gameObject.SetActive(false);
		//show the elements
		foreach(GameObject obj in line.allObjectsToShow)
			obj.SetActive(true);

		currentIndex += 1;

		//wait a bit before we show the next curve
		yield return new WaitForSeconds(1.0f);
		unlockOrder[currentIndex].gameObject.SetActive(true);
	}

	IEnumerator hideAfter(DrawingLine line)
	{
		yield return new WaitForSeconds(0.0f);
		unlockOrder[currentIndex].gameObject.SetActive(false);
		//show the elements
		foreach(GameObject obj in line.allObjectsToShow)
			obj.SetActive(true);
	}

	public void resetDrawingCurves()
	{
		foreach(DrawingLine line in unlockOrder)
		{
			line.gameObject.SetActive(false);

			foreach(GameObject obj in line.allObjectsToShow)
			{
				obj.SetActive(false);
			}

		}

		unlockOrder[0].gameObject.SetActive(true);

		currentIndex = 0;
	}
	#endregion

	
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
