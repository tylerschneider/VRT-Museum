using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnedObject;
    public GameObject prefab;

    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        //if the last spawn object leaves the area, spawn another
        if(other.gameObject == spawnedObject)
        {
            //if a paint object, delete the collider that helps it stand after falling
            if (spawnedObject.GetComponent<RaygunObject>())
            {
                if(spawnedObject.GetComponent<RaygunObject>().raygunObjectType == "Paint")
                {
                    Destroy(spawnedObject.GetComponent<BoxCollider>());
                }
            }
            spawnedObject = Instantiate(prefab, transform.position, prefab.transform.rotation);
            spawnedObject.name = prefab.name;
        }
    }
}
