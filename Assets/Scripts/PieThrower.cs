<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pieを投げる機能を持ったクラス
public class PieThrower : MonoBehaviour
{

	//速度ベクトルのうち信頼性の高いもの
	List<Vector3> adoptedVelocity = new List<Vector3>();

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	//pieを投げる関数
	public void ThrowPie(Pie pie, Vector3[] prePoses, float _speed)
	{
		Vector3 direction = CulculateDirection(prePoses);
		float speed = CulculateSpeed(prePoses,_speed);
		pie.Throwed(direction, speed);
	}

	//妥当な速度ベクトルのみを抽出する
	void ExtructedVelocity(Vector3[] prePositions)
	{
		//直近10フレームでの速度ベクトル
		Vector3[] velocitys = new Vector3[10];
		//前向きのベクトル
		List<Vector3> forwardVelocity = new List<Vector3>();
		//
		List<float> forwardVelocitySizes = new List<float>();
		List<float> highDeviation = new List<float>();

		//前向きのベクトルだけをListにする 
		for (int i = 0; i < velocitys.Length - 1; i++)
		{
			velocitys[i] = prePositions[i] - prePositions[i + 1];
			if (Vector3.Dot(velocitys[i], prePositions[0]) > 0)
			{
				forwardVelocity.Add(velocitys[i]);
			}
		}
		//Listにしたベクトルの大きさを求める
		for (int i = 0; i < forwardVelocity.Count; i++)
		{
			forwardVelocitySizes.Add(forwardVelocity[i].magnitude);
		}
		//Listにしたベクトルの大きさから偏差値を求め，偏差値60以上を採用する
		for (int i = 0; i < forwardVelocity.Count; i++)
		{
			highDeviation.Add(MathmaticsCulculater.CulculateDeviationValue(forwardVelocitySizes[i],
																		   forwardVelocitySizes));
			if (highDeviation[i] > 60)
			{
				adoptedVelocity.Add(forwardVelocity[i]);
			}
		}

		if(adoptedVelocity == null){
			adoptedVelocity.Add(new Vector3(0, 0, 0));
		}
	}

	//pieの飛んでく方向を計算する
	Vector3 ThrowPieDirection(Vector3[] prePoses)
	{
		//妥当なベクトルの選定
		ExtructedVelocity(prePoses);
		//MathmaticsCulculater.RowPathFilter();
		//各ベクトルの最小二乗平面に最後のベクトルを射影したベクトル
		Vector3 direction = MathmaticsCulculater.VectorProjectionToLeastSquaresPlane(adoptedVelocity);

		//テストコード
		//Vector3 direction = prePositions[0] - prePositions[1];
		return direction;
	}

	//方向の計算
	Vector3 CulculateDirection(Vector3[] prePoses)
	{
		Vector3 throwingVector = ThrowPieDirection(prePoses);
		return throwingVector.normalized;
	}

	//速度の計算
	float CulculateSpeed(Vector3[] prePoses, float speed)
	{
		Vector3 throwingVector = ThrowPieDirection(prePoses);
		return throwingVector.magnitude * speed/ Time.deltaTime;
	}
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pieを投げる機能を持ったクラス
public class PieThrower : MonoBehaviour
{

	//速度ベクトルのうち信頼性の高いもの
	List<Vector3> adoptedVelocity = new List<Vector3>();

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	//pieを投げる関数
	public void ThrowPie(Pie pie, Vector3[] prePoses, float _speed)
	{
		Vector3 direction = CulculateDirection(prePoses);
		float speed = CulculateSpeed(prePoses,_speed);
		pie.Throwed(direction, speed);
	}

	//妥当な速度ベクトルのみを抽出する
	void ExtructedVelocity(Vector3[] prePositions)
	{
		//直近10フレームでの速度ベクトル
		Vector3[] velocitys = new Vector3[10];
		//前向きのベクトル
		List<Vector3> forwardVelocity = new List<Vector3>();
		//
		List<float> forwardVelocitySizes = new List<float>();
		List<float> highDeviation = new List<float>();

		//前向きのベクトルだけをListにする 
		for (int i = 0; i < velocitys.Length - 1; i++)
		{
			velocitys[i] = prePositions[i] - prePositions[i+1];
			if (Vector3.Dot(velocitys[i], prePositions[0] - prePositions[velocitys.Length - 1]) > 0)
			{
				forwardVelocity.Add(velocitys[i]);
			}
		}
		//Listにしたベクトルの大きさを求める
		for (int i = 0; i < forwardVelocity.Count; i++)
		{
			forwardVelocitySizes.Add(forwardVelocity[i].magnitude);
		}
		//Listにしたベクトルの大きさから偏差値を求め，偏差値60以上を採用する
		for (int i = 0; i < forwardVelocity.Count; i++)
		{
			highDeviation.Add(MathmaticsCulculater.CulculateDeviationValue(forwardVelocitySizes[i],
																		   forwardVelocitySizes));
			if (highDeviation[i] > 60)
			{
				adoptedVelocity.Add(forwardVelocity[i]);
			}
		}

		if(adoptedVelocity == null){
			adoptedVelocity.Add(new Vector3(0, 0, 0));
		}
	}

