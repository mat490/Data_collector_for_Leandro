using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Jugador : MonoBehaviour
{
    public float distanciaRaycast = 1f; // Distancia del raycast
    public LayerMask capasObstaculos; // Capas que contienen obstáculos
    public LayerMask capasComida; // Capas que contienen obstáculos
    private bool hayObstaculoArriba;
    private bool hayObstaculoAbajo;
    private bool hayObstaculoIzquierda;
    private bool hayObstaculoDerecha;

    private bool hayComidaArriba;
    private bool hayComidaAbajo;
    private bool hayComidaIzquierda;
    private bool hayComidaDerecha;

    public int adIzq = 1;
    public int adDer = 1;
    public int adArr = 1;
    public int adAba = 1;
    public int pasado = 4;
    

    public int mov;
    
    private int posx;
    private int posy;    

    public bool termino;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DetectarComidaAdyacentes", 0.5f);
        Invoke("DetectarObstaculosAdyacentes", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            mov = 0; // Movimiento a la izquierda
            
            GuardarDatosEnLista();
            // Reinicio de los estados de las celdas adyacentes
            adIzq = 1;
            adDer = 1;
            adArr = 1;
            adAba = 1;

            Invoke("DetectarComidaAdyacentes", 0.5f);
            Invoke("DetectarObstaculosAdyacentes", 0.5f);

            Mover(mov);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)){
            mov = 1; // Movimiento a la derecha

            GuardarDatosEnLista();

            adIzq = 1;
            adDer = 1;
            adArr = 1;
            adAba = 1;

            Invoke("DetectarComidaAdyacentes", 0.5f);
            Invoke("DetectarObstaculosAdyacentes", 0.5f);

            Mover(mov);

        }
        else if  (Input.GetKeyDown(KeyCode.UpArrow)){
            mov = 2; // Movimiento hacia arriba

            GuardarDatosEnLista();

            adIzq = 1;
            adDer = 1;
            adArr = 1;
            adAba = 1;

            Invoke("DetectarComidaAdyacentes", 0.5f);
            Invoke("DetectarObstaculosAdyacentes", 0.5f);
           
            Mover(mov);

        }
        else if  (Input.GetKeyDown(KeyCode.DownArrow)){
            mov = 3; // Movimiento hacia abajo

            GuardarDatosEnLista();

            
            adIzq = 1;
            adDer = 1;
            adArr = 1;
            adAba = 1;
            

            Invoke("DetectarComidaAdyacentes", 0.5f);
            Invoke("DetectarObstaculosAdyacentes", 0.5f);

            Mover(mov);

        }

        if (termino){GuardarDatosEnArchivo();}
    }

    public void Mover ( int mov){
    Vector3 nuevaPosicion = transform.position;

    switch (mov)
    {
        case 0:
            nuevaPosicion.x -= 1;
            break;
        case 1:
            nuevaPosicion.x += 1;
            break;
        case 2:
            nuevaPosicion.y += 1;
            break;
        case 3:
            
            nuevaPosicion.y -= 1;
            break;
        default:
            break;
    }

    transform.position = nuevaPosicion;
    

    }

    void DetectarComidaAdyacentes(){
        // Origen del raycast en la posición del jugador
        Vector2 origen = transform.position;

        // Realizar raycast en cada dirección adyacente
        hayComidaArriba = Physics2D.Raycast(origen, Vector2.up, distanciaRaycast, capasComida);
        hayComidaAbajo = Physics2D.Raycast(origen, Vector2.down, distanciaRaycast, capasComida);
        hayComidaIzquierda = Physics2D.Raycast(origen, Vector2.left, distanciaRaycast, capasComida);
        hayComidaDerecha = Physics2D.Raycast(origen, Vector2.right, distanciaRaycast, capasComida);

        if (hayComidaArriba){
            Debug.Log("Hay comida Arriba");  
            adArr = 2;      
        }
        if (hayComidaAbajo){
            Debug.Log("Hay comida Abajo");
            adAba = 2;
        }
        if (hayComidaIzquierda){
            Debug.Log("Hay comida Izquierda");
            adIzq = 2;
        }
        if (hayComidaDerecha){
            Debug.Log("Hay comida Derecha");
            adDer = 2;
        }
    }

    void DetectarObstaculosAdyacentes()
    {
        // Origen del raycast en la posición del jugador
        Vector2 origen = transform.position;

        Debug.Log("Mov "+mov+" izq "+adIzq +" Der " + adDer +" Arr " +adArr+" Aba "+adAba );

        // Realizar raycast en cada dirección adyacente
        hayObstaculoArriba = Physics2D.Raycast(origen, Vector2.up, distanciaRaycast, capasObstaculos);
        hayObstaculoAbajo = Physics2D.Raycast(origen, Vector2.down, distanciaRaycast, capasObstaculos);
        hayObstaculoIzquierda = Physics2D.Raycast(origen, Vector2.left, distanciaRaycast, capasObstaculos);
        hayObstaculoDerecha = Physics2D.Raycast(origen, Vector2.right, distanciaRaycast, capasObstaculos);
        
        Debug.Log("Arriba "+hayObstaculoArriba+" izq "+hayObstaculoIzquierda +" Der " + hayObstaculoDerecha +" Arr " +adArr+" Aba "+hayObstaculoAbajo );

        if (hayObstaculoArriba){
            Debug.Log("Hay obsataculo Arriba");
            adArr = 0;        
        }
        if (hayObstaculoAbajo){
            Debug.Log("Hay obsataculo Abajo");
            adAba = 0;
        }
        if (hayObstaculoIzquierda){
            Debug.Log("Hay obsataculo Izquierda");
            adIzq = 0;
        }
        if (hayObstaculoDerecha){
            Debug.Log("Hay obsataculo Derecha");
            adDer = 0;
        }

    }

    private List<string> datosGuardados = new List<string>(); // Lista para almacenar los datos
    void GuardarDatosEnLista()
    {
        // Obtener la posición actual del jugador
        Vector3 posicion = transform.position;
        Debug.Log("Mov "+mov+" izq "+adIzq +" Der " + adDer +" Arr " +adArr+" Aba "+adAba );

        // Crear una cadena con los datos a guardar
        string datos = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", 
                                    posx, 
                                    posy,
                                    pasado,
                                    adIzq,
                                    adDer,
                                    adArr,
                                    adAba,
                                    mov);

        // Agregar los datos a la lista
        //datosGuardados.Add(datos);
        pasado = mov;
        datosGuardados.Insert(datosGuardados.Count, datos);
    }

    void GuardarDatosEnArchivo()
    {
        // Obtener la ruta del archivo de texto
        string rutaArchivo = Application.dataPath + "/datos.txt";

        // Escribir los datos en el archivo de texto
        using (StreamWriter writer = new StreamWriter(rutaArchivo))
        {
            foreach (string linea in datosGuardados)
            {
                writer.WriteLine(linea);
            }
        }
    }

    // Método para recibir la información de la celda
    public void RecibirMensaje(int x, int y)
    {
        posx = x;
        posy = y;
    }

    // Método para guardar los datos y terminar la ejecución
    public void terminar(){
        termino = true;
        Application.Quit();
    }
}
