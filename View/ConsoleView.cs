using System;
using System.Collections.Generic;
using Grafos.Model;
namespace Grafos.View {
  public class ConsoleView {
    // Helpers avanzados para entrada y validación
    public bool AskYesNo(string prompt) {
      while (true) {
        Console.Write(prompt);
        var input = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
        if (input == "" || input == "N") return false;
        if (input == "S") return true;
        Console.WriteLine("Por favor responde S o N.");
      }
    }

    public string Ask(string prompt) {
      while (true) {
        Console.Write(prompt);
        var input = (Console.ReadLine() ?? "").Trim();
        if (!string.IsNullOrEmpty(input)) return input;
        Console.WriteLine("El valor no puede estar vacío.");
      }
    }

    public string AskOptional(string prompt) {
      Console.Write(prompt);
      return (Console.ReadLine() ?? "").Trim();
    }

    public int AskIntMin(string prompt, int min) {
      while (true) {
        Console.Write(prompt);
        var input = (Console.ReadLine() ?? "").Trim();
        if (int.TryParse(input, out int val) && val >= min) return val;
        Console.WriteLine($"Ingresa un número entero mayor o igual a {min}.");
      }
    }

    public Rol AskRol(string prompt = "Rol (1=Estudiante, 2=Profesor, 3=Egresado): ") {
      while (true) {
        Console.Write(prompt);
        var input = (Console.ReadLine() ?? "").Trim();
        if (input == "1" || input.Equals("Estudiante", StringComparison.OrdinalIgnoreCase)) return Rol.Estudiante;
        if (input == "2" || input.Equals("Profesor", StringComparison.OrdinalIgnoreCase)) return Rol.Profesor;
        if (input == "3" || input.Equals("Egresado", StringComparison.OrdinalIgnoreCase)) return Rol.Egresado;
        Console.WriteLine("Opción inválida. Intenta de nuevo.");
      }
    }

    public void Clear() {
      Console.Clear();
      Console.WriteLine("Pantalla limpiada.\n");
    }
    public void Title(string t) => Console.WriteLine($"\n=== {t} ===");
    public void Subtitle(string t) => Console.WriteLine($"\n-- {t} --");
    public void Info(string t) => Console.WriteLine(t);
    public void Error(string t) => Console.WriteLine($"[ERROR] {t}");
    public void PrintAdjacency(string t) => Console.WriteLine(t);

    // Menú principal interactivo
    public void ShowMainMenu() {
      Console.WriteLine("\n--- MODO CRUD INTERACTIVO ---");
      Console.WriteLine("[1] Agregar usuario (Id, Nombre, Rol)");
      Console.WriteLine("[2] Eliminar usuario (Id)");
      Console.WriteLine("[3] Agregar relación A->B");
      Console.WriteLine("[4] Eliminar relación A->B");
      Console.WriteLine("[5] Actualizar usuario (Id: cambiar Nombre/Rol)");
      Console.WriteLine("[6] Listar adyacencia");
      Console.WriteLine("[7] Consultas (sin seguidores / influyentes / activos)");
      Console.WriteLine("[8] BFS (origen) y DFS completo");
      Console.WriteLine("[9] Totales (|V|, |E|)");
      Console.WriteLine("[10] Listar usuarios");
      Console.WriteLine("[C] Limpiar pantalla");
      Console.WriteLine("[0] Salir");
    }
    public void ShowUserList(List<Grafos.Model.Vertex> users) {
      if (users == null || users.Count == 0) {
        Console.WriteLine("No hay usuarios registrados.");
        return;
      }
      Console.WriteLine("\n=== Usuarios registrados ===");
      Console.WriteLine($"{"ID",-5} {"Nombre",-20} {"Rol"}");
      Console.WriteLine(new string('-', 40));
      foreach (var u in users)
        Console.WriteLine($"{u.Id,-5} {u.Nombre,-20} {u.Rol}");
    }
    // Limpiar pantalla
    public void ClearScreen() {
      Console.Clear();
    }


    // Pausa
    public void Pause() {
      Console.WriteLine("Presiona ENTER para continuar...");
      Console.ReadLine();
    }

    // Helpers para imprimir listas
    public void PrintList(string titulo, List<string> items) {
      Console.WriteLine($"{titulo}: {string.Join(", ", items)}");
    }
    public void PrintPairs(string titulo, List<(string, string)> pares) {
      Console.WriteLine(titulo);
      foreach (var (a, b) in pares) Console.WriteLine($"- {a} -> {b}");
    }

    // Imprime el resultado de BFS
    public void PrintBFS(string origen, List<string> orden, int alcanzados) {
      Console.WriteLine($"\nBFS desde {origen}: {string.Join(", ", orden)}");
      Console.WriteLine($"Alcanzados: {alcanzados}");
    }

    // Imprime el resultado de DFS
    public void PrintDFS(List<string> orden, bool hayCiclo) {
      Console.WriteLine($"\nDFS completo: {string.Join(", ", orden)}");
      Console.WriteLine($"¿Ciclo detectado?: {(hayCiclo ? "Sí" : "No")}");
    }

    // Imprime una consulta de usuarios
    public void PrintConsulta(string titulo, List<string> usuarios) {
      Console.WriteLine($"{titulo}: {string.Join(", ", usuarios)}");
    }

    // Imprime si un usuario es alcanzable desde otro
    public void PrintAlcanzable(string a, string b, bool alcanzable) {
      Console.WriteLine($"¿{b} es alcanzable desde {a}?: {(alcanzable ? "Sí" : "No")}");
    }

    // Imprime totales
    public void PrintTotales(int vertices, int aristas) {
      Console.WriteLine($"Total de usuarios: {vertices}");
      Console.WriteLine($"Total de relaciones: {aristas}");
    }

    // Log de operaciones CRUD
    public void Log(string mensaje) {
      Console.WriteLine(mensaje);
    }
  }
}
