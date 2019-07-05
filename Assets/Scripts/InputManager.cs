using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//入力操作を司る
public class InputManager : MonoBehaviour
{
	//右手で生成するとtrue
	bool isCreatedR = false;
	//左手で生成するとfalse
	bool isCreatedL = false;
	//右手でパイを持っているときにtrue
	bool isHavingR = false;
	//左手でパイを持っているときにtrue
	bool isHavingL = false;
	//右手で投げた瞬間true
	bool isThrowingR = false;
	//左手で投げた瞬間true
	bool isThrowingL = false;

	//bool moved;

	// Update is called once per frame
	void Update()
	{
		if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
		{
			Debug.Log("右人差し指トリガーを押した");
			if (isThrowingR)
			{
				isThrowingR = false;
			}
			isCreatedR = true;
		}

		if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
		{
			Debug.Log("左人差し指トリガーを押した");
			if (isThrowingL)
			{
				isThrowingL = false;
			}
			isCreatedL = true;
		}

		if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
		{
			Debug.Log("右人差し指トリガーを押したまま");
			isHavingR = true;
		}

		if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
		{
			isHavingL = true;
		}

		if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) && isCreatedR)
		{
			//右手の位置からパイを投げる
			Debug.Log("右人差し指トリガーを離した");
			isHavingR = false;
			isThrowingR = true;
			isCreatedR = false;
		}

		if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger) && isCreatedL)
		{
			//左手の位置からパイを投げる
			Debug.Log("左人差し指トリガーを離した");
			isHavingL = false;
			isThrowingL = true;
			isCreatedL = false;
		}
	}

	public bool CreatedR()
	{
		return isCreatedR;
	}

	public bool CreatedL()
	{
		return isCreatedL;
	}

	public bool HavingR()
	{
		return isHavingR;
	}

	public bool HavingL()
	{
		return isHavingL;
	}

	public bool ThrowingR()
	{
		return isThrowingR;
	}

	public bool ThrowingL()
	{
		return isThrowingL;
	}

	//public bool Moved(){
	//    return moved;
	//}
}