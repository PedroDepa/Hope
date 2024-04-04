using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PickObjects : MonoBehaviour
{
    public bool isPickable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RecolectorItems")
        {
            other.GetComponentInParent<PickUpObjects>().ObjectToPickUp = this.gameObject;
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "RecolectorItems")
        {
            other.GetComponentInParent<PickUpObjects>().ObjectToPickUp = null;
        }
    }

}
