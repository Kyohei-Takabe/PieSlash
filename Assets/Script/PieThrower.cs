using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pieを投げる機能を持ったクラス
public class PieThrower : MonoBehaviour
{
	[SerializeField]
	protected bool m_parentHeldObject = false;
	[SerializeField]
	protected OVRInput.Controller m_controller;
	[SerializeField]
	protected Transform m_parentTransform;


	//protected PieGenerator generator;

	protected Pie m_grabbedObj = null;

	protected Vector3 m_lastPos;
	protected Quaternion m_lastRot;

	protected Vector3 m_anchorOffsetPosition;
	protected Quaternion m_anchorOffsetRotation;

	protected Vector3 m_grabbedObjectPosOff;
	protected Quaternion m_grabbedObjectRotOff;
	protected bool m_grabEnabled = true;

	public float throwSpeed { get; set; }
	protected bool operatingWithoutOVRCameraRig = true;

	protected virtual void Awake()
	{
		m_anchorOffsetPosition = transform.localPosition;
		m_anchorOffsetRotation = transform.localRotation;

		// If we are being used with an OVRCameraRig, let it drive input updates, which may come from Update or FixedUpdate.

		OVRCameraRig rig = null;
		if (transform.parent != null && transform.parent.parent != null)
			rig = transform.parent.parent.GetComponent<OVRCameraRig>();

		if (rig != null)
		{
			rig.UpdatedAnchors += (r) => { OnUpdatedAnchors(); };
			operatingWithoutOVRCameraRig = false;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		m_lastPos = transform.position;
		m_lastRot = transform.rotation;
		if (m_parentTransform == null)
		{
			if (gameObject.transform.parent != null)
			{
				m_parentTransform = gameObject.transform.parent.transform;
			}
			else
			{
				m_parentTransform = new GameObject().transform;
				m_parentTransform.position = Vector3.zero;
				m_parentTransform.rotation = Quaternion.identity;
			}
		}
	}

	void FixedUpdate()
	{
		if (operatingWithoutOVRCameraRig)
			OnUpdatedAnchors();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnUpdatedAnchors()
	{
		Vector3 handPos = OVRInput.GetLocalControllerPosition(m_controller);
		Quaternion handRot = OVRInput.GetLocalControllerRotation(m_controller);
		Vector3 destPos = m_parentTransform.TransformPoint(m_anchorOffsetPosition + handPos);
		Quaternion destRot = m_parentTransform.rotation * handRot * m_anchorOffsetRotation;
		GetComponent<Rigidbody>().MovePosition(destPos);
		GetComponent<Rigidbody>().MoveRotation(destRot);

		if (!m_parentHeldObject)
		{
			MoveGrabbedObject(destPos, destRot);
		}
		m_lastPos = transform.position;
		m_lastRot = transform.rotation;
	}

	void OnDestroy()
	{
		if (m_grabbedObj != null)
		{
			GrabEnd();
		}
	}

	public virtual void GrabBegin(Pie grabbable)
	{
		GrabVolumeEnable(false);
		m_grabbedObj = grabbable;
		m_grabbedObj.GrabBegin(this,m_grabbedObj.grabPoint);
		m_lastPos = transform.position;
		m_lastRot = transform.rotation;

		Quaternion relOri = Quaternion.Inverse(transform.rotation) * m_grabbedObj.transform.rotation;
		m_grabbedObjectRotOff = relOri;

		//MoveGrabbedObject(m_lastPos, m_lastRot, true);
		if (m_parentHeldObject)
		{
			m_grabbedObj.transform.parent = transform;
		}
	}

	//掴んでいるときにオブジェクトが手に追従する
	public void MoveGrabbedObject(Vector3 pos, Quaternion rot, bool forceTeleport = false)
	{
		if (m_grabbedObj == null)
		{
			return;
		}

		Rigidbody grabbedRigidbody = m_grabbedObj.grabbedRigidbody;
		Vector3 grabbablePosition = pos + rot * m_grabbedObjectPosOff;
		Quaternion grabbableRotation = rot * m_grabbedObjectRotOff;

		grabbedRigidbody.MovePosition(grabbablePosition);
		grabbedRigidbody.MoveRotation(grabbableRotation);
	}

	//GrabEndを呼べば投げることができる
	public void GrabEnd()
	{
		if (m_grabbedObj != null)
		{
			OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(m_controller), orientation = OVRInput.GetLocalControllerRotation(m_controller) };
			OVRPose offsetPose = new OVRPose { position = m_anchorOffsetPosition, orientation = m_anchorOffsetRotation };
			localPose = localPose * offsetPose;

			OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
			Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(m_controller) * throwSpeed;
			Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(m_controller);

			GrabbableRelease(linearVelocity, angularVelocity);
		}

		// Re-enable grab volumes to allow overlap events
		GrabVolumeEnable(true);
	}

	protected virtual void GrabVolumeEnable(bool enabled)
	{
		if (m_grabEnabled == enabled)
		{
			return;
		}

		m_grabEnabled = enabled;
	}

	protected void GrabbableRelease(Vector3 linearVelocity, Vector3 angularVelocity)
	{
		m_grabbedObj.GrabEnd(linearVelocity, angularVelocity);
		if (m_parentHeldObject) m_grabbedObj.transform.parent = null;
		m_grabbedObj = null;
	}
}
