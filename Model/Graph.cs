using System;
using System.Collections.Generic;
using System.Linq;
namespace Grafos.Model {
  public class Graph {
  public int VerticesCount => vertices.Count;
  public int EdgesCount => adj.Sum(kv => kv.Value.Count);
    // Devuelve todos los usuarios registrados
    public List<Vertex> GetAllUsers() {
      return vertices.Values.ToList();
    }
    public Dictionary<string, Vertex> vertices = new();
    public Dictionary<string, List<string>> adj = new();
    public Dictionary<string, int> indegree = new();

    // Agrega un vértice si no existe
    public bool AddVertex(Vertex v) {
      if (vertices.ContainsKey(v.Id)) return false;
      vertices[v.Id] = v;
      adj[v.Id] = new List<string>();
      indegree[v.Id] = 0;
      return true;
    }

    // Elimina un vértice y todas las aristas incidentes
    public bool RemoveVertex(string id) {
      if (!vertices.ContainsKey(id)) return false;
      // Eliminar aristas entrantes
      foreach (var kv in adj) {
        kv.Value.Remove(id);
      }
      // Eliminar aristas salientes y actualizar indegree
      if (adj.ContainsKey(id)) {
        foreach (var dest in adj[id]) {
          if (indegree.ContainsKey(dest)) indegree[dest]--;
        }
      }
      adj.Remove(id);
      indegree.Remove(id);
      vertices.Remove(id);
      return true;
    }

    // Agrega una arista si ambos vértices existen y no es duplicada
    public bool AddEdge(string a, string b) {
      if (!vertices.ContainsKey(a) || !vertices.ContainsKey(b)) return false;
      if (adj[a].Contains(b)) return false;
      adj[a].Add(b);
      indegree[b]++;
      return true;
    }

    // Elimina una arista si existe
    public bool RemoveEdge(string a, string b) {
      if (!vertices.ContainsKey(a) || !vertices.ContainsKey(b)) return false;
      if (!adj[a].Contains(b)) return false;
      adj[a].Remove(b);
      indegree[b]--;
      return true;
    }

    // Actualiza nombre y/o rol de un usuario
    public bool UpdateUser(string id, string? nuevoNombre = null, Rol? nuevoRol = null) {
      if (!vertices.ContainsKey(id)) return false;
      if (nuevoNombre != null) vertices[id].Nombre = nuevoNombre;
      if (nuevoRol != null) vertices[id].Rol = nuevoRol.Value;
      return true;
    }

    // Grado de entrada
    public int InDegree(string id) {
      return indegree.ContainsKey(id) ? indegree[id] : 0;
    }

    // Grado de salida
    public int OutDegree(string id) {
      return adj.ContainsKey(id) ? adj[id].Count : 0;
    }

    // BFS desde un origen
    public (List<string> orden, int alcanzados) BFS(string origen) {
      var visitados = new HashSet<string>();
      var orden = new List<string>();
      if (!vertices.ContainsKey(origen)) return (orden, 0);
      var q = new Queue<string>();
      q.Enqueue(origen);
      visitados.Add(origen);
      while (q.Count > 0) {
        var u = q.Dequeue();
        orden.Add(u);
        foreach (var v in adj[u]) {
          if (!visitados.Contains(v)) {
            visitados.Add(v);
            q.Enqueue(v);
          }
        }
      }
      return (orden, orden.Count);
    }

    // DFS completo y detección de ciclo
    public (List<string> orden, bool hayCiclo) DFSCompleto() {
      var color = new Dictionary<string, int>(); // 0: blanco, 1: gris, 2: negro
      foreach (var v in vertices.Keys) color[v] = 0;
      var orden = new List<string>();
      bool ciclo = false;
      void DFS(string u) {
        color[u] = 1;
        orden.Add(u);
        foreach (var v in adj[u]) {
          if (color[v] == 0) DFS(v);
          else if (color[v] == 1) ciclo = true;
        }
        color[u] = 2;
      }
      foreach (var v in vertices.Keys)
        if (color[v] == 0) DFS(v);
      return (orden, ciclo);
    }

    // Usuarios sin seguidores (in-degree == 0)
    public List<string> UsuariosSinSeguidores() {
      var res = new List<string>();
      foreach (var v in vertices.Keys)
        if (indegree[v] == 0) res.Add(v);
      return res;
    }

    // Usuarios más influyentes (máximo in-degree)
    public List<string> UsuariosInfluyentes() {
      int max = -1;
      foreach (var v in vertices.Keys)
        if (indegree[v] > max) max = indegree[v];
      var res = new List<string>();
      foreach (var v in vertices.Keys)
        if (indegree[v] == max) res.Add(v);
      return res;
    }

    // Usuarios más activos (máximo out-degree)
    public List<string> UsuariosMasActivos() {
      int max = -1;
      foreach (var v in vertices.Keys)
        if (adj[v].Count > max) max = adj[v].Count;
      var res = new List<string>();
      foreach (var v in vertices.Keys)
        if (adj[v].Count == max) res.Add(v);
      return res;
    }

    // ¿b es alcanzable desde a?
    public bool Alcanzable(string a, string b) {
      var (orden, _) = BFS(a);
      return orden.Contains(b);
    }

    // Lista de adyacencia como texto
    public string AdjAsText() {
      var lines = new List<string>();
      foreach (var kv in adj.OrderBy(k => k.Key)) {
        var dests = kv.Value.Count == 0 ? "-" : string.Join(", ", kv.Value);
        lines.Add($"{kv.Key} -> {dests}");
      }
      return string.Join(Environment.NewLine, lines);
    }
  }
}
