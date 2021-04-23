using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = transform.GetChild(0).position;
    }
}
