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

	private void OnTriggerExit(Collider other)
	{
		//クリームが残るようにする
		//消滅させる

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
