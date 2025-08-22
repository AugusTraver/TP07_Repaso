using Microsoft.Data.SqlClient;
using Dapper;
namespace Tp07_Repaso.Models;
public class BD
{
    private static string _connectionString = @"Server =localHost; DataBase = TP07; Integrated Security = True;TrustServerCertificate=True;";
    public static Usuario IniciarSesion(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Usuarios WHERE username = @Username AND password = @Passwod";
            Usuario usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Username = username, Passwod = password });
            return usuario;
        }
    }
    public static bool Registrarse(Usuario usuario)
    {

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            bool SeRegistro = true;
            string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE nombre = @PNombre AND apellido = @Papellido ";
            int count = connection.QueryFirstOrDefault<int>(checkQuery, new { PNombre = usuario.nombre, Papellido = usuario.apellido });
            if (count != 0)
            {
                SeRegistro = false;
                return SeRegistro;
            }

            string query = "INSERT INTO Usuarios (username, password, nombre, apellido, foto, ultimoLogin) VALUES (@Pusername, @Ppassword, @Pnombre, @Papellido, @Pfoto, @PultimoLogin)";
            connection.Execute(query, new
            {
                Pusername = usuario.username,
                Ppassword = usuario.password,
                Pnombre = usuario.nombre,
                Papellido = usuario.apellido,
                Pfoto = usuario.foto,
                PultimoLogin = usuario.ultimoLogin
            });
            return SeRegistro;
        }

    }
    public static List<Tarea> TraerTareas(int idU)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string checkQuery = "SELECT * FROM Tareas WHERE idU = @IDU AND finalizada = 0";
            List<Tarea> Tareas = connection.Query<Tarea>(checkQuery, new { IDU = idU }).ToList();
            return Tareas;
        }
    }
   public static void CrearTarea(Tarea tarea)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string insertQuery = "INSERT INTO Tareas (titulo, descripcion, fecha, finalizada, idU) VALUES (@pTitulo, @pDescripcion, @pFecha, @pFinalizada, @pIdu)";
        connection.Execute(insertQuery, new { pTitulo = tarea.titulo, pDescripcion = tarea.descripcion, pFecha = tarea.fecha, pFinalizada = tarea.finalizada, pIdu = tarea.idU });
    }
}

    public static void EliminarTarea(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string checkQuery = "DELETE FROM Tareas WHERE id = @idu";
            connection.Query(checkQuery, new { idu = id });
        }
    }
    public static Tarea TraerTarea(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string checkQuery = "SELECT * FROM Tareas WHERE id = @idu";
            Tarea tarea = connection.QueryFirstOrDefault<Tarea>(checkQuery, new { idu = id });
            return tarea;
        }
    }
    public static void ActualizarTarea(Tarea tarea, int idTarea)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string updateQuery = "UPDATE Tareas SET titulo = @pTitulo, descripcion = @pDescripcion, fecha = @pFecha, finalizada = @pFinalizada, idU = @pIdu WHERE id = @pId";
            connection.Execute(updateQuery, new { pTitulo = tarea.titulo, pDescripcion = tarea.descripcion, pFecha = tarea.fecha, pFinalizada = tarea.finalizada, pIdu = tarea.idU, pId = idTarea });
        }
    }

    public static void FinalizarTarea(int idTarea)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET finalizada = 1 WHERE id = @idtarea";
            connection.Execute(query, new { idtarea = idTarea });
        }
    }
    public static void ActualizarFechaLogin(int idU)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string updateQuery = "UPDATE Usuarios SET ultimoLogin = GETDATE() WHERE id = @idU";
            connection.Execute(updateQuery, new { idU = idU });
        }
    }
}