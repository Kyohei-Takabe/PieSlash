using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerRL
{
	right,
	left
}

public class OVRConsole : MonoBehaviour
{
	OVRDebugConsole console;

	[SerializeField]
	OVRInput.Controller Controller;
	//[SerializeField]
	//OVRInput.Controller leftController;
	[SerializeField]
	GameObject Collider;
	[SerializeField]
	OVRPlayerController player;
	[SerializeField]
	OVRCameraRig cameraRig;
	//[SerializeField]
	//GameObject left;
	public bool isControllerData;
	public ControllerRL RL;
	public CharacterStatus status;

	private Color col;


	// Start is called before the first frame update
	void Start()
    {
		console = OVRDebugConsole.instance;
    }

    // Update is called once per frame
    void Update()
    {
		if(!isControllerData){
			console.AddMessage(status.acceralation.ToString(),Color.green);
			console.AddMessage(status.damp.ToString(), Color.green);
			console.AddMessage(status.mass.ToString(), Color.green);
			console.AddMessage(status.comb.ToString(), Color.green);
			console.AddMessage(status.combMax.ToString(), Color.green);
			console.AddMessage(status.acceralationRate.ToString(), Color.green);
		}
		else{
			if (RL == ControllerRL.left)
			{
				col = Color.red;
			}
			else if(RL == ControllerRL.left){
				col = Color.blue;
			}
			console.AddMessage("Controller", col);
			console.AddMessage(transform.TransformPoint(OVRInput.GetLocalControllerPosition(Controller)).ToString(), col);
			console.AddMessage("Collider", col);
			console.AddMessage(transform.TransformPoint(Collider.transform.position).ToString(), col);
			console.AddMessage("Player", col);
			console.AddMessage(transform.TransformPoint(player.transform.position).ToString(), col);
			console.AddMessage("Camara", col);
			console.AddMessage(transform.TransformPoint(cameraRig.transform.position).ToString(), col);
		}

	}
}
