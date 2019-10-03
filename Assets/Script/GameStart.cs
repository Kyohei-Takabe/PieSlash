using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

  //  void OnCollisionExit(Collider col)
  //  {
		//if (col.tag == "Player" || col.tag == "Pie")
    //    {
    //        SceneManager.LoadScene("Play");
    //    }
    //}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.transform.tag == "Hand" || collision.transform.tag == "PlayerPie")
		{
			SceneManager.LoadScene("Play");
		}
	}
}
