using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OVRCamerarigにアタッチされているスクリプト
//入力を受け取ったら対応した動作をする
//相手のパイにぶつかったらぶつかった箇所によって対応した処理をする→未実装
//public enum Hand
//{
//	right,
//	left
//}

public class Player : OVRPlayerController
{
	CharacterStatus status;
	public OVRGrabber right;
	public OVRGrabber left;
	//public HandController controller;
	//InputManager inputManager;

	public override void Start()
	{
		base.Start();
		Acceleration = status.acceralation;
		Damping = status.damp;
		if(right!=null){
			right.throwSpeed = status.throwSpeed;
		}
		if (left != null)
		{
			left.throwSpeed = status.throwSpeed;
		}
		//controller = GetComponent<HandController>();
		//inputManager = GetComponent<InputManager>();
	}

	public override void Awake()
	{
		base.Awake();
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}

	public override void Update()
	{
		Acceleration = status.acceralation;
		Damping = status.damp;
		base.Update();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "Pie"){
			float mass = status.mass;
			mass += 5.0f+10.0f*(status.comb - 1);
			status.mass = mass;
			//status.isHit = true;
		}
	}
}
