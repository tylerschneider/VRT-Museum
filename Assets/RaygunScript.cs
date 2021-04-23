using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class RaygunScript : MonoBehaviour
{
    public static RaygunScript s;

    public float distance = 10f;
    public float sizerSpeed = 1f;
    public int sizerRotationSpeed = 1;
    public float maxSizerSize = 5f;

    private RaygunObject raygunObject;
    private string raygunObjectType = "";

    private int rotationTarget = 0;
    private int rotationProgress = 0;

    private bool pressing = false;

    public InputActionReference secondButtonLeft;
    public InputActionReference secondButtonRight;

    void Start()
    {
        if (s == null)
        {
            s = this;
        }
    }

    public void SetObject()
    {
        XRBaseInteractable obj = transform.Find("AttachPoint").GetComponent<XRSocketInteractor>().selectTarget;
        raygunObject = obj.GetComponent<RaygunObject>();
        raygunObjectType = raygunObject.raygunObjectType;
    }

    public void UnsetObject()
    {
        raygunObject = null;
        raygunObjectType = "";
    }

    public void OnTriggerButtonEnter()
    {
        pressing = true;

        transform.Find("RaycastPoint").GetComponent<LineRenderer>().enabled = true;
    }

    public void OnTriggerButtonExit()
    {
        pressing = false;
        transform.Find("RaycastPoint").GetComponent<LineRenderer>().enabled = false;
    }

    public void OnSecondButtonPress()
    {
        //if using the raygun sizer
        if (raygunObjectType == "Sizer")
        {
            //get label objects
            Transform sizerLabels = raygunObject.transform.Find("SizerLabels");
            Transform sizerXYZ = raygunObject.transform.Find("SizerXYZ");

            //change the rotation based on which sizer is selected
            switch(raygunObject.sizer)
            {
                case "X":
                    raygunObject.sizer = "Y";
                    rotationTarget = 120;
                    break;
                case "Y":
                    raygunObject.sizer = "Z";
                    rotationTarget = 240;
                    break;
                case "Z":
                    //change the gameobject when xyz
                    raygunObject.sizer = "XYZ";
                    sizerXYZ.gameObject.SetActive(true);
                    sizerLabels.gameObject.SetActive(false);
                    break;
                case "XYZ":
                    raygunObject.sizer = "X";
                    rotationTarget = 0;
                    sizerXYZ.gameObject.SetActive(false);
                    sizerLabels.gameObject.SetActive(true);
                    break;
            }
        }
    }

    private void Update()
    {
        if(secondButtonLeft.action.phase == InputActionPhase.Performed || secondButtonRight.action.triggered)
        {
            OnSecondButtonPress();
        }

        //if sizer
        if(raygunObjectType == "Sizer")
        {
            //get the label
            Transform sizerLabels = raygunObject.transform.Find("SizerLabels");

            //check if done rotating
            if (rotationProgress != rotationTarget)
            {
                //add rotation speed
                rotationProgress += sizerRotationSpeed;

                //if 360, reset to 0
                if (rotationProgress == 360)
                {
                    rotationProgress = 0;
                }

                //rotate the labels
                sizerLabels.Rotate(0, sizerRotationSpeed, 0, Space.Self);
            }
        }

        if (pressing)
        {
            //check for hit
            RaycastHit hit;
            if (Physics.Raycast(transform.Find("RaycastPoint").transform.position, transform.forward, out hit, distance))
            {
                //if hitting a statue object
                if (hit.collider.tag == "StatueObject")
                {
                    //get the object
                    GameObject hitObject = hit.collider.gameObject;

                    //if using the raygun sizer
                    if (raygunObjectType == "Sizer")
                    {
                        //check which sizer is selected 
                        if (raygunObject.sizer == "X" && hitObject.transform.localScale.x < 5)
                        {
                            hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x + sizerSpeed, hitObject.transform.localScale.y, hitObject.transform.localScale.z);
                        }
                        else if (raygunObject.sizer == "Y" && hitObject.transform.localScale.y < 5)
                        {
                            hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x, hitObject.transform.localScale.y + sizerSpeed, hitObject.transform.localScale.z);
                        }
                        else if (raygunObject.sizer == "Z" && hitObject.transform.localScale.z < 5)
                        {
                            hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x, hitObject.transform.localScale.y, hitObject.transform.localScale.z + sizerSpeed);
                        }
                        else if (raygunObject.sizer == "XYZ")
                        {
                            if (hitObject.transform.localScale.x < 5 && hitObject.transform.localScale.y < 5 && hitObject.transform.localScale.z < 5)
                            {
                                hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x + sizerSpeed, hitObject.transform.localScale.y + sizerSpeed, hitObject.transform.localScale.z + sizerSpeed);
                            }
                        }
                    }
                    //if using paint, change material color
                    else if (raygunObjectType == "Paint")
                    {
                        Debug.Log("Paint");
                        hitObject.GetComponent<Renderer>().material.color = raygunObject.color;
                    }
                    //if using material, change material
                    else if (raygunObjectType == "Material")
                    {
                        Debug.Log("Material");
                        Color col = hitObject.GetComponent<Renderer>().material.color;
                        hitObject.GetComponent<Renderer>().material = raygunObject.materialType;
                        hitObject.GetComponent<Renderer>().material.color = col;                    }
                    //if using wireframe, change mesh
                    else if (raygunObjectType == "Wireframe")
                    {
                        Debug.Log("Wireframe");

                    }
                }
            }
        }
    }
}
