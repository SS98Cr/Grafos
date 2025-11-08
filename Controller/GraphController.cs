using Grafos.View;
using Grafos.Model;
using System.Collections.Generic;

namespace Grafos.Controller {
  public class GraphController {
    private readonly ConsoleView view = new ConsoleView();
    private readonly Graph graph = new Graph();

    public void Run() {
      view.Title("Bienvenido a Grafos - Modo Interactivo");

      // 1) Setup inicial
      if (view.AskYesNo("¿Deseas INICIAR DESDE CERO y cargar datos manualmente? (S/N): ")) {
        SetupInteractivoInicial();
      } else if (view.AskYesNo("¿Deseas cargar el DEMO automático predefinido? (S/N): ")) {
        CargarDemo();
      } else {
        view.Info("Iniciando con grafo vacío.");
      }

      // 2) Resumen
      if (graph.VerticesCount > 0) {
        view.Subtitle("Lista de adyacencia");
        view.PrintAdjacency(graph.AdjAsText());
      } else {
        view.Info("Aún no hay datos en el grafo.");
      }
      view.Info($"Total de usuarios: {graph.VerticesCount} | Total de relaciones: {graph.EdgesCount}");
      view.Pause();

      // 3) Menú CRUD interactivo
      while (true) {
        view.ShowMainMenu();
        string op = view.Ask("\nElige una opción: ").Trim().ToUpperInvariant();
        if (op == "0") break;
        if (op == "C") { view.Clear(); continue; }

        switch (op) {
          case "1": { // Agregar usuario
            string id = AskIdNuevo("Id: ");
            string nombre = view.Ask("Nombre: ");
            Rol rol = view.AskRol();
            graph.UpdateUser(id, nombre, rol); // cambia TMP por datos reales
            view.Log($"Alta usuario: {id} - {nombre} ({rol})");
            break;
          }

          case "2": { // Eliminar usuario
            string id = AskIdExistente("Id a eliminar: ");
            // (Opcional) Confirmación
            if (!graph.RemoveVertex(id))
              view.Error("No se pudo eliminar el usuario (no existe o referencia interna inconsistente).");
            else
              view.Log($"Eliminado usuario: {id}");
            break;
          }

          case "3": { // Agregar relación
            string a = AskIdExistente("Origen (Id): ");
            string b = AskIdExistente("Destino (Id): ");
            if (a == b) { view.Error("No se permiten relaciones A→A."); break; } // opcional
            if (!graph.AddEdge(a, b))
              view.Error($"No se pudo agregar {a}→{b} (verifica existencia y duplicados).");
            else
              view.Log($"Alta relación: {a} → {b}");
            break;
          }

          case "4": { // Eliminar relación
            string a = AskIdExistente("Origen (Id): ");
            string b = AskIdExistente("Destino (Id): ");
            if (!graph.RemoveEdge(a, b))
              view.Error("No existe esa relación.");
            else
              view.Log($"Eliminada relación: {a} → {b}");
            break;
          }

          case "5": { // Actualizar usuario
            string id = AskIdExistente("Id a actualizar: ");
            string nuevoNombre = view.AskOptional("Nuevo nombre (deja vacío para no cambiar): ");
            bool cambiarRol = view.AskYesNo("¿Cambiar rol? (S/N): ");
            Rol? nuevoRol = null;
            if (cambiarRol) nuevoRol = view.AskRol();

            if (graph.UpdateUser(id, string.IsNullOrWhiteSpace(nuevoNombre) ? null : nuevoNombre, nuevoRol))
              view.Log($"Actualización usuario: {id}");
            else
              view.Error("No se pudo actualizar usuario.");
            break;
          }

          case "6": { // Listar adyacencia
            view.PrintAdjacency(graph.AdjAsText());
            break;
          }

          case "7": { // Consultas
            view.PrintConsulta("Usuarios sin seguidores", graph.UsuariosSinSeguidores());
            view.PrintConsulta("Usuarios más influyentes", graph.UsuariosInfluyentes());
            view.PrintConsulta("Usuarios más activos", graph.UsuariosMasActivos());
            break;
          }

          case "8": { // BFS + DFS
            string origen = AskIdExistente("BFS desde (Id): ");
            var (orden, alcanzados) = graph.BFS(origen);
            view.PrintBFS(origen, orden, alcanzados);

            var (dfsOrden2, hayCiclo2) = graph.DFSCompleto();
            view.PrintDFS(dfsOrden2, hayCiclo2);
            break;
          }

          case "9": { // Totales
            view.PrintTotales(graph.VerticesCount, graph.EdgesCount);
            break;
          }

          case "10": { // Listar usuarios
            var users = graph.GetAllUsers();
            view.ShowUserList(users);
            break;
          }

          default:
            view.Error("Opción inválida.");
            break;
        }

        view.Pause();
      }

      view.Info("Fin de la ejecución. ¡Gracias!");
    }

