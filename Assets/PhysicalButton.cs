using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalButton : MonoBehaviour
{
    public float buttonLimit = -0.03f;

    private Transform buttonTop;

    // Start is called before the first frame update
    void Start()
    {
        buttonTop = transform.Find("ButtonTop");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localVelocity = buttonTop.InverseTransformDirection(buttonTop.GetComponent<Rigidbody>().velocity);
        localVelocity.x = 0;
        localVelocity.z = 0;

        buttonTop.GetComponent<Rigidbody>().velocity = transform.TransformDirection(localVelocity);

        if (buttonTop.localPosition.y > 0)
        {
            buttonTop.localPosition = Vector3.zero;
        }
        else if(buttonTop.localPosition.y < buttonLimit)
        {
            buttonTop.localPosition = new Vector3(0, buttonLimit, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject, other.gameObject);
        if(other.tag == "Button")
        {
            Debug.Log("Button Pressed");
        }
    }
}
