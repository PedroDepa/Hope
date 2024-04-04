using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public Transform interactionZone;

    void Update()
    {
        if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickObjects>().isPickable == true && PickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickedObject = ObjectToPickUp;
                PickedObject.GetComponent<PickObjects>().isPickable = false;
                PickedObject.transform.SetParent(interactionZone);
                PickedObject.transform.position = interactionZone.position;
                PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;

            }
        }
        else if (PickedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickedObject.GetComponent<PickObjects>().isPickable = true;
                PickedObject.transform.SetParent(null);
                PickedObject.GetComponent<Rigidbody>().useGravity = true;
                PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                PickedObject = null;
            }
        }
        
        
    }

}
