using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public GameObject objectToSpawn;

	public float 		spawnRateMin = 1.0f;
	public float 		spawnRateMax = 4.0f;
	public float		minScale = 0.5f;
	public float		maxScale = 1.5f;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private float 		mCurrentSpawnTime = 0.0f;
	private BoxCollider mColl;
	private Vector3		mRandomScale;
	private float		spawnRate;

	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void Awake()
	{
		mColl = GetComponent<BoxCollider>();
	}


	void Update()
	{
		if(Time.time > mCurrentSpawnTime + spawnRate)
		{
			spawnObject();
			mCurrentSpawnTime = Time.time + spawnRate;
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
	void spawnObject()
	{
		Vector3 spawnPos = new Vector3(Random.Range(mColl.bounds.min.x, mColl.bounds.max.x),
			Random.Range(mColl.bounds.min.y, mColl.bounds.max.y),
			Random.Range(mColl.bounds.min.z, mColl.bounds.max.z));

		GameObject newObj = Instantiate(objectToSpawn, spawnPos, Quaternion.identity) as GameObject;

		float randScaleVal = Random.Range(minScale, maxScale);
		mRandomScale = new Vector3(randScaleVal, randScaleVal, randScaleVal);
		newObj.transform.localScale = mRandomScale;

		spawnRate = Random.Range(spawnRateMin, spawnRateMax);
	}

	#endregion
}
