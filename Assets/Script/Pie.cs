using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
	//public GameObject pieModel;
	Rigidbody rig;
	// Start is called before the first frame update
	void Start()
	{
		rig = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerExit(Collider other)
	{
		//クリームが残るようにする
		//消滅させる
		//地面に着弾した時の処理
		//if (other.tag == "Ground")
		//{
		//	//rig.constraints = RigidbodyConstraints.None;
		//	//rig.constraints = RigidbodyConstraints.FreezePosition;
		//	Destroy(this.gameObject);
		//}
		//Playerに着弾した時の処理
		//Enemyに着弾した時の処理
		//ステージ上の障害物に着弾した時の処理

	}

	private void OnCollisionEnter(Collision collision)
	{
		string tag = collision.transform.tag;
		if (tag == "Ground" || tag == "Wall")
		{
			//rig.constraints = RigidbodyConstraints.None;
			//rig.velocity = Vector3.zero;
			//rig.constraints = RigidbodyConstraints.FreezePosition;
			Destroy(this.gameObject);
		}

		if((this.tag == "PlayerPie" && tag == "Enemy")||(this.tag == "EnemyPie" && tag == "Player")){
			Destroy(this.gameObject);
			CharacterStatus status = collision.transform.GetComponent<CharacterStatus>();
			status.mass += 5.0f;
		}
	}

	public void Throwed(Vector3 direction, Vector3 anglespeed, float speed)
	{
		rig.velocity = direction * speed;
		rig.angularVelocity = anglespeed;
	}

	public void Throwed(Vector3 direction, float speed)
	{
		rig.AddForce(direction * speed);
	}
}
