using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//手のアンカーにアタッチするクラス
//アンカーにしてほしい処理が書かれている
public class HandController : MonoBehaviour
{
	GameObject pie = null;

	PieGenerator generator;
	PieThrower thrower;
	//OVRGrabber grabber;
	[SerializeField]
	protected OVRInput.Controller m_controller;
	//現在から11フレーム前までの位置ベクトル
	//prePositions[0]が最新の位置ベクトル
	OVRPose[] localPose = new OVRPose[11];
	//Vector3[] prePositions = new Vector3[11];
	//Quaternion[] preRot = new Quaternion[10];
	//手とオブジェクトの
	Vector3 objectPosOffset = new Vector3(0.1f, 0, 0);
	//Vector3 objectRotateOffset = new Vector3(0,0,90);

	bool ishaving = false;

	//public float pieSpeed = 100.0f;

	void Start()
	{
		generator = GetComponent<PieGenerator>();
		thrower = GetComponent<PieThrower>();
		//grabber = GetComponent<OVRGrabber>();
		if (generator == null)
		{
			Debug.Log("generator is null");
		}
		if (thrower == null)
		{
			Debug.Log("thrower is null");
		}

		//とりあえず0で初期化
		//for (int i = 0; i < prePositions.Length; i++)
		//{
		//	prePositions[i] = Vector3.zero;
		//}
		for (int i = 0; i < localPose.Length; i++)
		{
			localPose[i] = new OVRPose { position = Vector3.zero, orientation = Quaternion.identity };
		}
	}

	void Update()
	{

	}

	void SetLocalPose(OVRPose lastPose){
		for (int i = 0; i < localPose.Length - 1; i++)
		{
			localPose[i + 1] = localPose[i];
		}
		localPose[0] = lastPose;
	}

	// パイを生成する

	public void CreatePie(Hand hand)
	{
		if(!ishaving){
			//pie = generator.Generate();
			pie = generator.Generate(transform, hand);

			ishaving = true;
		}
	}

	//パイを持っている状態(パイを生成してまだ投げていない状態のこと)
	public void HavingPie(Hand hand)
	{
		if(ishaving){
			pie.transform.position = transform.position;
			pie.transform.rotation = transform.rotation;
			if (hand == Hand.right)
			{
				pie.transform.Translate(-objectPosOffset);
			}
			if (hand == Hand.left)
			{
				pie.transform.Translate(objectPosOffset);
			}

			//pie.transform.Rotate(objectRotateOffset);

			//SetPosition(transform.localPosition);
			//SetRotate(transform.localRotation);
			//SetLocalPose(new OVRPose { position = transform.localPosition, orientation = transform.localRotation });
		}
	}

	//パイを投げる
	public void ThrowPie(float speed)
	{
		if(ishaving)
		{
			ishaving = false;
			pie.transform.parent = null;
			//grabber.ForceRelease(pie.GetComponent<OVRGrabbable>());
			//thrower.ThrowPie(pie.GetComponent<Pie>(), prePositions, preRot,speed);

			////コントローラの位置情報を初期化
			//for (int i = 0; i < prePositions.Length; i++)
			//{
			//	prePositions[i] = Vector3.zero;
			//}

			thrower.ThrowPie(m_controller,pie.GetComponent<Pie>(), localPose, speed);

			for (int i = 0; i < localPose.Length; i++)
			{
				localPose[i].position = Vector3.zero;
				localPose[i].orientation = Quaternion.identity;
			}
		}
	}

	public Vector3 GetControlerPose()
	{
		return localPose[0].position;
	}

	public Quaternion GetControlerRot()
	{
		return localPose[0].orientation;
	}
	
}
