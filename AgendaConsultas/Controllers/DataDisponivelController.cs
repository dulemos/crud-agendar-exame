using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaConsultas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaConsultas.Controllers
{
    public class DataDisponivelController : Controller
    {
        private readonly IDataDisponivelDAL data;
        public DataDisponivelController(IDataDisponivelDAL dataDisponivel)
        {
            data = dataDisponivel;
        }
        public IActionResult Index()
        {
            List<DataDisponivel> listaDatasDisponiveis = new List<DataDisponivel>();
            listaDatasDisponiveis = data.GetAllDatasDisponiveis().ToList();
            ViewData["Admin"] = "true";
            return View(listaDatasDisponiveis);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] DataDisponivel dataDisponivel)
        {
            if (ModelState.IsValid)
            {
                data.AddDataDisponivel(dataDisponivel);
                return RedirectToAction("Index");
            }
            return View(dataDisponivel);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) NotFound();
            DataDisponivel dataDisponivel = data.GetDataDisponivel(id);
            if (dataDisponivel == null) NotFound();
            return View(dataDisponivel);
        }   
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null) NotFound();
            DataDisponivel dataDisponivel = data.GetDataDisponivel(id);
            if (dataDisponivel == null) NotFound();
            return View(dataDisponivel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, [Bind]DataDisponivel dataDisponivel)
        {
            if (id != dataDisponivel.Id) NotFound();
            if (ModelState.IsValid)
            {
                data.UpdateDataDisponivel(dataDisponivel);
                return RedirectToAction("Index");
            }
            return View(dataDisponivel);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) NotFound();
            DataDisponivel dataDisponivel = data.GetDataDisponivel(id);
            if (dataDisponivel == null) NotFound();
            return View(dataDisponivel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            data.DeleteDataDisponivel(id);
            return RedirectToAction("Index");
        }
    }
}
