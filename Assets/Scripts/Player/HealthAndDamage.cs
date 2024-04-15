using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthAndDamage : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float maxLife = 100f;
    [SerializeField] private bool invencible = false;
    [SerializeField] private float timeInvencible = 1f;
    [SerializeField] private Transform respawnWaypoint;

    public Image lifeBar;
   
   
    void Start(){

        life = maxLife;
        //life = Mathf.Clamp(life, 0, 100);//No permite que se modifique la vida+
     
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
            life = maxLife;
            invencible = false;
            gameObject.transform.position = respawnWaypoint.position;
            gameObject.transform.rotation = respawnWaypoint.rotation;
            //GameObject newObject = Instantiate(gameObject, respawnWaypoint.position, respawnWaypoint.rotation);
            gameObject.SetActive(true);
        }
    }
}
