using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhysicalButton : MonoBehaviour
{
    public float buttonLimit = -0.03f;

    private Transform buttonTop;

    public bool openRaygunHolder;
    public bool materialMachineLeft;
    public bool materialMachineRight;
    public bool setMaterial;

    public XRSocketInteractor interactable;

    private float rotationSpeed = 1;
    private bool lerpRotation = false;
    private Transform affectedTransform;
    private MaterialMachine materialMachine;

    // Start is called before the first frame update
    void Start()
    {
        buttonTop = transform.Find("ButtonTop");

        if(openRaygunHolder)
        {
            affectedTransform = transform.parent.Find("RaygunMachineGlass");
        }
        else if(materialMachineLeft || materialMachineRight || setMaterial)
        {
            affectedTransform = transform.parent;
            materialMachine = affectedTransform.GetComponent<MaterialMachine>();
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
                if (affectedTransform.localEulerAngles.y < 270)
                {
                    affectedTransform.Rotate(0, 0, rotationSpeed, Space.Self);
                }
            }
            else
            {
                if (affectedTransform.localEulerAngles.y > 90)
                {
                    affectedTransform.Rotate(0, 0, -rotationSpeed, Space.Self);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Button")
        {
            if(openRaygunHolder)
            {
                lerpRotation = !lerpRotation;

                if(lerpRotation)
                {
                    //return objects here ?
                }
            }

            if (materialMachineLeft)
            {
                materialMachine.selectedMaterial = materialMachine.selectedMaterial - 1 >= 0 ? materialMachine.selectedMaterial - 1 : materialMachine.materials.Length - 1;
                materialMachine.right = false;
                Debug.Log(materialMachine.selectedMaterial);
            }
            if (materialMachineRight)
            {
                materialMachine.selectedMaterial = materialMachine.selectedMaterial + 1 <= materialMachine.materials.Length - 1 ? materialMachine.selectedMaterial + 1 : 0;
                materialMachine.right = true;
                Debug.Log(materialMachine.selectedMaterial);
            }

            if(setMaterial && interactable.selectTarget != null)
            {
                interactable.selectTarget.GetComponent<RaygunObject>().materialType = materialMachine.materials[materialMachine.selectedMaterial];
                Material[] matArray = interactable.selectTarget.GetComponent<Renderer>().materials;
                matArray[2] = materialMachine.materials[materialMachine.selectedMaterial];
                interactable.selectTarget.GetComponent<Renderer>().materials = matArray;
                Debug.Log("Changed to material " + materialMachine.materials[materialMachine.selectedMaterial].name);
            }
            else
            {
                Debug.Log("No material object in socket");
            }
        }
    }
}
