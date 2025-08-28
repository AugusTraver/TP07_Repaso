using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp07_Repaso.Models;

namespace Tp07_Repaso.Controllers;

public class AccountController : Controller
{
    public IActionResult DLogin()
    {
        return View("Login");
    }
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        Usuario usuario = BD.IniciarSesion(username, password);
        if (usuario == null)
        {
            return View("Login");
        }
        else{
        HttpContext.Session.SetString("usuario", usuario.Id.ToString());
        BD.ActualizarFechaLogin(usuario.Id);
        return RedirectToAction("CargarTareas","Home");
        }
    }

    [HttpPost]
    public IActionResult DRegistrarse()
    {
        return View("Registrar");
    }

    [HttpPost]
    public IActionResult Registrarse(
    string username,
    string password,
    string nombre,
    string apellido,
    IFormFile fotoFile)
{
    string nombreFoto = null;

    if (fotoFile != null && fotoFile.Length > 0)
    {
        string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagenes");
        Directory.CreateDirectory(carpeta);

        nombreFoto = Path.GetFileName(fotoFile.FileName);
        string ruta = Path.Combine(carpeta, nombreFoto);

        using (var stream = new FileStream(ruta, FileMode.Create))
        {
            fotoFile.CopyTo(stream);
        }
    }

    Usuario usu = new Usuario(username, password, nombre, apellido, nombreFoto, DateTime.Now);

    bool pudo = BD.Registrarse(usu);

    if (pudo)
    {
        return RedirectToAction("DLogin","Account");
    }
    else
    {
        ViewBag.pudo = pudo;
        return View("Registrar","Account");
    }
}

}
