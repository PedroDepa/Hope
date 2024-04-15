using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private int amount = 10;
    private bool enemyIsTrigger = false;
    [SerializeField] private EnemyStadistics enemyStadistics;

    private Animator animator;

      private bool IsAttack = true;

    private void Start(){

        animator = GetComponent<Animator>();

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

        animator.SetBool("IsAttack", IsAttack);
        if (Input.GetKeyDown(KeyCode.Q) && enemyIsTrigger)
        {
            IsAttack = true;
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
