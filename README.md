# Recopilación de datos con Unity para el Agente Leandro V2

### Motivación

Implementar un escenario en el motor gráfico Unity, utilizado para la creación de
videojuegos, para recopilar datos de entrenamiento para entrenar a Leandro y sea capaz de resolver el problema del agente triangulo.

### Elementos del proyecto

Dentro de la carpeta principal Assets se encuentran las carpetas:

- **Prefabs**
- **Scenes**
- **Scripts**
- **TextMeshPro**

### Prefabs

Los elementos contenidos en la carpeta Prefabs son:

**Obstáculos y comida**

Los obstáculos son cuadrados violeta, estos se encuentran en la capa “capasObstaculo”, y tienen un RigidBody 2D:

![Untitled 1](./Recopilación de datos con Unity para el Agente Le a74ea777e0bd438cb7bf9f9883f1432c/Untitled 1.PNG "Untitled 1")

La comida es un circulo verde, estas se encuentran en la capa “capasComida”, tienen un RigidBody 2D y un Circle Collider 2D marcado como trigger:

![Untitled](./imagenes/Untitled.png "Untitled")

**Celdas y limite**

El limite es un cuadrado naranja ubicado en la capa “capasObstaculo”:

![Untitled](Recopilacio%CC%81n%20de%20datos%20con%20Unity%20para%20el%20Agente%20Le%20a74ea777e0bd438cb7bf9f9883f1432c/Untitled%202.png)

Las celdas son cuadrados blancos y negros con un Box Collider 2D y el script celdaPos que envía al jugador la posición de la celda que pisa:

![Untitled](Recopilacio%CC%81n%20de%20datos%20con%20Unity%20para%20el%20Agente%20Le%20a74ea777e0bd438cb7bf9f9883f1432c/Untitled%203.png)

### Scenes

Guarda las escenas del proyecto que en este caso es solo una “SampleScene”.

### Scripts

Guarda todas las scripts utilizadas en el proyecto:

**celdaPos.cs**

Este script permite a cada celda almacenar y comunicar su posición en la cuadrícula del juego.

Primero, se importan las bibliotecas necesarias.

Luego, en la clase "celdaPos", se definen dos variables públicas, `pos_x` y `pos_y`. Estas almacenan la posición de la celda en los ejes x e y, respectivamente.

El método `Start()` se ejecuta cuando el juego comienza. Dentro de este método, las variables `pos_x` y `pos_y` se establecen a la posición `x` y `y` del objeto al que está adjunto el script. La posición se convierte a un entero utilizando el operador de conversión `(int)`.

El método `OnTriggerEnter2D(Collider2D collision)` se ejecuta cuando un objeto 2D ingresa en el área de colisión de la celda. El parámetro `collision` contiene información sobre el objeto que colisionó con la celda.

Dentro de este método, se verifica si el objeto que colisionó tiene la etiqueta "Player", que se supone que es el jugador en el juego.

Si el objeto es el jugador, el script accede al componente "Jugador" del objeto, que es otro script asociado al jugador. Luego, este script del jugador recibe un mensaje que incluye la posición `x` y `y` de la celda, a través del método `RecibirMensaje(pos_x, pos_y)`. Esto permite al jugador conocer la posición de la celda en la que se encuentra actualmente.

**Jugador.cs**

El script se inicia importando las bibliotecas necesarias para su funcionamiento. Luego, se definen varias variables públicas y privadas que almacenarán información sobre el estado del Jugador y su entorno, como la distancia del raycast, las capas de obstáculos y comida, etc.

El método `Start()` se ejecuta al principio del juego. Aquí, se invocan los métodos `DetectarComidaAdyacentes()` y `DetectarObstaculosAdyacentes()` después de un retraso de 0.5 segundos para permitir que los datos se actualicen y no se vean modificados por los raycasts.

El método `Update()` se ejecuta en cada fotograma del juego. Aquí, se verifica si se ha pulsado alguna tecla de dirección (arriba, abajo, izquierda, derecha) y se realiza el movimiento correspondiente. Cada vez que se realiza un movimiento, se reinician los estados de las celdas adyacentes y se invocan de nuevo los métodos para detectar comida y obstáculos adyacentes.

El método `Mover()` toma un entero como argumento que representa la dirección del movimiento. Dependiendo de este argumento, el jugador se moverá hacia la izquierda, la derecha, arriba o abajo.

Los métodos `DetectarComidaAdyacentes()` y `DetectarObstaculosAdyacentes()` utilizan raycasts para detectar si hay comida u obstáculos en las celdas adyacentes al Jugador.

El método `GuardarDatosEnLista()` guarda información sobre el estado actual del jugador y su entorno en una lista. Esta información incluye la posición actual del jugador, el movimiento que ha realizado, y si hay comida u obstáculos en las celdas adyacentes.

Finalmente, el método `GuardarDatosEnArchivo()` guarda la información recopilada en un archivo de texto.

Además de las funciones ya mencionadas, el script también incluye el método `RecibirMensaje(int x, int y)`, que se utiliza para actualizar las coordenadas `x` e `y` del jugador. Este método se podría usar, por ejemplo, para actualizar la posición del jugador basada en la entrada de otros scripts o eventos del juego.

