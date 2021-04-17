using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalButton : MonoBehaviour
{
    public float buttonLimit = -0.03f;

    private Transform buttonTop;

    public bool openRaygunHolder;
    private float rotationSpeed = 1;
    private bool lerpRotation = false;
    private Transform glass;

    // Start is called before the first frame update
    void Start()
    {
        buttonTop = transform.Find("ButtonTop");

        if(openRaygunHolder)
        {
            glass = transform.parent.Find("RaygunMachineGlass");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.zero;

        if (buttonTop.localPosition.y > 0)
        {
            pos.y = 0;
        }
        else if(buttonTop.localPosition.y < buttonLimit)
        {
            pos.y = buttonLimit;
        }
        else
        {
            pos.y = buttonTop.localPosition.y;
        }

        buttonTop.localPosition = pos;


        if(openRaygunHolder)
        {
            if (lerpRotation)
            {
                if (glass.localEulerAngles.y < 270)
                {
                    glass.Rotate(0, 0, rotationSpeed, Space.Self);
                }
            }
            else
            {
                if (glass.localEulerAngles.y > 90)
                {
                    glass.Rotate(0, 0, -rotationSpeed, Space.Self);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Button")
        {
            if(openRaygunHolder == true)
            {
                lerpRotation = !lerpRotation;
            }
        }
    }
}
