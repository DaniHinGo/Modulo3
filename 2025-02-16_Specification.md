# Project Katrina

## Arquitectura
Simple, 3 capas

### Backend 
Controladores, Business, Data, Tests.

## Abstract 
---
Diseñar una biblioteca para secretos para contar. Esta biblioteca digital debe tener la capacidad de enumerar todos los libros disponibles, buscarlos y poder leerlos bien sea en línea o descargarlos.

## Definición de Katrina

### Modulo de Autores
el módulo de autores define los distintos autores que están en la biblioteca. Deben tener:
''' Typescript

interface Autor {
    "Nombre" : string; // Nombres del autor
    "Apellido" : string; // Apellidos del Autor
    "Peudonimos": string; // Otros nombres que utiliza el autor
    "Fecha de Nacimiento": Date // yyyy-mm-dd ISO 80903
    "País" : string; // Pais de Nacimiento
    "Nacionalidad": string; // nacionalidad registrada
    "isAlive": bool; // Si está vivo o no
    "Fecha de muerte": int; // En que fecha murio, por defecto 0
    "Idiomas": string; // En que idiomas escribía originalmente.
    "Generos": String; // Que generos literarios escribía
    "Biografia": string; // Un breve resumen de la vida y obra del autor
    "Galardones": string; // Que galardones recibió.
    "isEnabled": bool; // Si el autor está activo
}

#### Condiciones
* Nombre apellido, Fecha de nacimiento, país no pueden estar vacíos.
* la fecha de nacimiento no puede ser mayor al año corriente.
* La fecha debe ir en ISO80903 yyyy-mm-dd
* Sí isAlive es true, fecha de muerte no puede ser 0.
* Los idiomas deben utilizar la nomenclatura de código de país (EN, ES, DE, JP, PT, FR, IT, RU, UK) lista separada por comas
* Generos deben ser una lista separada por comas

### Acciones
* Crear un autor
* Editar un autor
* Eliminar un autor. No eliminamos de la base de datos por que no podemos tener libros huérfanos. Desactivamos el autor para que no aparezca en las búsquedas.
* Búsquedas
  * Buscar por nombre y apellido de manera inclusiva
  * Buscar por género
  * Buscar por año
  * Buscar por Nacionalidad
  * Buscar por Idioma
  * Buscar todo al mismo tiempo.
  * Buscar si está vivo

### Módulo de Libros
''' Typescript

interface Libros
{
    "Título": string; // titulo impreso del libro
    "ISBN13" : string; // Formato 978-0593081655
    "Editorial": String;
    "Año de publicación": int;
    "Formato": string; // que tipo de archivo es.
    "Género": string // lista separada por comas de los géneros.
    "Idioma": string;// lista separada por comas
    "portada": string; // PATH
    "Edición": string; 
    "ContraPortada" : string; // Synopsis/Descripción/Resumen sin spoilers del libro
    "AuthorID": int // Quien es el autor.
}

* El título no puede estar vacío
* El ISBN debe ser correcto
* El año de publicación no puede ser mayor a 2026 y menor a 1450
* Editorial no puede estar vacía
* Idioma es una lista de lenguajes por código oficial separada por comas.
* Contraportada no debe tener más de 500 palabras (1500 carácteres).
* El autor tiene que existir. Si no viola el FK
* En principio, la portada va vacía. Luego se convertira en un PATH de la imagen de mi libro.

### Acciones
* Agreagar un libro
* Eliminar un libro.
* Editar un libro
* Descargar el libro
* Leer en línea
* Búsqueda
    * Buscar por nombre
    * Buscar por autor
    * Buscar por género
    * Buscar por idioma
    * Buscar por formato
    * Buscar por año de publicación
    * Buscar por editorial
    * Buscar el ISBN13

### Módulo de Audiolibros
---
''' Typescript

interface Audiolibros
{
    "Titulo": string;
    "AuthorId": int
    "Genero": string;
    "NarradorId": int;
    "Longitud o Duración": string - int; 
    // 17 Horas 29 Minutos, 17:29, 17'29" '    
    // // Horas y minutos 60 segundos -> 1 minuto, 60 minutos -> 1 hora, 24 horas -> 1 día, 365 días -> 1 año, 5 años -> lustro, 10 años -> década, 100 años -> siglo, y 1000 años -> milenio
    "Tamaño" : int // Megabytes se redondea hacia arriba.
    "Path" : string;
}

* Nombre no puede estar vacío.
* El author tiene que existir
* El narrador tiene que existir
* El tamaño es en megabytes, 1 megabyte es 1024 kbs.
* longitud va a depender de la unidad
    * Si es un string, simplemente escriben cuanto dura.
    * Si es un integer: van a guardar minutos, y calculan cuantas horas y minutos son.

### Módulo de Administración

Modulo donde se agregan los libros, se editan los libros, autores y usuarios.

* Estadísticas del sitio (Crear una tabla, donde se mida cuantas veces se descargó un libro, cuantas veces fue escuchado, y cuantas veces fue leído) (ChartJS)
* Yo puedo reiniciar la clave de los usuarios
* Yo puedo desactivar o registrar un usuario
* yo puedo activar o desactivar un autor
* Puedo subir, crear, editar, y eliminar libros, audiolibros y autores.

### PRUEBAS UNITARIAS DE TODO EL BACKEND.

## Frontend 

Una página por acción con el servicio de datos inyectado en tiempo de ejecución

* login
* La página principal (a decisión de uds)
* Librería
    * Últmos libros enumerados en orden cronológico inverso
    * Búsqueda por título
    * Búsqueda por autor
    * Búsqueda por género
    * Búsqueda por fecha
    * Búsqueda por editorial
    * Mirar la lirería completa en orden cronológico levogiro, inverso y alfábetico y alfabético inverso.
    * Ver detalle de un libro
        * el detalle debe tener el autor, la reseña del libro, y un link pra descargar el PDF o leerlo en línea.
        * Al hacer click en el autor debe llevarme a la página de detalle del autor.

* Autor

* Últimos autores en orden cronológico inverso
* Búsqueda por nombre y/o apellido
* Búsqueda por país
* búsqueda por Género
* Mirar todos los autores en orden alfabético.
* Ver detalle del autor
    * Nombre, apellido, fecha de nacimiento, lugar de nacimiento y una lista cronológica inversa de todos sus libros.
    * Al hacer click en el libro, debe llevarme al detalle del libro como tal.

* Audiolibros

* últimos libros en orden cronológico inverso
* Búsqueda por nombre
* ver detalle del libro
    * Nombre del libro
    * Longitud en horas
    * un link para descargar o escuchar en línea.
    * Otros libros del mismo autor.
    * Detalle del autor como en libros.

* Administración

* Enumerar mis usuarios
* buscar usuario por nombre o correcto
* Detalle usuario
    * Cambiar la clave
    * Activar o desactivar mi usuario
* enumerar los libros, Audiolibros
    * Administrar mis libros
    * Cambiar nombre, subir el PDF, y el Audio.
* Enumerar autores
    * Adminisrtrar autores
    * NO SE DEBE BORRAR UN AUTOR.
    * Desactivar mi autor para que no salga en la búsqueda.

# [PICK ANGULAR OR REMIX]