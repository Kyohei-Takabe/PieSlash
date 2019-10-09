using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pie : MonoBehaviour
{
	//public GameObject pieModel;
	[SerializeField]
	protected Collider m_grabPoint = null;
	Rigidbody rig;
	protected bool m_grabbedKinematic = false;
	protected Collider m_grabbedCollider = null;
	protected PieThrower m_grabbedBy = null;
	//AudioSource source;
	[SerializeField]
	protected AudioClip pieCrash;


	public Rigidbody grabbedRigidbody
	{
		get { return rig; }
	}

	public Collider grabPoint
	{
		get { return m_grabPoint; }
	}

	void Awake()
	{
		if (m_grabPoint == null)
		{
			// Get the collider from the grabbable
			Collider collider = this.GetComponent<Collider>();
			if (collider == null)
			{
				throw new ArgumentException("Grabbables cannot have zero grab points and no collider -- please add a grab point or collider.");
			}

			// Create a default grab point
			m_grabPoint = collider;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		rig = GetComponent<Rigidbody>();
		//source = GetComponent<AudioSource>();
		m_grabbedKinematic = GetComponent<Rigidbody>().isKinematic;
	}

	// Update is called once per frame
	void Update()
	{

	}

	virtual public void GrabBegin(PieThrower hand, Collider grabPoint)
	{
		m_grabbedBy = hand;
		m_grabbedCollider = grabPoint;
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
	}

	virtual public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
	{
		rig.isKinematic = false;
		rig.velocity = linearVelocity;
		rig.angularVelocity = angularVelocity;
		m_grabbedBy = null;
		m_grabbedCollider = null;
	}

	private void OnCollisionEnter(Collision collision)
	{
		string tag = collision.transform.tag;
		if (tag == "Ground" || tag == "Wall")
		{
			AudioSource.PlayClipAtPoint(pieCrash,transform.position);
			Destroy(this.gameObject);
		}

		if((this.tag == "PlayerPie" && tag == "Enemy")||(this.tag == "EnemyPie" && tag == "Player")){
			AudioSource.PlayClipAtPoint(pieCrash, transform.position);
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
