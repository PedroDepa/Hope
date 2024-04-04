using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemyStadistics : MonoBehaviour
{
    public int lifeEnemy = 50;
    public bool enemyInvencible = false;
    public float enemyTimeInvencible = 1f;

    public Transform respawnWaypoint;

    public void LostLife(int amount)
    {
        if (!enemyInvencible && lifeEnemy > 0)
        {
            lifeEnemy -= amount;
        }

        if (lifeEnemy <= 0)
        {
            Respawnd();
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

            gameObject.transform.position = respawnWaypoint.position;
            gameObject.transform.rotation = respawnWaypoint.rotation;

            gameObject.SetActive(true);
        }
    }


    
}
