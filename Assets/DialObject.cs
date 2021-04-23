using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;
using UnityEngine.UI;

public class DialObject : MonoBehaviour
{
    private float startHandRot;
    private float handRot;
    private Vector3 startRot;
    private bool grabbed;
    private float angle;
    public float value;
    public GameObject valueDialImage;
    public DialObject colorDial;

    private void Start()
    {
        startRot = transform.eulerAngles;
    }

    void Update()
    {
        if(gameObject.name == "ColorDial")
        {
            value = transform.eulerAngles.z / 360;
        }
        else
        {
            value = Mathf.Abs(1 - (transform.eulerAngles.z / 360));
        }

        if (GetComponent<XRGrabInteractable>().selectingInteractor != null)
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
            
            transform.eulerAngles = new Vector3(startRot.x, startRot.y, startRot.z - angle);
        }
        else
        {
            grabbed = false;
            startRot.z = transform.eulerAngles.z;
        }

        if(colorDial == null && valueDialImage != null)
        {
            valueDialImage.GetComponent<Image>().material.color = Color.HSVToRGB(value, 1, 1);
        }
        else if(colorDial != null && valueDialImage != null)
        {
            valueDialImage.GetComponent<Image>().material.color = Color.HSVToRGB(colorDial.value, value, 1);
        }
    }
}
