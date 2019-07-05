using System.Collections;
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

	public GameObject Generate(Transform transform)
	{
		Transform trans = transform;
		Vector3 size = transform.localScale;
		trans.position += size;

		GameObject newPie = Instantiate(piePrefab, trans);
		return newPie;
	}
}
