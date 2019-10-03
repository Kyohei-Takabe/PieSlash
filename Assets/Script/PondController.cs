using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondController : MonoBehaviour
{

	public Pond[] ponds;
	//Pond current;
	int nowIndex;
    // Start is called before the first frame update
    void Start()
    {
		nowIndex = Random.Range(0, 5);
		ponds[nowIndex].cream = 200.0f;
		ponds[nowIndex].hasCream = true;

		foreach (Pond pond in ponds){
			pond.VanishMaterial();
		}
	}

    // Update is called once per frame
    void Update()
    {
		if(!ponds[nowIndex].hasCream){
			int temp = Random.Range(0, 4);
			nowIndex = (nowIndex > temp) ? temp : temp++;
			ponds[nowIndex].hasCream = true;
			ponds[nowIndex].cream = 200.0f;

			foreach(Pond pond in ponds){
				pond.VanishMaterial();
			}
		}
    }
}
