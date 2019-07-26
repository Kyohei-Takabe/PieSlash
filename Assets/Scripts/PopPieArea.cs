using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPieArea : MonoBehaviour
{
	bool isInside;
	float timer;
	public float timelag;
	GameObject pie;

	PieGenerator generator;
	
    // Start is called before the first frame update
    void Start()
    {
		timer = 0.0f;
		isInside = false;
		generator = GetComponent<PieGenerator>();
		pie = generator.Generate(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		isInside = true;
	}

	private void OnTriggerExit(Collider other)
	{
		isInside = false;
	}

	void NoExistAnyObj()
	{
		if(!isInside)
		{
			if(timer < timelag)
			{
				timer += Time.deltaTime;
			}

			if(timer > timelag)
			{
				timer = 0.0f;
				pie = generator.Generate(transform);
			}
			
		}
	}
}
