using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    public GameObject obstaculoPrefab; // Prefab del obstáculo
    public GameObject comidaPrefab; // Prefab de la comida
    public int cantidadObstaculos; // Cantidad total de obstáculos
    public int cantidadComida; // Cantidad total de comida
    public Vector2Int rangoInicio; // Inicio del rango de coordenadas
    public Vector2Int rangoFin; // Fin del rango de coordenadas

    private List<Vector2Int> posicionesGeneradas = new List<Vector2Int>(); // Lista para almacenar las posiciones generadas
    private Vector2Int posicionJugador; // Posición actual del jugador

    void Start()
    {
        // Obtener la posición inicial del jugador
        GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObj != null)
        {
            posicionJugador = new Vector2Int((int)jugadorObj.transform.position.x, (int)jugadorObj.transform.position.y);
        }

        GenerarElementos();
    }

    void GenerarElementos()
    {
        // Generar obstáculos
        for (int i = 0; i < cantidadObstaculos; i++)
        {
            GenerarElemento(obstaculoPrefab);
        }

        // Generar comida
        for (int i = 0; i < cantidadComida; i++)
        {
            GenerarElemento(comidaPrefab);
        }
    }

    void GenerarElemento(GameObject prefab)
    {
        // Generar elemento hasta encontrar una posición válida
        while (true)
        {
            // Generar una posición aleatoria dentro del rango
            Vector2Int posicion = GenerarPosicionAleatoria();

            // Verificar si la posición es la misma que la del jugador
            if (posicion == posicionJugador)
            {
                // Si es la misma que la del jugador, continuar para generar en otra posición
                continue;
            }

            // Verificar si la posición ya ha sido generada anteriormente
            if (!posicionesGeneradas.Contains(posicion))
            {
                // Si la posición no está en la lista, crear el elemento en esa posición
                Instantiate(prefab, new Vector3(posicion.x, posicion.y, 0), Quaternion.identity);

                // Agregar la posición a la lista de posiciones generadas
                posicionesGeneradas.Add(posicion);

                // Salir del bucle ya que se generó el elemento
                break;
            }
        }
    }

    Vector2Int GenerarPosicionAleatoria()
    {
        // Generar coordenadas aleatorias dentro del rango y convertirlas a enteros
        int posX = Random.Range(rangoInicio.x, rangoFin.x + 1);
        int posY = Random.Range(rangoInicio.y, rangoFin.y + 1);

        return new Vector2Int(posX, posY);
    }
}
