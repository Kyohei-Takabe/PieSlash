using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pie : MonoBehaviour
{
    Image image;
    Rigidbody rig;
    void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.clear;
        rig = gameObject.GetComponent<Rigidbody>();
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            image.color = new Color(1.0f, 246.0f / 255.0f, 208.0f / 255.0f, 235.0f / 255.0f);
        }
        
    }

    public void thrown(Vector3 force)
    {
        rig.AddForce(force);
    }
}
