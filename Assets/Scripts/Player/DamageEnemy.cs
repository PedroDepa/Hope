using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public int amount = 10;
    private bool enemyIsTrigger = false;
    [SerializeField] private EnemyStadistics enemyStadistics;

    private void Start(){


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyIsTrigger = true;
        }

    }

       private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyIsTrigger = false;
        }

    }

    void Update(){

        if (Input.GetKeyDown(KeyCode.Q) && enemyIsTrigger)
        {
            enemyStadistics.LostLife(amount);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyIsTrigger = true;
        }

    }
}
