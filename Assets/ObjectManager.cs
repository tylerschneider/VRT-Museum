using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager s;
    public GameObject selectedObject;

    void Start()
    {
        if(s == null)
        {
            s = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeColor(System.Single value)
    {
        Color col = selectedObject.GetComponent<Renderer>().material.color;
        Color.RGBToHSV(col, out float h, out float s, out float v);
        h = value;
        col = Color.HSVToRGB(h, s, v);
        selectedObject.GetComponent<Renderer>().material.color = col;
    }
}
