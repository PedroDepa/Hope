using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;


public class EnemyStadistics : MonoBehaviour
{
    [SerializeField] private float lifeEnemy = 50;
    [SerializeField] private float maxLifeEnemy = 50;
    [SerializeField] private bool enemyInvencible = false;
    [SerializeField] private float enemyTimeInvencible = 1f;

    [SerializeField] private Transform respawnWaypoint;

    
    void Start()
    {
        lifeEnemy = maxLifeEnemy;
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
            gameObject.SetActive(false);
            gameObject.transform.position = respawnWaypoint.position;
            gameObject.transform.rotation = respawnWaypoint.rotation;
            gameObject.SetActive(true);
            lifeEnemy = maxLifeEnemy;
        }
    }


    
}
