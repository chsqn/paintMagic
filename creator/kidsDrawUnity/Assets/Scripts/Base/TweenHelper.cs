using UnityEngine;
using System.Collections;

public class TweenHelper : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter

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
	public static void punchScale(GameObject inputObject, Vector3 amount, float time, string onCompleteCall, GameObject objectCaller)
	{
		iTween.PunchScale(inputObject, iTween.Hash("amount", amount, "time", time, "onComplete", onCompleteCall, "onCompleteTarget", objectCaller));
	}


	public static void rotateAdd(GameObject objectToRotate, Vector3 rotAmount, float rotTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		iTween.RotateAdd(objectToRotate, iTween.Hash("amount", rotAmount, "time", rotTime, "easeType", inputEasyType, "onComplete", onCompleteCall, "onCompleteTarget", objectCaller));
	}

	public static void scaleLocal(GameObject menuToScale, Vector3 scaleAmount, float scaleTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		iTween.ScaleTo(menuToScale, iTween.Hash("scale", scaleAmount, "islocal", true, "time", scaleTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}

	public static void scaleWorld(GameObject menuToScale, Vector3 scaleAmount, float scaleTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		iTween.ScaleTo(menuToScale, iTween.Hash("scale", scaleAmount, "islocal", false, "time", scaleTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}

	public static void moveLocal(GameObject menuToMove, Vector3 position, float moveTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		iTween.MoveTo(menuToMove, iTween.Hash("position", position, "islocal", true, "time", moveTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}

	public static void moveWorld(GameObject menuToMove, Vector3 position, float moveTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		iTween.MoveTo(menuToMove, iTween.Hash("position", position, "islocal", false, "time", moveTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}
		

	public static void unhideAndScale(GameObject inputObj, Vector3 scaleFrom, Vector3 scaleTo, float scaleTime, iTween.EaseType inputEasyType, string onCompleteCall, GameObject objectCaller)
	{
		inputObj.SetActive(true);
		inputObj.transform.localScale = scaleFrom;
		iTween.ScaleTo(inputObj, iTween.Hash("scale", scaleTo, "islocal", true, "time", scaleTime, "easeType", inputEasyType, "ignoretimescale", true, "oncomplete",onCompleteCall, "onCompleteTarget", objectCaller));
	}

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
