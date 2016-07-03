using UnityEngine;
using System.Collections;

public class DestroyParticlesAfterDuration : MonoBehaviour 
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
	private ParticleSystem mPSystem;
	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		mPSystem = GetComponent<ParticleSystem>();
	}

	void OnEnable()
	{
		StartCoroutine(destroyAfter(mPSystem.duration));
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
	IEnumerator destroyAfter(float waitingTime)
	{
		yield return new WaitForSeconds(waitingTime);
		Destroy(this.gameObject);
	}

	#endregion
}
