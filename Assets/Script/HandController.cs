using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Hand
{
	right,
	left
}

//手のアンカーにアタッチするクラス
//アンカーにしてほしい処理が書かれている
public class HandController : MonoBehaviour
{
	GameObject pieObject = null;

	PieGenerator generator;
	PieThrower thrower;
	//OVRGrabber grabber;
	public OVRInput.Controller m_controller;
	//public Hand hand;
	//現在から11フレーム前までの位置ベクトル
	//prePositions[0]が最新の位置ベクトル
	//OVRPose[] localPose = new OVRPose[11];
	//Vector3[] prePositions = new Vector3[11];
	//Quaternion[] preRot = new Quaternion[10];
	//手とオブジェクトの
	//Vector3 objectPosOffset = new Vector3(0.1f, 0, 0);
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
		//for (int i = 0; i < localPose.Length; i++)
		//{
		//	localPose[i] = new OVRPose { position = Vector3.zero, orientation = Quaternion.identity };
		//}
	}

	void Update()
	{

		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, m_controller))
		{
			CreatePie();
		}

		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, m_controller)){
			thrower.MoveGrabbedObject(transform.position, transform.rotation, true);
		}

		if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, m_controller))
		{
			if (ishaving)
			{
				ishaving = false;
				thrower.GrabEnd();
			}
		}
	}

	public void CreatePie()
	{
		if (!ishaving){
			//pie = generator.Generate();

			pieObject = generator.Generate(transform, m_controller);
			Pie pie = pieObject.GetComponent<Pie>();
			ishaving = true;
			thrower.GrabBegin(pie);
		}
	}
}
