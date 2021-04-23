using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DialObject : MonoBehaviour
{
    private float startHandRot;
    private float handRot;
    public Vector3 startRot = new Vector3(0, 90, 90);
    private bool grabbed;
    public float angle;
    private float newAngle;

    void Update()
    {
        /*if (GetComponent<XRGrabInteractable>().selectingInteractor != null)
        {
            //get the z rotation of the grabber
            handRot = GetComponent<XRGrabInteractable>().selectingInteractor.transform.eulerAngles.z;

            //check if the dial was just grabbed
            if (grabbed == false)
            {
                //set the start rotation
                startHandRot = handRot;
                grabbed = true;
            }

            //find the angle change since grabbing
            float angle = handRot - startHandRot;

            Debug.Log(startRot.x + " " + angle);
            
            transform.eulerAngles = new Vector3(startRot.x + angle, startRot.y, startRot.z);
        }
        else
        {
            grabbed = false;
            startRot.x = transform.eulerAngles.x;
        }*/
    }
}