	//pieの飛んでく方向を計算する
	Vector3 ThrowPieDirection(Vector3[] prePoses)
	{
		//妥当なベクトルの選定
		ExtructedVelocity(prePoses);
		//MathmaticsCulculater.RowPathFilter();
		//各ベクトルの最小二乗平面に最後のベクトルを射影したベクトル
		Vector3 direction = MathmaticsCulculater.VectorProjectionToLeastSquaresPlane(adoptedVelocity);

		//テストコード
		//Vector3 direction = prePositions[0] - prePositions[1];
		return direction;
	}

	//方向の計算
	Vector3 CulculateDirection(Vector3[] prePoses)
	{
		Vector3 throwingVector = ThrowPieDirection(prePoses);
		return throwingVector.normalized;
	}

	//速度の計算
	float CulculateSpeed(Vector3[] prePoses, float speed)
	{
		Vector3 throwingVector = ThrowPieDirection(prePoses);
		return throwingVector.magnitude * speed/ Time.deltaTime;
	}
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pieを投げる機能を持ったクラス
public class PieThrower : MonoBehaviour
{

	//速度ベクトルのうち信頼性の高いもの
	List<Vector3> adoptedVelocity = new List<Vector3>();

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	//pieを投げる関数
	public void ThrowPie(Pie pie, Vector3[] prePoses, float _speed)
	{
		Vector3 direction = CulculateDirection(prePoses);
		float speed = CulculateSpeed(prePoses,_speed);
		pie.Throwed(direction, speed);
	}

	//妥当な速度ベクトルのみを抽出する
	void ExtructedVelocity(Vector3[] prePositions)
	{
		//直近10フレームでの速度ベクトル
		Vector3[] velocitys = new Vector3[10];
		//前向きのベクトル
		List<Vector3> forwardVelocity = new List<Vector3>();
		//
		List<float> forwardVelocitySizes = new List<float>();
		List<float> highDeviation = new List<float>();

		//前向きのベクトルだけをListにする 
		for (int i = 0; i < velocitys.Length - 1; i++)
		{
			velocitys[i] = prePositions[i] - prePositions[i+1];
			if (Vector3.Dot(velocitys[i], prePositions[0] - prePositions[velocitys.Length - 1]) > 0)
			{
				forwardVelocity.Add(velocitys[i]);
			}
		}
		//Listにしたベクトルの大きさを求める
		for (int i = 0; i < forwardVelocity.Count; i++)
		{
			forwardVelocitySizes.Add(forwardVelocity[i].magnitude);
		}
		//Listにしたベクトルの大きさから偏差値を求め，偏差値60以上を採用する
		for (int i = 0; i < forwardVelocity.Count; i++)
		{
			highDeviation.Add(MathmaticsCulculater.CulculateDeviationValue(forwardVelocitySizes[i],
																		   forwardVelocitySizes));
			if (highDeviation[i] > 60)
			{
				adoptedVelocity.Add(forwardVelocity[i]);
			}
		}

		if(adoptedVelocity == null){
			adoptedVelocity.Add(new Vector3(0, 0, 0));
		}
	}

	//pieの飛んでく方向を計算する
	Vector3 ThrowPieDirection(Vector3[] prePoses)
	{
		//妥当なベクトルの選定
		ExtructedVelocity(prePoses);
		//MathmaticsCulculater.RowPathFilter();
		//各ベクトルの最小二乗平面に最後のベクトルを射影したベクトル
		Vector3 direction = MathmaticsCulculater.VectorProjectionToLeastSquaresPlane(adoptedVelocity);

		//テストコード
		//Vector3 direction = prePositions[0] - prePositions[1];
		return direction;
	}

	//方向の計算
	Vector3 CulculateDirection(Vector3[] prePoses)
	{
		Vector3 throwingVector = ThrowPieDirection(prePoses);
		return throwingVector.normalized;
	}

	//速度の計算
	float CulculateSpeed(Vector3[] prePoses, float speed)
	{
		Vector3 throwingVector = ThrowPieDirection(prePoses);
		return throwingVector.magnitude * speed/ Time.deltaTime;
	}
}
