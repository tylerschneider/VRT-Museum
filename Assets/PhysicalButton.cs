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
    public bool statueFinalizer;
    public bool colorSpawner;

    public XRSocketInteractor interactable;

    public GameObject raygun;

    public GameObject statueBase;
    public GameObject statueBasePrefab;

    public GameObject colorPrefab;
    public Transform colorSpawnpoint;
    public DialObject colorDial;
    public DialObject saturationDial;
    public DialObject brightnessDial;

    private float rotationSpeed = 1;
    private bool lerpRotation = false;
    private Transform affectedTransform;
    private MaterialMachine materialMachine;
    private Vector3 startPos;
    private Quaternion startRot;

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
        else if(statueFinalizer)
        {
            startPos = statueBase.transform.position;
            startRot = statueBase.transform.rotation;
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

        if(statueFinalizer)
        {
            if(statueBase.GetComponent<XRGrabInteractable>())
            {
                if(statueBase.GetComponent<XRGrabInteractable>().selectingInteractor != null)
                {
                    statueBase = Instantiate(statueBasePrefab, startPos, startRot);
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
                Debug.Log("Pressed");
                lerpRotation = !lerpRotation;
                raygun.layer = LayerMask.NameToLayer("Raygun");

                if(lerpRotation)
                {
                    if(!raygun.GetComponent<XRGrabInteractable>().isSelected)
                    {
                        raygun.transform.position = raygun.GetComponent<RaygunScript>().startPos;
                    }
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
            else if(setMaterial)
            {
                Debug.Log("No material object in socket");
            }

            if(statueFinalizer)
            {
                if(statueBase.transform.childCount != 0)
                {
                    foreach(Transform child in statueBase.transform)
                    {
                        Destroy(child.GetComponent<XRGrabInteractable>());
                        Destroy(child.GetComponent<StatueObject>());
                    }

                    Destroy(statueBase.GetComponent<StatueObject>());
                    statueBase.AddComponent<XRGrabInteractable>().interactionLayerMask = LayerMask.NameToLayer("StatueObject");
                    statueBase.GetComponent<Rigidbody>().isKinematic = false;
                }

            }

            if(colorSpawner)
            {
                GameObject go = Instantiate(colorPrefab, colorSpawnpoint.position, colorSpawnpoint.rotation);
                Color col = Color.HSVToRGB(colorDial.value, saturationDial.value, brightnessDial.value);
                go.GetComponent<RaygunObject>().color = col;
                Material[] matArray = go.GetComponent<Renderer>().materials;
                matArray[1].color = col;
            }
        }
    }
}
