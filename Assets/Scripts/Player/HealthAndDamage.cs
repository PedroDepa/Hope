using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndDamage : MonoBehaviour
{
    public int life = 100;
    public bool invencible = false;
    public float timeInvencible = 1f;

    public Transform respawnWaypoint;
   
    void Start(){
        
    }

    public void LostLife(int calidad)
    {
        if (!invencible && life > 0)
        {
            life -= calidad;
            StartCoroutine(Invulnerability());
        }

        if (life <= 0)
        {
            Respawn();
        } 
    }

    IEnumerator Invulnerability()
    {
        invencible = true;
        yield return new WaitForSeconds(timeInvencible);
        invencible = false;
    }

  
    void Dead()
    {
        gameObject.SetActive(false);
    }

    void Respawn()
    {
        if (respawnWaypoint != null)
        {
            Dead();
            gameObject.transform.position = respawnWaypoint.position;
            gameObject.transform.rotation = respawnWaypoint.rotation;
            //GameObject newObject = Instantiate(gameObject, respawnWaypoint.position, respawnWaypoint.rotation);
            gameObject.SetActive(true);

        }
    }
}
