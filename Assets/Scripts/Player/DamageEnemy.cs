using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private int amount = 10;
    private bool enemyIsTrigger = false;
    [SerializeField] private EnemyStadistics enemyStadistics;
    [SerializeField] private LifeMiniBoss miniBossStadistics;
    private bool IsAttacking = false;
    private Animator animator;
    [SerializeField] private BoxCollider damageCollider;
    private bool canAttack = true; // Nueva variable para controlar si se puede realizar el ataque

    private void Start()
    {
        animator = GetComponent<Animator>();
        damageCollider.enabled = false;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyIsTrigger = true;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack) // Agregar la condición para verificar si se puede atacar
        {
            IsAttacking = true;
            activeAttack();
            StartCoroutine(DealDamage());
        }
    }

    private void activeAttack()
    {
        animator.SetBool("IsAttacking", IsAttacking);
    }

    private IEnumerator DealDamage()
    {
        canAttack = false; // Evitar nuevos ataques mientras se realiza uno
        damageCollider.enabled = true;
        yield return new WaitForSeconds(0.5f); // Tiempo que la animación de ataque dura
        if (enemyIsTrigger)
        {
            enemyStadistics.LostLife(amount);
            miniBossStadistics.LostLife(amount);
        }
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.5f); // Resto de la duración de la animación
        IsAttacking = false;
        animator.SetBool("IsAttacking", IsAttacking);
        damageCollider.enabled = false;
        canAttack = true; // Permitir nuevos ataques una vez que termine la animación
    }
}
