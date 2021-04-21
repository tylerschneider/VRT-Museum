using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMachine : MonoBehaviour
{
    public Material[] materials;
    public int selectedMaterial = 0;
    public float rotationSpeed;
    private Transform inner;
    public bool right = true;

    private void Start()
    {
        inner = transform.Find("MaterialMachineInner");
    }

    private void Update()
    {
        float targetRot = (360 / (materials.Length)) * selectedMaterial;

        if(Mathf.Round(inner.localEulerAngles.z) != targetRot)
        {
            if(right)
            {
                inner.Rotate(0, 0, rotationSpeed);
            }
            else
            {
                inner.Rotate(0, 0, -rotationSpeed);
            }
        }
        else
        {
            inner.localEulerAngles = new Vector3(0, 0, targetRot);
        }
    }
}
