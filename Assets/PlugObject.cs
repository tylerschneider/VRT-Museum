using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugObject : MonoBehaviour
{
    public RaygunObject raygunObject;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Raygunobject")
        {
            Debug.Log(other.gameObject.name, other.gameObject);
            Destroy(other.GetComponent<Rigidbody>());
            other.transform.parent = transform.Find("AttachPoint");
            other.transform.position = transform.Find("AttachPoint").transform.position;

            raygunObject = other.GetComponent<RaygunObject>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == raygunObject)
        {
            raygunObject.gameObject.AddComponent<Rigidbody>();
            raygunObject.transform.parent = null;
            raygunObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
