using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StudioColorManager : MonoBehaviour
{
    public Renderer[] objects;
    public ReflectionProbe[] reflectionProbes;
    private XRSocketInteractor colorSocket;
    private XRSocketInteractor emissionSocket;
    public XRBaseInteractable previousColor;
    public XRBaseInteractable previousEmission;

    private void Awake()
    {
        BakeProbes();
    }

    void Start()
    {
        colorSocket = transform.Find("ColorPlug").GetComponent<XRSocketInteractor>();
        emissionSocket = transform.Find("EmissionPlug").GetComponent<XRSocketInteractor>();

        previousColor = colorSocket.selectTarget;
        previousEmission = emissionSocket.selectTarget;
    }

    void Update()
    {
        if(colorSocket.selectTarget != null && colorSocket.selectTarget != previousColor)
        {
            Debug.Log("Change Color");
            previousColor = colorSocket.selectTarget;
            ChangeColors(true);
        }

        if(emissionSocket.selectTarget != null && emissionSocket.selectTarget != previousEmission)
        {
            Debug.Log("Change Emission");
            previousEmission = emissionSocket.selectTarget;
            ChangeColors(false);
        }
    }

    private void ChangeColors(bool color)
    {
        foreach (Renderer obj in objects)
        {
            foreach (Material mat in obj.materials)
            {
                if (color && mat.name == "ColorMetal (Instance)")
                {
                    mat.color = colorSocket.selectTarget.GetComponent<RaygunObject>().color;
                }
                else if (!color && mat.name == "Emission (Instance)")
                {
                    mat.color = emissionSocket.selectTarget.GetComponent<RaygunObject>().color;
                    mat.SetColor("_EmissionColor", emissionSocket.selectTarget.GetComponent<RaygunObject>().color);
                    //mat.color = emissionSocket.selectTarget.GetComponent<RaygunObject>().color;
                }
            }
        }

        BakeProbes();
    }

    private void BakeProbes()
    {
        foreach (ReflectionProbe probe in reflectionProbes)
        {
            probe.RenderProbe(null);
        }
    }
}
