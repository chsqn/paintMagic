using UnityEngine;
using System.Collections;

public class TriggerTouchedOnAllAnimations : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public Animator[] allAnimators;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private int 	mTriggerTouched = Animator.StringToHash("touched");

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
	public void touched()
	{
		foreach(Animator anim in allAnimators)
		{
			anim.SetTrigger(mTriggerTouched);
		}
	}

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
