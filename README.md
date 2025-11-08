Proyecto: Grafo Dirigido - MVC en C# (.NET Console App)

Autor: Sebastián Soto
Curso: Programación III
Profesor: Carlos Alberto Loaiza Guerrero
Universidad de Manizales

--------------------------

Descripción General

Este proyecto implementa un Grafo Dirigido utilizando el lenguaje C# bajo la arquitectura MVC (Modelo–Vista–Controlador), ejecutado en modo consola.
Permite gestionar una red de usuarios (nodos) y sus relaciones (aristas) simulando interacciones entre distintos roles: Estudiantes, Profesores y Egresados.

El sistema ofrece dos modos de inicio:

Modo interactivo: el usuario crea manualmente los nodos y relaciones desde cero.

Modo demo: carga automática de un grafo predefinido con 12 usuarios y 18 relaciones.

Incluye un CRUD funcional completo, recorridos BFS y DFS, consultas sociales y estadísticas generales.

--------------------------

Tecnologías y Entorno

Lenguaje: C# 12

Framework: .NET 9.0 Console App

IDE recomendado: Visual Studio Code

Arquitectura: MVC (Model - View - Controller)

Paradigma: Programación Orientada a Objetos

--------------------------
```
Estructura del Proyecto
📂 Grafos
 ┣ 📂 Model
 ┃ ┣ 📜 Graph.cs          # Lógica del grafo dirigido
 ┃ ┗ 📜 Vertex.cs         # Representación de un nodo (usuario)
 ┣ 📂 View
 ┃ ┗ 📜 ConsoleView.cs    # Interfaz de usuario por consola (menú e interacción)
 ┣ 📂 Controller
 ┃ ┗ 📜 GraphController.cs # Controlador principal (flujo, CRUD, lógica interactiva)
 ┗ 📜 Program.cs          # Punto de entrada principal
```
 Cada capa cumple responsabilidades separadas:

Model: gestión de datos, operaciones CRUD, recorridos y consultas.

View: interacción con el usuario mediante consola.

Controller: orquesta la comunicación entre modelo y vista.

--------------------------

Funcionalidades Principales
Módulo	                        Funcionalidad
Inicio interactivo	            Permite crear usuarios y relaciones manualmente antes de iniciar el CRUD.
Cargar demo automático	        Genera un grafo base con datos precargados (12 usuarios y 18 relaciones).
CRUD completo	                Crear, leer, actualizar y eliminar usuarios o relaciones.
Listados	                    Lista de usuarios registrados y lista de adyacencia.
Recorridos BFS y DFS	        Recorrido en anchura y profundidad con detección de ciclos.
Consultas sociales	            Usuarios sin seguidores, más influyentes y más activos.
Totales	                        Cantidad de vértices y aristas actuales.
Menú interactivo limpio	        Opción de limpiar pantalla y pausar la ejecución.

--------------------------
```
=== Bienvenido a Grafos - Modo Interactivo ===
¿Deseas INICIAR DESDE CERO y cargar datos manualmente? (S/N): S

=== Carga inicial interactiva ===
¿Cuántos usuarios deseas crear? (mínimo 1): 3
-- Usuario #1
Id (ej: A): A
Nombre: Ana
Rol (1=Estudiante, 2=Profesor, 3=Egresado): 1
-- Usuario #2
Id (ej: A): B
Nombre: Beto
Rol (1=Estudiante, 2=Profesor, 3=Egresado): 2
-- Usuario #3
Id (ej: A): C
Nombre: Caro
Rol (1=Estudiante, 2=Profesor, 3=Egresado): 3

¿Deseas agregar una relación A→B? (S/N): S
Origen (Id): A
Destino (Id): B
Alta relación: A → B
¿Deseas agregar una relación A→B? (S/N): N
```
--------------------------
```
--- MODO CRUD INTERACTIVO ---
[1] Agregar usuario (Id, Nombre, Rol)
[2] Eliminar usuario (Id)
[3] Agregar relación A->B
[4] Eliminar relación A->B
[5] Actualizar usuario (Id: cambiar Nombre/Rol)
[6] Listar adyacencia
[7] Consultas (sin seguidores / influyentes / activos)
[8] BFS (origen) y DFS completo
[9] Totales (|V|, |E|)
[10] Listar usuarios
[C] Limpiar pantalla
[0] Salir
```
--------------------------

Conceptos Aplicados

Estructuras de datos: grafo dirigido representado mediante lista de adyacencia.

Algoritmos:

Búsqueda en anchura (BFS)

Búsqueda en profundidad (DFS) con detección de ciclos

Programación orientada a objetos:

Encapsulamiento y responsabilidad por capas (MVC)

Tipos enumerados (enum Rol)

Arquitectura MVC: separación clara entre lógica, presentación y control.

Persistencia temporal: los datos existen solo durante la ejecución (no hay base de datos).

--------------------------

Requerimientos del Taller

Según el documento “Actividad Colaborativa II – Taller Integrador: Grafos Dirigidos (MVC en C#)”, el sistema debe cumplir con:

Aplicación en consola con separación MVC.

Implementación de un grafo dirigido con nodos y relaciones.

Implementación completa del CRUD.

Uso de recorridos BFS y DFS.

Consultas de análisis social (usuarios sin seguidores, influyentes y activos).

Control total del usuario sobre el ingreso de datos.

Reporte en consola del estado del grafo (adyacencia y totales).

Este proyecto cumple todos los puntos anteriores.

--------------------------
Autor

Sebastián Soto
Estudiante de Ingeniería de Sistemas y Telecomunicaciones
Universidad de Manizales
