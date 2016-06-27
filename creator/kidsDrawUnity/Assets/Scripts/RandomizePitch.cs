using UnityEngine;
using System.Collections;

public class RandomizePitch : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public float randValue = 0.0f;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private AudioSource mAudioSource;

	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		mAudioSource = GetComponent<AudioSource>();
	}

	void OnEnable()
	{
		mAudioSource.pitch = 1.0f + (Random.Range(-randValue, randValue));
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
