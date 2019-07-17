using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//入力操作を司る
public class OVRInputManager : InputManager
{


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


	//public bool Moved(){
	//    return moved;
	//}
}