using Microsoft.Data.SqlClient;
using Dapper;
namespace Tp07_Repaso.Models;
public class BD
{
    private static string _connectionString = @"Server =localHost; DataBase = TP07; Integrated Security = True; TrustServer Certificate = True;";
    public static Usuario IniciarSesion(string username, string password)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE username = @Username AND password = @Password";
            usuario = connection.QueryFirstOrDefault(query, new { Username = username, Password = password });
        }
        return usuario;
    }
    public static bool Registrarse(Usuario usuario)
    {

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            bool SeRegistro = true;
            string checkQuery = "SELECT COUNT(*) FROM Integrante WHERE nombre = @PNombre AND apellido = Papellido ";
            int count = connection.QueryFirstOrDefault<int>(checkQuery, new { PNombre = usuario.nombre, Papellido = usuario.apellido });
            if (count > 0)
            {
                SeRegistro = false;
                return SeRegistro;
            }
            else
            {
                SeRegistro = true;
            }
            string query = "INSERT INTO Usuarios (username, password, nombre, apellido, foto, ultimoLogin) VALUES (@Pusername, @Ppassword, @Pnombre, @Papellido, @Pfoto, @PultimoLogin)";
            connection.Execute(query, new
            {
                ussername = usuario.username,
                ppassword = usuario.password,
                nombre = usuario.nombre,
                apellido = usuario.apellido,
                foto = usuario.foto,
                ultimoLogin = usuario.ultimoLogin
            });
            return SeRegistro;
        }
    }
}