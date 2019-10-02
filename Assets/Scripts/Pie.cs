using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generable:MonoBehaviour
{
	protected Rigidbody rig;
	//protected OVRGrabbable grabbable;
	void Start()
	{
		rig = GetComponent<Rigidbody>();
		//grabbable = GetComponent<OVRGrabbable>();
		//status = GetComponent<PieStatus>();
	}
}

public class Pie : Generable
{
	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		//クリームが残るようにする
		//消滅させる
		//地面に着弾した時の処理
		if (other.tag == "Ground")
		{
			rig.constraints = RigidbodyConstraints.None;
			//rig.constraints = RigidbodyConstraints.FreezePosition;
		}
		//Playerに着弾した時の処理
		//Enemyに着弾した時の処理
		//ステージ上の障害物に着弾した時の処理

	}

	private void OnCollisionEnter(Collision collision)
	{
		string tag = collision.transform.tag;
		if (tag == "Ground")
		{
			//rig.constraints = RigidbodyConstraints.None;
			//rig.velocity = Vector3.zero;
			//rig.constraints = RigidbodyConstraints.FreezePosition;
			Destroy(this.gameObject);
		}

		if(tag == "Player"||tag == "Enemy"){
			Destroy(this.gameObject);
		}

	}

	public void Throwed(Vector3 direction, Vector3 anglespeed, float speed)
	{
		rig.velocity = direction * speed;
		rig.angularVelocity = anglespeed;
	}

	public void Throwed(Vector3 direction, float speed)
	{
		rig.velocity = direction * speed;
	}
}
