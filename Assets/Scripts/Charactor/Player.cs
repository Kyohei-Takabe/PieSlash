using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charactor
{
    //Cameraを格納
    public GameObject Camera;
    MeshRenderer mesh;
    GameObject Cream;


    public float speed;
    //player,cameraのtransformを持つ
    Rigidbody rig;

    public AudioClip hitSound;
    public AudioClip washSound;
    // Use this for initialization
    public override void Start()
    {
        rig = GetComponent<Rigidbody>();
        Cream = transform.Find("Cream").gameObject;
        mesh = Cream.GetComponent<MeshRenderer>();
        mesh.enabled = false;
        //Camera = transform.Find("PlayerCamera").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = gameObject.transform.position;
        //横(x軸方向)にどれだけ視点が回転するのか
        float X_Rotation = Input.GetAxis("Mouse X");
        //縦(z軸方向)にどれだけ視点が回転するのか
        float Y_Rotation = Input.GetAxis("Mouse Y");
        //Playerが左右に動く
        //子のカメラも左右に動く
        gameObject.transform.Rotate(0, X_Rotation, 0);
        //カメラをx軸中心に-Y_Rotationだけ回転
        //カメラが上下に動く
        Camera.transform.Rotate(-Y_Rotation, 0, 0);


        float angleDir = gameObject.transform.eulerAngles.y * (Mathf.PI / 180.0f);
        //縦方向
        Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));
        //横方向
        Vector3 dir2 = new Vector3(Mathf.Cos(angleDir), 0, -Mathf.Sin(angleDir));


        if (Input.GetKey(KeyCode.W))
        {
            Move(speed, dir1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(speed, -dir2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(speed, dir2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(speed, -dir1);
        }

        rig.MovePosition(pos);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            throwPie();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pie")
        {

            //print("Hit");
            mesh.enabled = true;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }

        if (collision.gameObject.tag == "River")
        {
            mesh.enabled = false;
            AudioSource.PlayClipAtPoint(washSound, transform.position);
        }

    }

}
