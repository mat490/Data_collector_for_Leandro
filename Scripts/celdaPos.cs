using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celdaPos : MonoBehaviour
{
    public int pos_x;
    public int pos_y;
   

    void Start()
    {
        pos_x = (int)transform.position.x;
        pos_y = (int)transform.position.y;
    }
 

  private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que colisionó es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
        
            // Acceder al script del jugador 
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();

            // Enviar el mensaje al jugador
            jugador.RecibirMensaje(pos_x,pos_y);
        }
        
    }
}
