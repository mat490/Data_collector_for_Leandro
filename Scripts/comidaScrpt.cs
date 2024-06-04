using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comidaScrpt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que colisionó es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Comida en" + transform.position.x + "," + transform.position.y);
            Destroy(gameObject);
            // Acceder al script del jugador (cambia "NombreDelScriptDelJugador" al nombre real de tu script)
            
        }
    }
}
