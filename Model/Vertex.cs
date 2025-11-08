namespace Grafos.Model {
  public enum Rol { Estudiante, Profesor, Egresado }
  public class Vertex {
    public string Id { get; }
    public string Nombre { get; set; }
    public Rol Rol { get; set; }
    public Vertex(string id,string nombre,Rol rol){ Id=id; Nombre=nombre; Rol=rol; }
  }
}
