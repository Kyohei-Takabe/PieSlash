using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRConsole : MonoBehaviour
{
	OVRDebugConsole console;

	[SerializeField]
	OVRInput.Controller rightController;
	[SerializeField]
	OVRInput.Controller leftController;

    // Start is called before the first frame update
    void Start()
    {
		console = OVRDebugConsole.instance;
    }

    // Update is called once per frame
    void Update()
    {
		console.AddMessage(OVRInput.GetLocalControllerPosition(rightController).ToString(), Color.red);
		console.AddMessage(OVRInput.GetLocalControllerRotation(rightController).ToString(), Color.red);

		console.AddMessage(OVRInput.GetLocalControllerPosition(leftController).ToString(), Color.green);
		console.AddMessage(OVRInput.GetLocalControllerRotation(leftController).ToString(), Color.green);
	}
}
