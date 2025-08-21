using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp07_Repaso.Models;

namespace Tp07_Repaso.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        return RedirectToAction("DLogin","Account");
    }
    public IActionResult CargarTareas()
    {
        int idU = int.Parse(HttpContext.Session.GetString("usuario"));
        ViewBag.Tareas = BD.TraerTareas(idU);
        return View("Tareas","Home");
    }
    [HttpPost]
    public IActionResult DCrearTarea()
    {
        return View("CrearTarea");
    }
    [HttpPost]
    public IActionResult CrearTarea(string titulo, string descripcion, bool finalizada)
    {
        int idU = int.Parse(HttpContext.Session.GetString("usuario"));
        Tarea tarea = new Tarea(titulo, descripcion, DateTime.Now, finalizada, idU);
        BD.CrearTarea(tarea);
        return RedirectToAction("CargarTareas");
    }
    public IActionResult FinalizarTarea(int idTarea)
    {
        BD.FinalizarTarea(idTarea);
        return RedirectToAction("CargarTareas");
    }
    public IActionResult EliminarTarea(int idTarea)
    {
        BD.EliminarTarea(idTarea);
        return RedirectToAction("CargarTareas");
    }
    public IActionResult DActualizarTarea()
    {
        return View("ActualizarTarea");
    }
    public IActionResult ActualizarTarea(string titulo, string descripcion, DateTime fecha, bool finalizada)
    {
        int idU = int.Parse(HttpContext.Session.GetString("usuario"));
        Tarea tarea = new Tarea(titulo, descripcion, fecha, finalizada, idU);
        BD.ActualizarTarea(tarea);
        return RedirectToAction("CargarTareas");
    }
}
