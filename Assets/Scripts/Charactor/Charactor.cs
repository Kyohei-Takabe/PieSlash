using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    public GameObject pie;

    public virtual void Start()
    {
        //pie = gameObject.AddComponent<Pie>() as Pie;

    }

    public void Move(float speed, Vector3 direction)
    {
        gameObject.transform.position += speed * direction * Time.deltaTime;
    }

    public void throwPie()
    {
        var newPie = Instantiate(pie);
        //位置をCharactorに合わせて移動
        newPie.transform.position += gameObject.transform.position;

        //Charactorの向きを示す単位ベクトルの算出
        float Y_angle = gameObject.transform.eulerAngles.y;
        float Y_radius = Y_angle * (Mathf.PI / 180.0f);
        Vector3 direction = new Vector3(Mathf.Sin(Y_radius), 0.0f, Mathf.Cos(Y_radius));
        //Charactor中心にnewPieを回転させる
        newPie.transform.RotateAround(gameObject.transform.position, Vector3.up, Y_angle);
        //print(newPie.transform.position);
        //print(Y_angle);

        var rig = newPie.GetComponent<Rigidbody>();
        rig.AddForce(100 * direction);
        //newPie.thrown(100*direction);
        Destroy(newPie, 5.0f);

    }

}