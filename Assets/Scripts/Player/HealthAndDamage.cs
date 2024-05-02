using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthAndDamage : MonoBehaviour
{
    [SerializeField] public float life;
    [SerializeField] private float maxLife = 100f;
    public bool invencible = false;
    [SerializeField] private float timeInvencible = 1f;
    [SerializeField] private Transform respawnWaypoint;
    [SerializeField] MovimientodelPersonaje playerMove;
    public Image lifeBar;
    private Animator animator; // Variable para el Animator

    void Start()
    {
        playerMove = GetComponent<MovimientodelPersonaje>();

        life = maxLife;
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    private void Update()
    {
        lifeBar.fillAmount = life / maxLife;
    }

    public void LostLife(float damage)
    {
        if (!invencible && life > 0)
        {
            life -= damage;
            StartCoroutine(Invulnerability());
        }

        if (life <= 0)
        {
            Respawn();
        }
    }

    IEnumerator Invulnerability()
    {
        animator.SetBool("isLostLife", true);
        playerMove.enabled = false;
        invencible = true;
        yield return new WaitForSeconds(timeInvencible);
        animator.SetBool("isLostLife", false);
                playerMove.enabled = true;
        invencible = false;

    }

    IEnumerator RespawnAfterDelay(float delay)
    {

        animator.SetBool("isDead", true);  // Iniciar la animación de muerte
        float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length; // Obtener la duración de la animación de muerte
        yield return new WaitForSeconds(delay + deathAnimationLength);
        animator.SetBool("isDead", false); // Restablecer la animación de muerte

        life = maxLife;
        invencible = false;
        gameObject.transform.position = respawnWaypoint.position;
        gameObject.transform.rotation = respawnWaypoint.rotation;
    }

    void Respawn()
    {
        if (respawnWaypoint != null)
        {
            StartCoroutine(RespawnAfterDelay(5f));
        }
    }
}