El método `terminar()` es un método público que se usa para finalizar la ejecución del juego. Cuando se llama a este método, el booleano `termino` se establece en `true` y luego se llama al método `Application.Quit()` para cerrar la aplicación. Sin embargo, es importante mencionar que `Application.Quit()` no tiene efecto en el editor de Unity o en aplicaciones web. En su lugar, se usa principalmente en aplicaciones de escritorio.

**Generador.cs**

Este código se encarga de generar obstáculos y comida en posiciones aleatorias dentro de un rango especificado.

Primero, se definen una serie de variables públicas para almacenar los prefabs de los obstáculos y la comida, la cantidad de cada uno que se va a generar, y los rangos de inicio y fin dentro de los cuales se generarán. También se definen una serie de variables privadas para almacenar las posiciones generadas y la posición actual del jugador.

Al iniciar el juego (`Start()`), se obtiene la posición inicial del jugador. Luego, se llama al método `GenerarElementos()` para generar los obstáculos y la comida.

`GenerarElementos()` es un método que genera una cantidad específica de obstáculos y comida llamando al método `GenerarElemento()` con el prefab correspondiente.

`GenerarElemento()` es un método que genera una instancia de un elemento (ya sea obstáculo o comida) en una posición aleatoria válida. Una posición es válida si no es la misma que la del jugador y si no ha sido generada anteriormente. Si se encuentra una posición válida, se crea la instancia del elemento y se agrega la posición a la lista de posiciones generadas.

`GenerarPosicionAleatoria()` es un método que genera una posición aleatoria dentro del rango especificado.

**comidaScript.cs**

Este script le da la funcionalidad a los objetos comida.

`void Start()` y `void Update()` son métodos predeterminados proporcionados por Unity. `Start()` se llama al inicio del primer frame y `Update()` se llama antes de cada frame. Actualmente, no se ha definido ninguna funcionalidad en estos dos métodos en este script.

El método `OnTriggerEnter2D(Collider2D collision)` se llama cuando otro objeto entra en un "trigger" (o un área de colisión marcada como trigger) de este objeto de comida. El objeto que entró en el trigger se pasa al método como el parámetro `collision`.

Dentro de `OnTriggerEnter2D()`, se verifica si el objeto que entró en el trigger tiene la etiqueta "Player". Si es así, se imprime un mensaje de registro con la ubicación de la comida y luego se destruye el objeto de comida con `Destroy(gameObject)`. Esto simula que el jugador "come" la comida al entrar en contacto con ella.

La línea comentada al final sugiere que el script del jugador podría ser accedido para realizar alguna acción cuando la comida es "comida", pero actualmente no se está realizando ninguna acción de este tipo.

### Escenario creado

Para simular el escenario del Agente Triangulo se creo una rejilla de 8x8 que donde se generan un limite, el agente triangulo, obstáculos y comida. En la esquina superior derecha encontramos un botón para guardar los datos una vez terminada la secuencia de movimientos del usuario, los obstáculos y la comida se generan aleatoriamente en los limites pero el agente siempre se genera en las coordenadas (-3,3) esto con el fin de limitar los escenarios posibles y los datos de entrenamiento no omitan demasiados escenarios:

![Untitled](Recopilacio%CC%81n%20de%20datos%20con%20Unity%20para%20el%20Agente%20Le%20a74ea777e0bd438cb7bf9f9883f1432c/Untitled%204.png)

### Agente Triangulo

Es controlado por el usuario con las teclas de navegación (flechitas). Siempre se genera en las coordenadas (-3,3), tiene el tag “Player”, un sprite de triangulo nativo de Unity con un color rojo asignado, contiene la script principal “Jugador.cs”, un RigidBody 2D y un Circle Collider 2D marcado como trigger:

![Untitled](Recopilacio%CC%81n%20de%20datos%20con%20Unity%20para%20el%20Agente%20Le%20a74ea777e0bd438cb7bf9f9883f1432c/Untitled%205.png)

![Untitled](Recopilacio%CC%81n%20de%20datos%20con%20Unity%20para%20el%20Agente%20Le%20a74ea777e0bd438cb7bf9f9883f1432c/Untitled%206.png)

### Generador de obstaculos y comida

Es un empty Game Object que contiene la script “Generador.cs” y por lo tanto es el encargado de generar los obstáculos y la comida en el mapa:

![Untitled](Recopilacio%CC%81n%20de%20datos%20con%20Unity%20para%20el%20Agente%20Le%20a74ea777e0bd438cb7bf9f9883f1432c/Untitled%207.png)

### Manera en la que se almacenan los datos

**Los movimiento, son las clases de los datos de entrenamiento (Movimiento decidido) y están definidos por los siguientes valores:**
Izquierda = 0
Derecha = 1
Arriba = 2
Abajo = 3
No hay movimiento anterior = 4

**Los estados de las celdas adyacentes están definidos por los siguientes valores:**
Obstáculo/límite = 0
Libre = 1
Comida = 2

Los datos recopilados son almacenados en un archivo .txt al presionar el botón “Guardar” (es importante refrescar la escena en Unity con “ctrl + r” para que los datos se actualicen. La apariencia de los datos almacenados es la siguiente:

**x, y, movPasado, estadoIzq, estadoDer, estadoArr, estadoAba, movDecidido**
3, -3,     4 ,                 0,                1,               0,               1,               1

![Untitled](Recopilacio%CC%81n%20de%20datos%20con%20Unity%20para%20el%20Agente%20Le%20a74ea777e0bd438cb7bf9f9883f1432c/Untitled%208.png)
