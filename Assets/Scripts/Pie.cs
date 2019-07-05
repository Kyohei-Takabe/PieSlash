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

	}

	public void Throwed(Vector3 direction, float speed)
	{
		rig.AddForce(direction * speed);
	}
}
