//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Grabber : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//	protected virtual void GrabBegin()
//    {
//        float closestMagSq = float.MaxValue;
//		OVRGrabbable closestGrabbable = null;
//        Collider closestGrabbableCollider = null;

//        // Iterate grab candidates and find the closest grabbable candidate
//		foreach (OVRGrabbable grabbable in m_grabCandidates.Keys)
//        {
//            bool canGrab = !(grabbable.isGrabbed && !grabbable.allowOffhandGrab);
//            if (!canGrab)
//            {
//                continue;
//            }

//            for (int j = 0; j < grabbable.grabPoints.Length; ++j)
//            {
//                Collider grabbableCollider = grabbable.grabPoints[j];
//                // Store the closest grabbable
//				//掴むオブジェクトを選ぶための位置がm_gripTransform
//                Vector3 closestPointOnBounds = grabbableCollider.ClosestPointOnBounds(m_gripTransform.position);
//				//掴もうとするオブジェクトとm_gripTransformの距離
//                float grabbableMagSq = (m_gripTransform.position - closestPointOnBounds).sqrMagnitude;
//				//ここまでサーチしたオブジェクトで一番近くであるか？
//                if (grabbableMagSq < closestMagSq)
//                {
//                    closestMagSq = grabbableMagSq;
//                    closestGrabbable = grabbable;
//                    closestGrabbableCollider = grabbableCollider;
//                }
//            }
//        }

//        // Disable grab volumes to prevent overlaps
//        GrabVolumeEnable(false);

//        if (closestGrabbable != null)
//        {
//            if (closestGrabbable.isGrabbed)
//            {
//                closestGrabbable.grabbedBy.OffhandGrabbed(closestGrabbable);
//            }

//            m_grabbedObj = closestGrabbable;
//            m_grabbedObj.GrabBegin(this, closestGrabbableCollider);

//            m_lastPos = transform.position;
//            m_lastRot = transform.rotation;

//            // Set up offsets for grabbed object desired position relative to hand.
//            if(m_grabbedObj.snapPosition)
//            {
//                m_grabbedObjectPosOff = m_gripTransform.localPosition;
//                if(m_grabbedObj.snapOffset)
//                {
//                    Vector3 snapOffset = m_grabbedObj.snapOffset.position;
//                    if (m_controller == OVRInput.Controller.LTouch) snapOffset.x = -snapOffset.x;
//                    m_grabbedObjectPosOff += snapOffset;
//                }
//            }
//            else
//            {
//                Vector3 relPos = m_grabbedObj.transform.position - transform.position;
//                relPos = Quaternion.Inverse(transform.rotation) * relPos;
//                m_grabbedObjectPosOff = relPos;
//            }

//            if (m_grabbedObj.snapOrientation)
//            {
//                m_grabbedObjectRotOff = m_gripTransform.localRotation;
//                if(m_grabbedObj.snapOffset)
//                {
//                    m_grabbedObjectRotOff = m_grabbedObj.snapOffset.rotation * m_grabbedObjectRotOff;
//                }
//            }
//            else
//            {
//                Quaternion relOri = Quaternion.Inverse(transform.rotation) * m_grabbedObj.transform.rotation;
//                m_grabbedObjectRotOff = relOri;
//            }

//            // Note: force teleport on grab, to avoid high-speed travel to dest which hits a lot of other objects at high
//            // speed and sends them flying. The grabbed object may still teleport inside of other objects, but fixing that
//            // is beyond the scope of this demo.
//            MoveGrabbedObject(m_lastPos, m_lastRot, true);
//            if(m_parentHeldObject)
//            {
//                m_grabbedObj.transform.parent = transform;
//            }
//        }
//    }

//    protected virtual void MoveGrabbedObject(Vector3 pos, Quaternion rot, bool forceTeleport = false)
//    {
//        if (m_grabbedObj == null)
//        {
//            return;
//        }

//        Rigidbody grabbedRigidbody = m_grabbedObj.grabbedRigidbody;
//        Vector3 grabbablePosition = pos + rot * m_grabbedObjectPosOff;
//        Quaternion grabbableRotation = rot * m_grabbedObjectRotOff;

//        if (forceTeleport)
//        {
//            grabbedRigidbody.transform.position = grabbablePosition;
//            grabbedRigidbody.transform.rotation = grabbableRotation;
//        }
//        else
//        {
//            grabbedRigidbody.MovePosition(grabbablePosition);
//            grabbedRigidbody.MoveRotation(grabbableRotation);
//        }
//    }

//    protected void GrabEnd()
//    {
//        if (m_grabbedObj != null)
//        {
//			OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(m_controller), orientation = OVRInput.GetLocalControllerRotation(m_controller) };
//            OVRPose offsetPose = new OVRPose { position = m_anchorOffsetPosition, orientation = m_anchorOffsetRotation };
//            localPose = localPose * offsetPose;

//			OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
//			Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(m_controller);
//			Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(m_controller);
//			//Vector3 angularVelocity = 
//			linearVelocity *= throwSpeed;
//            GrabbableRelease(linearVelocity, angularVelocity);
//        }

//        // Re-enable grab volumes to allow overlap events
//        GrabVolumeEnable(true);
//    }

//    protected void GrabbableRelease(Vector3 linearVelocity, Vector3 angularVelocity)
//    {
//        m_grabbedObj.GrabEnd(linearVelocity, angularVelocity);
//        if(m_parentHeldObject) m_grabbedObj.transform.parent = null;
//        m_grabbedObj = null;
//    }

//    protected virtual void GrabVolumeEnable(bool enabled)
//    {
//        if (m_grabVolumeEnabled == enabled)
//        {
//            return;
//        }

//        m_grabVolumeEnabled = enabled;
//        for (int i = 0; i < m_grabVolumes.Length; ++i)
//        {
//            Collider grabVolume = m_grabVolumes[i];
//			//掴んだ時は掴む判定をしないようにする
//            grabVolume.enabled = m_grabVolumeEnabled;
//        }

//        if (!m_grabVolumeEnabled)
//        {
//			//話した場合は持っているオブジェクトを含めて全てのオブジェクトを消す
//            m_grabCandidates.Clear();
//        }
//    }

//	protected virtual void OffhandGrabbed(OVRGrabbable grabbable)
//    {
//        if (m_grabbedObj == grabbable)
//        {
//            GrabbableRelease(Vector3.zero, Vector3.zero);
//        }
//    }
//}
