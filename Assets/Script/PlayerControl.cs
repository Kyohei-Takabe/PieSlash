using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Transform>().Translate(new Vector3(-0.1f, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Transform>().Translate(new Vector3(0.1f, 0, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Transform>().Translate(new Vector3(0, 0, 0.1f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            GetComponent<Transform>().Translate(new Vector3(0, 0, -0.1f));
        }
    }
}
