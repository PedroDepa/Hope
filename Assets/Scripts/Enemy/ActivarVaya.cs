using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarVaya : MonoBehaviour
{
    //[SerializeField] BoxCollider activoVaya;
    [SerializeField] GameObject vayaActivada;

    void Start()
    {
        vayaActivada.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            vayaActivada.gameObject.SetActive(true);
        }
    }
}

