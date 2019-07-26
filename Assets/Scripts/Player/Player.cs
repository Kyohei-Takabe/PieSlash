using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OVRCamerarigにアタッチされているスクリプト
//入力を受け取ったら対応した動作をする
//相手のパイにぶつかったらぶつかった箇所によって対応した処理をする→未実装
public enum Hand
{
	right,
	left
}

public class Player : MonoBehaviour
{
	private InputManager inputManager;
	public HandController rightHand;
	public HandController leftHand;
	CharacterStatus status;

	//public float walkSpeed;
	//player,cameraのtransformを持つ
	Rigidbody rig;

	// Use this for initialization
	public void Start()
	{
		inputManager = FindObjectOfType<OVRInputManager>();
		if (inputManager == null)
		{
			Debug.Log("inputManager is null");
		}
		rig = GetComponent<Rigidbody>();

		if (rightHand == null)
		{
			Debug.Log("rightHand is null");
		}

		if (leftHand == null)
		{
			Debug.Log("leftHand is null");
		}
		status = GetComponent<CharacterStatus>();
	}

	// Update is called once per frame
	void Update()
	{
		if (inputManager.CreatedR())
		{
			//右手アンカーの位置にパイを生成する関数
			rightHand.CreatePie(Hand.right);
		}

		if (inputManager.CreatedL())
		{
			//左手アンカーの位置にパイを生成する関数
			leftHand.CreatePie(Hand.left);
		}

		if (inputManager.HavingR())
		{
			rightHand.HavingPie(Hand.right);
		}

		if (inputManager.HavingL())
		{
			　leftHand.HavingPie(Hand.left);
		}

		if (inputManager.ThrowingR())
		{
			rightHand.ThrowPie(status.throwSpeed);
		}

		if (inputManager.ThrowingL())
		{
			leftHand.ThrowPie(status.throwSpeed);
		}


	}

	public void OnCollisionEnter(Collision collision)
	{


	}

}
