using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public void SpawnObject(GameObject go)
    {
        ObjectManager.s.selectedObject = Instantiate(go);
    }
}
