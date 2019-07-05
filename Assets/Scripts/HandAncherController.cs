using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//手のアンカーにアタッチするクラス
//アンカーにしてほしい処理が書かれている
public class HandAncherController : MonoBehaviour
{
	GameObject pie = null;
	PieGenerator generator;
	PieThrower thrower;
	//Vector3[] prePositions = new Vector3[10];
	bool hasPie = false;

	//Vector3 prePosition;
	//public float speed;

	void Start()
	{
		generator = GetComponent<PieGenerator>();
		thrower = GetComponent<PieThrower>();
		if (generator == null)
		{
			Debug.Log("generator is null");
		}
		if (thrower == null)
		{
			Debug.Log("thrower is null");
		}
	}

	void Update()
	{
		thrower.SetPosition(transform.position);

	}

	// パイを生成する
	public void CreatPie()
	{
		if (!hasPie && pie == null)
		{
			hasPie = true;
			pie = generator.Generate(transform);
		}
	}

	//パイを持っている状態(パイを生成してまだ投げていない状態のこと)
	public void HavingPie()
	{
		pie.transform.position = transform.position;
		pie.transform.rotation = transform.rotation;
	}

	//パイを投げる
	public void ThrowPie()
	{
		if (hasPie && pie != null)
		{
			hasPie = false;
			thrower.ThrowPie(pie);
			pie = null;
		}

	}
}
