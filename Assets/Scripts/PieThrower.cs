using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pieを投げる機能を持ったクラス
public class PieThrower : MonoBehaviour
{
	//現在から11フレーム前までの位置ベクトル
	Vector3[] prePositions = new Vector3[11];
	//速度ベクトルのうち信頼性の高いもの
	List<Vector3> adoptedVelocity = new List<Vector3>();

	// Start is called before the first frame update
	void Start()
	{
		//とりあえず0で初期化
		for (int i = 0; i < prePositions.Length; i++)
		{
			prePositions[i] = Vector3.zero;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	//このクラスを利用するオブジェクトのpos情報をsetする
	public void SetPosition(Vector3 lastFramePos)
	{
		for (int i = 1; i < prePositions.Length; i++)
		{
			prePositions[i] = prePositions[i - 1];
		}

		prePositions[0] = lastFramePos;
	}

	//pieを投げる関数
	public void ThrowPie(GameObject pie)
	{
		Vector3 direction = CulculateDirection();
		float speed = CulculateSpeed();
		Pie pieScript = pie.GetComponent<Pie>();
		pieScript.Throwed(direction, speed);
	}

	//妥当な速度ベクトルのみを抽出する
	void ExtructedVelocity()
	{
		//直近10フレームでの速度ベクトル
		Vector3[] velocitys = new Vector3[10];
		//前向きのベクトル
		List<Vector3> forwardVelocity = new List<Vector3>();
		//
		List<float> forwardVelocitySizes = new List<float>();
		List<float> highDeviation = new List<float>();

		//前向きのベクトルだけをListにする 
		for (int i = 0; i < velocitys.Length; i++)
		{
			velocitys[i] = prePositions[i + 1] - prePositions[i];
			if (Vector3.Dot(velocitys[i], velocitys[9]) > 0)
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
	Vector3 ThrowPieDirection()
	{
		//妥当なベクトルの選定
		ExtructedVelocity();
		//MathmaticsCulculater.RowPathFilter();
		Vector3 direction = MathmaticsCulculater.VectorProjectionToLeastSquaresPlane(adoptedVelocity);
		return direction;
	}

	//方向の計算
	Vector3 CulculateDirection()
	{
		Vector3 throwingVector = ThrowPieDirection();
		return throwingVector.normalized;
	}

	//速度の計算
	float CulculateSpeed()
	{
		Vector3 throwingVector = ThrowPieDirection();
		return throwingVector.magnitude;
	}
}
