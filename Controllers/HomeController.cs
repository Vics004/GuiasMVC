using Microsoft.AspNetCore.Mvc;
using PracticaMVC.Models;
using PracticaMVC.Servicios;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PracticaMVC.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly ILogger<HomeController> _logger;
        private readonly equiposDbContext _context;

        
        public HomeController(ILogger<HomeController> logger, equiposDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Autenticacion]

        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var tipoUsuario = HttpContext.Session.GetString("TipoUsuario");
            var nombreUsuario = HttpContext.Session.GetString("Nombre");

            if (usuarioId == null) 
            { 
                return RedirectToAction("Autenticar", "Home");
            }

            ViewBag.nombre = nombreUsuario;
            ViewData["tipoUsuario"] = tipoUsuario;
            return View();
        }

        public IActionResult Autenticar() 
        {
            ViewData["ErrorMessage"] = "";
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Autenticar(string txtUsuario, string txtClave)
        {
            var usuario = (from u in _context.usuarios
                           where u.email == txtUsuario 
                           && u.contrasenia == txtClave
                           && u.activo == "S"
                           && u.bloqueado == "N" select u).FirstOrDefault();

            if (usuario != null) 
            {
                HttpContext.Session.SetInt32("UsuarioId", usuario.id_usuario);
                HttpContext.Session.SetString("TipoUsuario", usuario.tipo_usuario);
                HttpContext.Session.SetString("Nombre", usuario.nombre);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ErrorMessage"] = "Error, Usuario invalido!!!!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