    // ======= Setup interactivo inicial =======
    private void SetupInteractivoInicial() {
      view.Title("Carga inicial interactiva");

      int n = view.AskIntMin("¿Cuántos usuarios deseas crear? (mínimo 1): ", 1);
      for (int i = 1; i <= n; i++) {
        view.Subtitle($"Usuario #{i}");
        string id = AskIdNuevo("Id (ej: A): ");
        string nombre = view.Ask("Nombre: ");
        Rol rol = view.AskRol();
        graph.UpdateUser(id, nombre, rol);
      }

      view.Subtitle("Relaciones dirigidas");
      while (view.AskYesNo("¿Deseas agregar una relación A→B? (S/N): ")) {
        string a = AskIdExistente("Origen (Id): ");
        string b = AskIdExistente("Destino (Id): ");
        if (a == b) { view.Error("No se permiten relaciones A→A."); continue; } // opcional
        if (!graph.AddEdge(a, b)) {
          view.Error($"No se pudo agregar {a}→{b} (verifica existencia y duplicados).");
        } else {
          view.Info($"Alta relación: {a} → {b}");
        }
      }

      view.Subtitle("Resumen inicial");
      view.PrintAdjacency(graph.AdjAsText());
      view.PrintTotales(graph.VerticesCount, graph.EdgesCount);
      view.Pause();
    }

    // ======= Demo automático =======
    private void CargarDemo() {
      var usuarios = new[] {
        ("A", "Ana", Rol.Estudiante), ("B", "Beto", Rol.Profesor), ("C", "Caro", Rol.Egresado),
        ("D", "Dani", Rol.Profesor), ("E", "Elena", Rol.Estudiante), ("F", "Fede", Rol.Profesor),
        ("G", "Gina", Rol.Egresado), ("H", "Hugo", Rol.Estudiante), ("I", "Iris", Rol.Profesor),
        ("J", "Juan", Rol.Egresado), ("K", "Karla", Rol.Estudiante), ("L", "Leo", Rol.Profesor)
      };
      foreach (var (id, nombre, rol) in usuarios)
        graph.AddVertex(new Vertex(id, nombre, rol));

      var relaciones = new[] {
        ("A","B"), ("A","C"), ("A","D"), ("A","E"),
        ("D","E"), ("D","F"), ("D","G"), ("D","H"),
        ("B","C"), ("C","A"),
        ("E","F"), ("F","G"), ("G","H"), ("H","E"),
        ("K","A"), ("K","D"), ("L","A"), ("L","K")
      };
      foreach (var (a, b) in relaciones)
        graph.AddEdge(a, b);
    }

    // ======= Helpers de entrada =======
    private string AskIdNuevo(string prompt) {
      while (true) {
        string id = view.Ask(prompt).ToUpperInvariant();
        if (!graph.AddVertex(new Vertex(id, "TMP", Rol.Estudiante))) {
          view.Error($"El Id '{id}' ya existe. Intenta otro.");
          continue;
        }
        return id;
      }
    }

    private string AskIdExistente(string prompt) {
      while (true) {
        string id = view.Ask(prompt).ToUpperInvariant();
        if (graph.GetAllUsers().Exists(u => u.Id == id))
          return id;
        view.Error("No existe usuario con ese Id.");
      }
    }
  }
}