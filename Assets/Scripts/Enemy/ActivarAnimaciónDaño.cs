using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimaciónDaño : MonoBehaviour
{
    bool playerInArea;
    bool enemyAttackOn;

    private Animator animator;
    private bool canAttack = true; // Variable para controlar si se puede atacar

    [SerializeField] private EnemyMove enemyMove;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) // Cambiar de OnTriggerStay a OnTriggerEnter para detectar la colisión al entrar en el área
    {
        if(other.CompareTag("Player")) // Verificar colisión con el jugador
        {
            enemyMove.enabled = false;
            canAttack = true; // Evitar nuevos ataques mientras se realiza uno
            playerInArea = true;
            StartAttackCoroutine();
            
        }
    }


    void OnTriggerStay(Collider other)
    {
         if(other.CompareTag("Player")) // Restablecer cuando el jugador sale del área
        {
            enemyMove.enabled = false;
            canAttack = true; // Evitar nuevos ataques mientras se realiza uno
            playerInArea = true;
            StartAttackCoroutine();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) // Restablecer cuando el jugador sale del área
        {
            enemyMove.enabled = true;
            playerInArea = false;
            enemyAttackOn = false;
            StopAllCoroutines();
            animator.SetBool("isAttacking", false); // Asegurarse de que la animación de ataque se detenga
        }
    }

    private void StartAttackCoroutine()
    {
        if (playerInArea && !enemyAttackOn && canAttack)
        {
            StartCoroutine(StartAttack());
        }
    }

    private IEnumerator StartAttack()
    {
        enemyAttackOn = true;
        canAttack = false; // Evitar nuevos ataques mientras se realiza uno
        
        animator.SetBool("isAttacking", true); // Activar la animación de ataque
        yield return new WaitForSeconds(2f); // Tiempo que la animación de ataque dura

        enemyAttackOn = false;
        animator.SetBool("isAttacking", false); // Desactivar la animación de ataque
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 2f); // Resto de la duración de la animación

        //canAttack = true; // Permitir nuevos ataques una vez que termine la animación
    }
}
