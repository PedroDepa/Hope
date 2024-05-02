using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] private HealthAndDamage playerStadistics;
    [SerializeField] private MovimientodelPersonaje playerMove;
    [SerializeField] private DamageEnemy damagEnemy;
    private Animator animator;
    private bool isRightClicking = false;
    private bool isCooldown = false;
    private bool isHoldingRightClick = false;
    private float extraCooldownTime = 0.5f; // Tiempo extra de cooldown después de llegar a cero
    public float cooldownDuration; // Duración del cooldown en segundos
    public float maxCooldownDuration;

    [SerializeField] private Slider sliderEnergy;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<MovimientodelPersonaje>();
        sliderEnergy.maxValue = maxCooldownDuration;
        sliderEnergy.value = maxCooldownDuration;
    }

    void Update()
    {
        if (!isCooldown && Input.GetMouseButtonDown(1)) // Agregar !isCooldown para asegurarse de que no esté en cooldown
        {
            isHoldingRightClick = true; // Indicar que se está manteniendo presionado el click derecho
            playerMove.enabled = false;
            isRightClicking = true;
            animator.SetBool("IsInvensible", true);
            playerStadistics.invencible = true;
            damagEnemy.enabled = false;
        }

        if (Input.GetMouseButtonUp(1) && isHoldingRightClick)
        {
            isHoldingRightClick = false; // Indicar que se dejó de mantener presionado el click derecho
            StartCoroutine(ActivateShieldCooldown()); // Iniciar el cooldown
        }

        if (Input.GetMouseButtonUp(1) && !isHoldingRightClick)
        {
            playerMove.enabled = true;
            isRightClicking = false;
            animator.SetBool("IsInvensible", false);
            playerStadistics.invencible = false;
            damagEnemy.enabled = true;
        }
    }

    IEnumerator ActivateShieldCooldown()
    {
        isCooldown = true;
        float timer = cooldownDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            sliderEnergy.value = timer;
            yield return null;
        }
        yield return new WaitForSeconds(extraCooldownTime); // Esperar tiempo extra
        isCooldown = false; // Reiniciar el cooldown
        sliderEnergy.value = maxCooldownDuration; // Asegurarse de que el slider vuelva al máximo
    }
}




