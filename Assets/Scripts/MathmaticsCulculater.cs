﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//ゲーム全体における数学的な計算を持つ
//static関数群
public static class MathmaticsCulculater
{


	//偏差値の計算
	public static float CulculateDeviationValue(float value, List<float> values)
	{
		//偏差
		float deviation = values.Average() - value;
		float standardDeviation = CulculateStandardDeviation(values);
		return deviation * 10 / standardDeviation;
	}

	//分散の計算
	static float CulculateDispersion(List<float> values)
	{
		float dispersion = 0;
		//各データに対する偏差
		float[] squareDeviations = new float[values.Count];

		for (int i = 0; i < values.Count; i++)
		{
			//i番目のデータに対する偏差の二乗
			squareDeviations[i] = (values[i] - values.Average()) * (values[i] - values.Average());
			//偏差の二乗を足していく
			dispersion += squareDeviations[i];
		}

		dispersion /= values.Count;

		return dispersion;
	}

	//標準偏差の計算
	static float CulculateStandardDeviation(List<float> values)
	{
		return Mathf.Sqrt(CulculateDispersion(values));
	}

	//最小二乗平面へのベクトルの射影
	public static Vector3 VectorProjectionToLeastSquaresPlane(List<Vector3> data)
	{
		// 最小二乗平面を用いた推測値を元に速度を求める
		float[] result = SumSamplingData(data);
		float a = result[0];
		float b = result[1];
		float c = result[2];

		// サンプリングした最後のデータを用いて、理想平面の値を求める
		Vector3 v = data.Last();

		float y = a + (b * v.x) + (c * v.z);

		// 実際に利用したいデータ
		Vector3 vec = new Vector3(v.x, y, v.z);
		return vec;

	}

	static float[] SumSamplingData(List<Vector3> data)
	{
		// xの合計値
		float x = 0;

		// x^2の合計値
		float x2 = 0;

		// x * yの合計値
		float xy = 0;

		// x * zの合計値
		float xz = 0;

		// yの合計値
		float y = 0;

		// y^2の合計値
		float y2 = 0;

		// y * zの合計値
		float yz = 0;

		// zの合計値
		float z = 0;

		// 計測したデータから、各種必要なsumを得る
		for (int i = 0; i < data.Count; i++)
		{
			Vector3 v = data[i];

			// 最小二乗平面との誤差は高さの差を計算するので、（今回の式の都合上）Yの値をZに入れて計算する
			float vx = v.x;
			float vy = v.z;
			float vz = v.y;

			x += vx;
			x2 += (vx * vx);
			xy += (vx * vy);
			xz += (vx * vz);

			y += vy;
			y2 += (vy * vy);
			yz += (vy * vz);

			z += vz;
		}

		// matA[0, 0]要素は要素数と同じ（\sum{1}のため）
		float l = 1 * data.Count;

		// 求めた和を行列の要素として2次元配列を生成
		float[,] matA = new float[,]
		{
		{l,  x,  y},
		{x, x2, xy},
		{y, xy, y2},
		};

		float[] b = new float[]
		{
		z, xz, yz
		};

		// 求めた値を使ってLU分解→結果を求める
		return LUDecomposition(matA, b);
	}

	static float[] LUDecomposition(float[,] aMatrix, float[] b)
	{
		// 行列数（Vector3データの解析なので3x3行列）
		int N = aMatrix.GetLength(0);

		// L行列(零行列に初期化)
		float[,] lMatrix = new float[N, N];
		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < N; j++)
			{
				lMatrix[i, j] = 0;
			}
		}

		// U行列(対角要素を1に初期化)
		float[,] uMatrix = new float[N, N];
		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < N; j++)
			{
				uMatrix[i, j] = i == j ? 1f : 0;
			}
		}

		// 計算用のバッファ
		float[,] buffer = new float[N, N];
		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < N; j++)
			{
				buffer[i, j] = 0;
			}
		}

		// LU分解開始
		for (int i = 0; i < N; i++)
		{
			int n = N - i - 1;

			float l0 = lMatrix[i, i] = aMatrix[0, 0];

			// l1成分をコピー
			float[] l1 = new float[n];
			for (int j = 0; j < n; j++)
			{
				lMatrix[j + i + 1, i] = l1[j] = aMatrix[j + 1, 0];
			}

			// u1^T成分をコピー
			float[] u1 = new float[n];
			for (int j = 0; j < n; j++)
			{
				uMatrix[i, j + i + 1] = u1[j] = aMatrix[0, j + 1] / l0;
			}

			// luを求める
			for (int j = 0; j < n; j++)
			{
				for (int k = 0; k < n; k++)
				{
					buffer[j, k] = l1[j] * u1[k];
				}
			}

			// A1を求める
			float[,] A1 = new float[n, n];
			for (int j = 0; j < n; j++)
			{
				for (int k = 0; k < n; k++)
				{
					A1[j, k] = aMatrix[j + 1, k + 1] - buffer[j, k];
				}
			}

			// A1を新しいaMatrixとして利用する
			aMatrix = A1;
		}

		// 求めたLU行列を使って連立方程式を解く
		float[] y = new float[N];
		for (int i = 0; i < N; i++)
		{
			float sum = 0;
			for (int k = 0; k <= i - 1; k++)
			{
				sum += lMatrix[i, k] * y[k];
			}
			y[i] = (b[i] - sum) / lMatrix[i, i];
		}

		float[] x = new float[N];
		for (int i = N - 1; i >= 0; i--)
		{
			float sum = 0;
			for (int j = i + 1; j <= N - 1; j++)
			{
				sum += uMatrix[i, j] * x[j];
			}
			x[i] = y[i] - sum;
		}

		return x;
	}


}
