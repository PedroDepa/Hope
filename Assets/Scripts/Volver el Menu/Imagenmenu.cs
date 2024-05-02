using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imagenmenu : MonoBehaviour
{
    [SerializeField] private LifeMiniBoss lifeMiniBoss;
    [SerializeField] private Canvas canvasMenu; // Variable para el Canvas
    [SerializeField] private MovimientodelPersonaje playerMove;

    void Start()
    {
        canvasMenu.enabled = false; // Inicialmente desactivamos el Canvas


    }

    void Update()
    {
        if (lifeMiniBoss.estaVivo)
        {
            canvasMenu.enabled = true; // Activar el Canvas cuando el mini boss está vivo
            playerMove.enabled = false;
        }
    }
}
