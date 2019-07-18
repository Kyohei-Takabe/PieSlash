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

	//現在から11フレーム前までの位置ベクトル
	//prePositions[0]が最新の位置ベクトル
	Vector3[] prePositions = new Vector3[11];

	public float pieSpeed;
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

		//とりあえず0で初期化
		for (int i = 0; i < prePositions.Length; i++)
		{
			prePositions[i] = Vector3.zero;
		}
	}

	void Update()
	{

	}

	//pos情報をsetする
	public void SetPosition(Vector3 lastFramePos)
	{
		for (int i = 1; i < prePositions.Length; i++)
		{
			prePositions[i] = prePositions[i - 1];
		}

		prePositions[0] = lastFramePos;
	}

	// パイを生成する
	public void CreatPie(Hand hand)
	{
		if (!hasPie)
		{
			hasPie = true;
			pie = generator.Generate(transform,hand);
		}
	}

	//パイを持っている状態(パイを生成してまだ投げていない状態のこと)
	public void HavingPie(Hand hand)
	{
		pie.transform.position = transform.position;
		pie.transform.rotation = transform.rotation;
		//Vector3 size = transform.localScale;
		//pie.transform.Translate(size.x,0,0);
		if(hand == Hand.right){
			pie.transform.Translate(-0.05f, 0, 0);
		}
		if(hand == Hand.left){
			pie.transform.Translate(0.05f, 0, 0);
		}

		pie.transform.Rotate(new Vector3(0, 0, 90));
		SetPosition(transform.position);
	}

	//パイを投げる
	public void ThrowPie()
	{
		if (hasPie)
		{
			//Pie pieScript = pie.GetComponent<Pie>();
			hasPie = false;
			thrower.ThrowPie(pie.GetComponent<Pie>(),prePositions,pieSpeed);

			//コントローラの位置情報を初期化
			for (int i = 0; i < prePositions.Length; i++)
			{
				prePositions[i] = Vector3.zero;
			}
			//投げたらそのパイをこの実装と切り離したい

		}

	}
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//手のアンカーにアタッチするクラス
//アンカーにしてほしい処理が書かれている
public class HandController : MonoBehaviour
{
	GameObject pie = null;

	PieGenerator generator;
	PieThrower thrower;

	//現在から11フレーム前までの位置ベクトル
	//prePositions[0]が最新の位置ベクトル
	Vector3[] prePositions = new Vector3[11];

	bool ishaving = false;

	public float pieSpeed = 1.0f;

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

		//とりあえず0で初期化
		for (int i = 0; i < prePositions.Length; i++)
		{
			prePositions[i] = Vector3.zero;
		}
	}

	void Update()
	{

	}

	//pos情報をsetする
	public void SetPosition(Vector3 lastFramePos)
	{
		for (int i = 0; i < prePositions.Length - 1; i++)
		{
			prePositions[i+1] = prePositions[i];
		}

		prePositions[0] = lastFramePos;
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
			//Vector3 size = transform.localScale;
			//pie.transform.Translate(size.x,0,0);
			if (hand == Hand.right)
			{
				pie.transform.Translate(-0.05f, 0, 0);
			}
			if (hand == Hand.left)
			{
				pie.transform.Translate(0.05f, 0, 0);
			}

			pie.transform.Rotate(new Vector3(0, 0, 90));
			SetPosition(transform.position);
		}
	}

	//パイを投げる
	public void ThrowPie()
	{
		if(ishaving){
			ishaving = false;
			pie.transform.parent = null;
			thrower.ThrowPie(pie.GetComponent<Pie>(), prePositions, pieSpeed);

			//コントローラの位置情報を初期化
			for (int i = 0; i < prePositions.Length; i++)
			{
				prePositions[i] = Vector3.zero;
			}
		}
	}
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//手のアンカーにアタッチするクラス
//アンカーにしてほしい処理が書かれている
public class HandController : MonoBehaviour
{
	GameObject pie = null;

	PieGenerator generator;
	PieThrower thrower;

	//現在から11フレーム前までの位置ベクトル
	//prePositions[0]が最新の位置ベクトル
	Vector3[] prePositions = new Vector3[11];

	bool ishaving = false;

	public float pieSpeed = 1.0f;

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

		//とりあえず0で初期化
		for (int i = 0; i < prePositions.Length; i++)
		{
			prePositions[i] = Vector3.zero;
		}
	}

	void Update()
	{

	}

	//pos情報をsetする
	public void SetPosition(Vector3 lastFramePos)
	{
		for (int i = 0; i < prePositions.Length - 1; i++)
		{
			prePositions[i+1] = prePositions[i];
		}

		prePositions[0] = lastFramePos;
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
			//Vector3 size = transform.localScale;
			//pie.transform.Translate(size.x,0,0);
			if (hand == Hand.right)
			{
				pie.transform.Translate(-0.05f, 0, 0);
			}
			if (hand == Hand.left)
			{
				pie.transform.Translate(0.05f, 0, 0);
			}

			pie.transform.Rotate(new Vector3(0, 0, 90));
			SetPosition(transform.position);
		}
	}

	//パイを投げる
	public void ThrowPie()
	{
		if(ishaving){
			ishaving = false;
			pie.transform.parent = null;
			thrower.ThrowPie(pie.GetComponent<Pie>(), prePositions, pieSpeed);

			//コントローラの位置情報を初期化
			for (int i = 0; i < prePositions.Length; i++)
			{
				prePositions[i] = Vector3.zero;
			}
		}
	}
}
