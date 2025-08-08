using Newtonsoft.Json;
namespace Tp07_Repaso.Models;
public class Tarea
{
    [JsonProperty]
    public int Id { private set; get; }
    public string titulo { private set; get; }
    public string descripcion { private set; get; }
    public DateTime fecha { private set; get; }
    public bool finalizada { private set; get; }
    public int idU { private set; get; }
    public Tarea(string titulo, string descripcion, DateTime fecha, bool finalizada, int idU)
    {
        this.titulo = titulo;
        this.descripcion = descripcion;
        this.fecha = fecha;
        this.finalizada = finalizada;
        this.idU = idU;
    }
}
