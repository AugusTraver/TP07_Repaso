using Newtonsoft.Json;
namespace Tp07_Repaso.Models;

public class Usuario
{
    [JsonProperty]
    public int Id { private set; get; }
    public string username { private set; get; }
    public string password { private set; get; }
    public string nombre { private set; get; }
    public string apellido { private set; get; }
    public string foto { private set; get; }
    public DateTime ultimoLogin { private set; get; }
    public Usuario() { }

    public Usuario(string pusername, string ppassword, string pnombre, string papellido, string pfoto, DateTime pultimoLogin)
    {
        username = pusername;
        password = ppassword;
        nombre = pnombre;
        apellido = papellido;
        foto = pfoto;
        ultimoLogin = pultimoLogin;

    }
}