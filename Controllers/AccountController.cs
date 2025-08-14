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
        HttpContext.Session.SetString("usuario", usuario.Id.ToString());
        BD.ActualizarFechaLogin(usuario.Id);
        return RedirectToAction("CargarTareas");
    }

    [HttpPost]
    public IActionResult DRegistrarse()
    {
        return View("Registrarse");
    }

    [HttpPost]
    public IActionResult Registrarse(string username, string password, string nombre, string apellido, string foto, DateTime ultimoLog)
    {
        Usuario usu = new Usuario(username, password, nombre, apellido, foto, ultimoLog);
        bool pudo = BD.Registrarse(usu);
        if (pudo == true)
        {
            return RedirectToAction("CargarTareas");
        }
        else
        {
            ViewBag.pudo = pudo;
            return View("Registrarse");
        }
    }

}
