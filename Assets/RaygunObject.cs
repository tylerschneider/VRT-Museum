using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RaygunObject : MonoBehaviour
{
    public string raygunObjectType = "";
    public Color color;
    public Material materialType;
    public string sizer = "X";
    private LayerMask startLayer;

    private void Start()
    {
        startLayer = gameObject.layer;
    }

    private void Update()
    {
        if (GetComponent<XRBaseInteractable>().isSelected && gameObject.layer != 11)
        {
            gameObject.layer = 11;
        }
        else if (!GetComponent<XRBaseInteractable>().isSelected && gameObject.layer != startLayer)
        {
            gameObject.layer = startLayer;
        }
    }
}
