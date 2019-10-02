using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
	public float initMass = 60.0f;
	public float _acceralation = 0.1f;
	public float _damp = 0.3f;
	//float acceralationRata = 1.0f;
	public float throwSpeed = 2.0f;
	public float maxPieCream = 100.0f;
	int _combMax;

	public float acceralation{
		//get { return _acceralation * acceralationRate; }
		get { return _acceralation; }
	}

	public float damp{
		//get { return _damp * acceralationRate; }
		get { return _damp; }
	}

	public float mass { get; set; }

	public float pieCream { get; set; }

	public int comb { get; set; }


	public int combMax{ get; set; }

	public bool isHit { get; set; }

	public float acceralationRate{
		get { return initMass/mass; }
	}

	// Start is called before the first frame update
	void Start()
	{
		mass = initMass;
		pieCream = maxPieCream;
		comb = 0;
		_combMax = 0;
		isHit = false;
	}

	private void Update()
	{
		
	}

}
