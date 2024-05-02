using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionesJuego : MonoBehaviour
{
    [SerializeField] Canvas optionsCanvas; // Referencia al Canvas que contiene las opciones
    [SerializeField] private MovimientodelPersonaje playerMove;


    void Start()
    {
        playerMove = GetComponent<MovimientodelPersonaje>();

        if (optionsCanvas == null)
        {
            Debug.LogError("Options Canvas not assigned to OptionsMenu!");
        }
        else
        {
            optionsCanvas.enabled = false; // Inicialmente desactivamos el Canvas de opciones
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionsMenu();
            playerMove.enabled = false;

        }
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsCanvas.enabled)
        {
            playerMove.enabled = true;
        }
    }

    void ToggleOptionsMenu()
    {
        optionsCanvas.enabled = !optionsCanvas.enabled; // Cambia el estado del Canvas de opciones

    }
}
