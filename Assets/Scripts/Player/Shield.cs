using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] private bool isInvulnerable = false;
    //[SerializeField] private float timeInvencible = 1f;

    [SerializeField] private HealthAndDamage playerStadistics;

    //private Animator animator;
    [SerializeField] private MovimientodelPersonaje playerMove;
    void Start()
    {
        //animator = GetComponent<Animator>();
        playerMove = GetComponent<MovimientodelPersonaje>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //GetComponent<Animator>().SetBool("isProtecting", true);
            playerStadistics.enabled = false;
            playerMove.enabled = false;
            isInvulnerable = true;
            StartCoroutine(tiempoEscudo());
        }
        if (Input.GetMouseButtonUp(1))
        {
            isInvulnerable = false;
            //GetComponent<Animator>().SetBool("isProtecting", false);
            playerStadistics.enabled = true;
            playerMove.enabled = true;
            isInvulnerable = false;

        }
    }

    IEnumerator tiempoEscudo()
    {
        yield return new WaitForSeconds(0.5f);
        isInvulnerable = false;
    }

   
}
