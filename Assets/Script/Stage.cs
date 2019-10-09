using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
	public AudioClip titleBGM;
	public AudioClip playBGM;
	AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
		source = GetComponent<AudioSource>();
		if(SceneManager.GetActiveScene().name == "Play"){
			source.clip = playBGM;

		}
		else if(SceneManager.GetActiveScene().name == "title")
		{
			source.clip = titleBGM;
		}
		source.Play();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
