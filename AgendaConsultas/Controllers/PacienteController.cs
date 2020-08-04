using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AgendaConsultas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaConsultas.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteDAL paci;
        public PacienteController(IPacienteDAL paciente)
        {
            paci = paciente;
        }

        public IActionResult Index()
        {
            List<Paciente> listaPacientes = new List<Paciente>();
            listaPacientes = paci.GetAllPacientes().ToList();
            ViewData["Admin"] = "true";
            return View(listaPacientes);
        }

        [HttpGet]
        public IActionResult AgendaConsulta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            Paciente paciente = paci.GetPaciente(Id);

            if(paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpGet]
        public IActionResult Create(string? message)
        {
            if (message != null) ViewData["message"] = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                Paciente valid = paci.GetPacienteByEmail(paciente.Email);
                if(valid == null)
                {
                    paci.AddPaciente(paciente);
                    return RedirectToAction("index");
                }
                else
                {
                    return RedirectToAction("Create", new { message = "E-mail já foi utilizado" });
                }
            }
            return View(paciente);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            Paciente paciente = paci.GetPaciente(Id);

            if(paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, [Bind]Paciente paciente)
        {
            if(id != paciente.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                paci.UpdatePaciente(paciente);
                return RedirectToAction("Index");
            }

            return View(paciente);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Paciente paciente = paci.GetPaciente(id);

            if(paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            paci.DeletePaciente(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login(string? message)
        {
            if (message != null) ViewData["Message"] = message;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string? email, string? password)
        {
            if(email == null || password == null)
            {
                string message = "E-mail ou senha inválidos";
                return RedirectToAction("Login", new { message = message });
            }

            Paciente paciente = paci.GetPacienteByEmail(email);

            if(paciente == null)
            {
                string message = "E-mail ou senha inválidos";
                return RedirectToAction("Login", new { message = message });
            }

            if(paciente.Email != email || paciente.Password != password)
            {
                string message = "E-mail ou senha inválidos";
                return RedirectToAction("Login", new { message = message });
            }

            return RedirectToAction("Index", "Exame", new { PacienteId = paciente.Id });
        }
    }
}
