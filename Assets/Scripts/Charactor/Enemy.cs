using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charactor
{
    public float speed;
    public float timeOut;
    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;

        if (pos.x < -5 || pos.x > 5)
        {
            speed *= -1;
        }

        gameObject.transform.position += Vector3.right * speed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            // Do anything

            timeElapsed = 0.0f;
            throwPie();

        }

    }
}
