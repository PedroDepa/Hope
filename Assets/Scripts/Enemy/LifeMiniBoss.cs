using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeMiniBoss : MonoBehaviour
{
    [SerializeField] private float lifeEnemy = 100;
    [SerializeField] private float maxLifeEnemy = 100;
    [SerializeField] private bool enemyInvencible = false;
    [SerializeField] private float enemyTimeInvencible = 1f;
    //[SerializeField] private bool enemyIsTrigger = false;

    [SerializeField] private CapsuleCollider damageCollider;


    [SerializeField] private Transform respawnWaypoint;


    public Image lifeBar;
    
     private void Update()
    {
         lifeBar.fillAmount = lifeEnemy / maxLifeEnemy;
    }
    void Start()
    {   
    
        lifeEnemy = maxLifeEnemy;
        lifeBar.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(damageCollider);
            lifeBar.gameObject.SetActive(true);
        }
    }

    public void LostLife(int damagePlayer)
    {
        if (!enemyInvencible && lifeEnemy > 0)
        {
            lifeEnemy -= damagePlayer;
            // StartCoroutine(Invulnerability());
            if (lifeEnemy <= 0)
            {
            Respawnd();
            Dead();
            }
        }
    }
    void Dead()
    {
        gameObject.SetActive(false);
        lifeBar.gameObject.SetActive(false);
    }

    void Respawnd()
    {
        if (respawnWaypoint != null)
        {
            Dead();
            //gameObject.SetActive(false);
            gameObject.transform.position = respawnWaypoint.position;
            gameObject.transform.rotation = respawnWaypoint.rotation;
            //gameObject.SetActive(true);
            //lifeEnemy = maxLifeEnemy;
        }
    }


    
}

