using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActivarVida : MonoBehaviour
{
    public Image lifeBar;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            lifeBar.gameObject.SetActive(true);
        }
    }
}
