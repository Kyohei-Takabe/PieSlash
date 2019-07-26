﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pieを生成する機能を持つクラス
public class PieGenerator : MonoBehaviour
{
	public GameObject piePrefab;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	//Playerの手元で生成する用
	public GameObject Generate(Transform _transform, Hand hand)
	{
		Transform trans = _transform;
		//Vector3 size = _transform.localScale;

		if (hand == Hand.right)
		{
			trans.Translate(-0.1f, 0, 0);
		}
		if (hand == Hand.left)
		{
			trans.Translate(0.1f, 0, 0);
		}

		//trans.Rotate(new Vector3(0,0,90));

		GameObject newPie = Instantiate(piePrefab, trans.position,trans.rotation);

		return newPie;
	}
	//Tableの上に生成する用
	public GameObject Generate(Transform _trans){
		Transform trans = _trans;
		trans.Translate(0, piePrefab.transform.position.y, 0);
		GameObject newPie = Instantiate(piePrefab,trans);
		return newPie;
	}
}
