using UnityEngine;
using System.Collections;

public class ButterflyCtrl : MonoBehaviour 
{
	// -----------------------------
	//	public parameter
	// -----------------------------

	#region publicParameter
	public ParticleSystem	touchedPSystem;
	public Transform 		targetPos;
	public Transform		moverTrans;
	public Animator  		butterflyAnim;
	public SpriteRenderer	butterFlySprite;
	public AnimationCurve 	posCurveX;
	public AnimationCurve 	posCurveY;
	public AnimationCurve	touchedCurve;
	public float			touchedPower = 200.0f;
	public float			touchedAnimSpeed = 2.0f;
	public float			animSpeed = 1.0f;

	#endregion publicParameter

		
	// -----------------------------
	//	private datamember
	// -----------------------------

	#region privateMember
	private Vector3 	mSourcePos;
	private float		mLerpValue 	= 0.0f;
	private bool		mIsFlying 	= false;
	private bool		mIsTouched  = false;
	private Vector3     mTouchedPos;
	private float		mCurrentDist;
	private float		mCurrentDistMult;

	private Vector3		mOldPos;
	private Vector3		mNewPos;

	private int			mID_TRIGGER_Fly = Animator.StringToHash("TRIGGER_Fly");
	private int			mID_TRIGGER_idle = Animator.StringToHash("TRIGGER_Idle");


	#endregion privateMember


	// -----------------------------
	//	monobehaviour
	// -----------------------------

	#region MonoBehaviour
	void OnEnable()
	{
		//get a random color
		butterFlySprite.color = ColorPaletteManager.Instance.paletteColors[Random.Range(0, ColorPaletteManager.Instance.paletteColors.Length)];

		//get the sourcePos
		mSourcePos = this.transform.position;	

		//start flying
		mLerpValue 			= 0.0f;
		mIsFlying 			= true;
		mCurrentDist 		= (this.transform.position - targetPos.position).magnitude;
		mCurrentDistMult 	= mCurrentDist / 1000.0f;

		butterflyAnim.SetTrigger(mID_TRIGGER_Fly);

	}

	void Update()
	{
		//if we are flying we move to the sourcePos
		if(mIsFlying)
		{
			mOldPos = moverTrans.position;
			mLerpValue += (Time.deltaTime * animSpeed * mCurrentDistMult);

			moverTrans.position = new Vector3(Mathfx.UnclampedLerp(mSourcePos.x, targetPos.position.x, posCurveX.Evaluate(mLerpValue)),
											  Mathfx.UnclampedLerp(mSourcePos.y, targetPos.position.y, posCurveY.Evaluate(mLerpValue)),
											  moverTrans.position.z);			


			if(mLerpValue >= 1.0f)
			{
				mLerpValue 	= 0.0f;
				mIsFlying 	= false;
				butterflyAnim.SetTrigger(mID_TRIGGER_idle);
			}
				
		}

		if(mIsTouched)
		{
			mLerpValue += (Time.deltaTime * touchedAnimSpeed);

			moverTrans.position = new Vector3(mTouchedPos.x,
				mTouchedPos.y + touchedCurve.Evaluate(mLerpValue) * touchedPower,
				moverTrans.position.z);

			if(mLerpValue >= 1.0f)
			{
				mLerpValue 	= 0.0f;
				mIsTouched 	= false;
				butterflyAnim.SetTrigger(mID_TRIGGER_idle);
			}
		}
	}

	void LateUpdate()
	{
		mNewPos = moverTrans.position;

		if(mNewPos.x - mOldPos.x < 0.0f) 
		{
			butterflyAnim.transform.localScale = new Vector3(-Mathf.Abs(butterflyAnim.transform.localScale.x),
				butterflyAnim.transform.localScale.y, butterflyAnim.transform.localScale.z);
		} else
		{
			butterflyAnim.transform.localScale = new Vector3(butterflyAnim.transform.localScale.x,
				butterflyAnim.transform.localScale.y, butterflyAnim.transform.localScale.z);
			
		}
	}

	#endregion MonoBehaviour

		
	// -----------------------------
	//	public api
	// -----------------------------

	#region publicAPI
	public void touched()
	{
		if(mIsFlying)
			return;

		if(mIsTouched)
			return;

		Instantiate(touchedPSystem.gameObject, moverTrans.position, touchedPSystem.transform.rotation);

		mIsTouched = true;
		mLerpValue =  0.0f;
		mTouchedPos = moverTrans.position;

		butterflyAnim.SetTrigger(mID_TRIGGER_Fly);
	}

	#endregion

		
	// -----------------------------
	//	private api
	// -----------------------------

	#region privateAPI

	#endregion
}
