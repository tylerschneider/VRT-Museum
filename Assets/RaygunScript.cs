using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        if (s == null)
        {
            s = this;
        }
    }

    private void OnTriggerButtonPress()
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
                    if(raygunObject.sizer == "X" && hitObject.transform.localScale.x < 5)
                    {
                        hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x + sizerSpeed, hitObject.transform.localScale.y, hitObject.transform.localScale.z);
                    }
                    else if(raygunObject.sizer == "Y" && hitObject.transform.localScale.y < 5)
                    {
                        hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x, hitObject.transform.localScale.y + sizerSpeed, hitObject.transform.localScale.z);
                    }
                    else if(raygunObject.sizer == "Z"  && hitObject.transform.localScale.z < 5)
                    {
                        hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x, hitObject.transform.localScale.y, hitObject.transform.localScale.z + sizerSpeed);
                    }
                    else if(raygunObject.sizer == "XYZ")
                    {
                        if(hitObject.transform.localScale.x < 5 && hitObject.transform.localScale.y < 5 && hitObject.transform.localScale.z < 5)
                        {
                            hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x + sizerSpeed, hitObject.transform.localScale.y + sizerSpeed, hitObject.transform.localScale.z + sizerSpeed);
                        }
                    }
                }
                else if (raygunObjectType == "Paint")
                {
                    hitObject.GetComponent<Renderer>().material.color = raygunObject.color;
                }
                else if (raygunObjectType == "Material")
                {
                    hitObject.GetComponent<Renderer>().material = raygunObject.materialType;
                }
                else if (raygunObjectType == "Wireframe")
                {

                }
            }
        }
    }

    public void OnMainButtonPress()
    {
        //if using the raygun sizer
        if (raygunObjectType == "Sizer")
        {
            Transform sizerLabels = raygunObject.transform.Find("SizerLabels");
            Transform sizerXYZ = raygunObject.transform.Find("SizerXYZ");

            switch(raygunObject.sizer)
            {
                case "X":
                    raygunObject.sizer = "Y";
                    rotationTarget = 120;
                    break;
                case "Y":
                    raygunObject.sizer = "Z";
                    rotationTarget = 240;
                    sizerLabels.Rotate(0, 360f / 3f, 0, Space.Self);
                    break;
                case "Z":
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Raygunobject")
        {
            Debug.Log(other.gameObject.name, other.gameObject);
            Destroy(other.GetComponent<Rigidbody>());
            other.transform.parent = transform.Find("AttachPoint");
            other.transform.position = transform.Find("AttachPoint").transform.position;

            raygunObject = other.GetComponent<RaygunObject>();
            raygunObjectType = raygunObject.raygunObjectType;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other == raygunObject)
        {
            raygunObject.gameObject.AddComponent<Rigidbody>();
            raygunObject.transform.parent = null;
            raygunObject = null;
            raygunObjectType = "";
        }
    }

    private void Update()
    {
        if(raygunObjectType == "Sizer")
        {
            Transform sizerLabels = raygunObject.transform.Find("SizerLabels");

            if (rotationProgress != rotationTarget)
            {
                rotationProgress += sizerRotationSpeed;
                if (rotationProgress == 360)
                {
                    rotationProgress = 0;
                }

                sizerLabels.Rotate(0, sizerRotationSpeed, 0, Space.Self);
            }
        }

        OnTriggerButtonPress();
    }
}
