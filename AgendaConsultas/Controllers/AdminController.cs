using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaConsultas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaConsultas.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminDAL iadmin;
        public AdminController(IAdminDAL admin)
        {
            iadmin = admin;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string? email, string password)
        {
            if (email == null || password == null)
            {
                string message = "E-mail ou senha inválidos";
                return RedirectToAction("Login", new { message = message });
            }

            Admin admin = iadmin.GetAdminByEmail(email);

            if (admin == null)
            {
                string message = "E-mail ou senha inválidos";
                return RedirectToAction("Login", new { message = message });
            }

            if (admin.email != email || admin.password != password)
            {
                string message = "E-mail ou senha inválidos";
                return RedirectToAction("Login", new { message = message });
            }

            return RedirectToAction("Index", "DataDisponivel");
        }
    }
}
