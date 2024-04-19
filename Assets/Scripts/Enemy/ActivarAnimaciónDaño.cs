using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimaciónDaño : MonoBehaviour
{
    bool playerInArea;
    bool enemyAttackOn;

    void Start()
    {
        
    }

    void OnTriggerStay()
    {
        playerInArea = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInArea && !enemyAttackOn)
        {
            StartCoroutine(StartAttack());
        }
    }

    private IEnumerator StartAttack()
    {
        enemyAttackOn = true;
        //canAttack = false; // Evitar nuevos ataques mientras se realiza uno
        //damageCollider.enabled = true;
        //animacion ataque
        yield return new WaitForSeconds(0.5f); // Tiempo que la animación de ataque dura
        enemyAttackOn = false;
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.5f); // Resto de la duración de la animación
        //IsAttacking = false;
        //animator.SetBool("IsAttacking", IsAttacking);
        //damageCollider.enabled = false;
        //canAttack = true; // Permitir nuevos ataques una vez que termine la animación
    }
}
