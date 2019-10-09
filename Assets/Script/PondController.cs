using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PondController : MonoBehaviour
{

	public Pond[] ponds;
	//Pond current;
	int nowIndex;
    // Start is called before the first frame update
    void Start()
    {
		if(SceneManager.GetActiveScene().name == "Play"){
			nowIndex = Random.Range(0, ponds.Length);
			ponds[nowIndex].cream = 200.0f;
			ponds[nowIndex].hasCream = true;

			foreach (Pond pond in ponds)
			{
				Debug.Log(pond.hasCream);
				pond.VanishMaterial();
			}
		}

		else{
			foreach(Pond pond in ponds){
				pond.cream = 0.0f;
				pond.hasCream = true;
				pond.VanishMaterial();
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
		if(SceneManager.GetActiveScene().name == "Play"){
			if (!ponds[nowIndex].hasCream)
			{
				int temp = Random.Range(0, ponds.Length - 1);
				nowIndex = (nowIndex > temp) ? temp : temp++;
				ponds[nowIndex].hasCream = true;
				ponds[nowIndex].cream = 200.0f;

				foreach (Pond pond in ponds)
				{
					pond.VanishMaterial();
				}
			}
		}
    }
}
