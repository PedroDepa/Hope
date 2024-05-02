using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HealthAndDamage seReinicia; // Referencia al script HealthAndDamage del jugador

    void Start()
    {
        //if (seReinicia.life = null)
       // {
        //    Debug.LogError("Player Health script not assigned to GameManager!");
        //}
    }

    void Update()
    {
        if (seReinicia.life <= 0)
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        // Obtener el índice de la escena actual y cargarla nuevamente
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
