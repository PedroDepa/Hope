using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    public float amount = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<HealthAndDamage>().LostLife(amount);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthAndDamage>().LostLife(amount);
        }

    }
}
