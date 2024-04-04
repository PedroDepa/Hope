using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public int amount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.tag == "Enemy")
        {
            other.GetComponent<EnemyStadistics>().LostLife(amount);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.tag == "Enemy")
        {
            other.GetComponent<EnemyStadistics>().LostLife(amount);
        }

    }
}
