using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyStadistics : MonoBehaviour
{
    [SerializeField] private float lifeEnemy;
    [SerializeField] private float maxLifeEnemy = 50;
    [SerializeField] private Slider sliderEnemy;
    [SerializeField] private bool enemyInvencible = false;
    [SerializeField] private float enemyTimeInvencible = 1f;

    [SerializeField] private Transform respawnWaypoint;

    
    void Start()
    {
        lifeEnemy = maxLifeEnemy;
        
    }

    private void Update()
    {
        sliderEnemy.value = lifeEnemy;
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
