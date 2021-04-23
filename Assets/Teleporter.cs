using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MainCamera")
        {
            other.transform.parent.parent.position = teleportPoint.position;
            Debug.Log(teleportPoint);
        }
    }
}
