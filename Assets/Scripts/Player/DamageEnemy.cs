using System.Collections;
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

    bool leftFist;

    GameObject enemy;
    private bool canAttack = true; // Nueva variable para controlar si se puede realizar el ataque


    
    private void Start()
    {
        animator = GetComponent<Animator>();
        damageCollider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("Boss"))
        {
            enemyIsTrigger = false;
            enemy = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyIsTrigger = true;
            enemy = other.gameObject;
        }
         if (other.gameObject.CompareTag("Boss"))
        {
            enemyIsTrigger = true;
            enemy = other.gameObject;
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
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); //No hago animacion :3
        //animator.SetBool("ataquederecho", ataquederecho);

    }

    private void Damage(GameObject enemyRef)
    {
        if(enemyRef.CompareTag("Enemy"))
        {
            enemyRef.GetComponent<EnemyStadistics>().LostLife(amount);

        }else if(enemyRef.CompareTag("Boss"))
        {
            enemyRef.GetComponent<LifeMiniBoss>().LostLife(amount);
        }
    }

    private IEnumerator DealDamage()
    {
        canAttack = false; // Evitar nuevos ataques mientras se realiza uno
        damageCollider.enabled = true;
        yield return new WaitForSeconds(0.5f); // Tiempo que la animación de ataque dura
        if (enemyIsTrigger)
        {
            Damage(enemy);
        }
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.5f); // Resto de la duración de la animación
        IsAttacking = false;
        animator.SetBool("IsAttacking", IsAttacking);
        damageCollider.enabled = false;
        canAttack = true; // Permitir nuevos ataques una vez que termine la animación
    }
}
