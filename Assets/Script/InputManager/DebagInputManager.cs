using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebagInputManager : InputManager
{
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.D))
		{
			Debug.Log("右人差し指トリガーを押した");
			if (isThrowingR)
			{
				isThrowingR = false;
			}
			isCreatedR = true;
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log("左人差し指トリガーを押した");
			if (isThrowingL)
			{
				isThrowingL = false;
			}
			isCreatedL = true;
		}

		if (Input.GetKey(KeyCode.D))
		{
			Debug.Log("右人差し指トリガーを押したまま");
			isHavingR = true;
		}

		if (Input.GetKey(KeyCode.A))
		{
			isHavingL = true;
		}

		if (Input.GetKeyUp(KeyCode.D) && isCreatedR)
		{
			//右手の位置からパイを投げる
			Debug.Log("右人差し指トリガーを離した");
			isHavingR = false;
			isThrowingR = true;
			isCreatedR = false;
		}

		if (Input.GetKeyUp(KeyCode.A) && isCreatedL)
		{
			//左手の位置からパイを投げる
			Debug.Log("左人差し指トリガーを離した");
			isHavingL = false;
			isThrowingL = true;
			isCreatedL = false;
		}
	}
}
