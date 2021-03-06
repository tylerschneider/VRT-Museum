using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleRotation : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float movementDistance = 0.1f;
    public float movementSpeed = 1;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = startPos + new Vector3(0, Mathf.Sin(Time.time * movementSpeed) * movementDistance, 0);
        transform.Rotate(0, rotationSpeed, 0, Space.Self);
    }

    public void DisableSocket()
    {
        Destroy(gameObject);
    }
}
