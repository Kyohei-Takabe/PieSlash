using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	//右手で生成するとtrue
	protected bool isCreatedR = false;
	//左手で生成するとfalse
	protected bool isCreatedL = false;
	//右手でパイを持っているときにtrue
	protected bool isHavingR = false;
	//左手でパイを持っているときにtrue
	protected bool isHavingL = false;
	//右手で投げた瞬間true
	protected bool isThrowingR = false;
	//左手で投げた瞬間true
	protected bool isThrowingL = false;

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

}
