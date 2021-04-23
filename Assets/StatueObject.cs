using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StatueObject : MonoBehaviour
{
    public bool baseObject = false;
    public bool connected = false;
    private LayerMask startLayer;
    private bool wasGrabbed;

    private int debugCount;
    private void Start()
    {
        startLayer = gameObject.layer;
    }

    private void Update()
    {
        if(!baseObject)
        {
            if (GetComponent<XRBaseInteractable>().isSelected && gameObject.layer != 11)
            {
                gameObject.layer = 11;
            }
            else if (!GetComponent<XRBaseInteractable>().isSelected && gameObject.layer == 11)
            {
                gameObject.layer = startLayer;
                //fix rigidbody when let go
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if this object collides with another statue object
        if (other.GetComponent<StatueObject>() && GetComponent<XRGrabInteractable>())
        {
            GameObject obj = other.gameObject;

            //if this object is not being grabbed and not connected and was grabbed last frame and the interacting object is connected to the base
            if (!GetComponent<XRGrabInteractable>().isSelected && !connected && wasGrabbed && other.GetComponent<StatueObject>().connected)
            {
                Debug.Log("Placing");

                wasGrabbed = false;
                //connect it
                connected = true;
                //make it so that the object won't move
                GetComponent<Rigidbody>().isKinematic = true;
                //change the layer for collisions
                gameObject.layer = 6;
                //set the parent to the base
                if(other.GetComponent<StatueObject>().baseObject)
                {
                    transform.parent = obj.transform;
                }
                else
                {
                    transform.parent = obj.transform.parent;
                }
            }
            //if this object is grabbed and connected
            else if(GetComponent<XRGrabInteractable>().isSelected && connected)
            {
                Debug.Log("Removing");
                //disconnect it
                GetComponent<StatueObject>().connected = false;
                //set not kinematic
                //GetComponent<Rigidbody>().isKinematic = false;
                //set layer back to held
                gameObject.layer = 6;
            }
            //if this object is grabbed and not connected 
            else if(GetComponent<XRGrabInteractable>().isSelected && !connected)
            {
                debugCount++;
                Debug.Log("Placing" + debugCount);
                wasGrabbed = true;
            }
        }
    }
}
