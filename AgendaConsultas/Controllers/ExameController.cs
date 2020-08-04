using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaConsultas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaConsultas.Controllers
{
    public class ExameController : Controller
    {
        private readonly IExameDAL exam;
        private readonly IDataDisponivelDAL data;
        private readonly IPacienteDAL paci;
        public ExameController(IExameDAL exame, IDataDisponivelDAL dataDisp, IPacienteDAL paciente)
        {
            exam = exame;
            data = dataDisp;
            paci = paciente;
        } 
        public IActionResult Index(int? PacienteId)
        {
            if (PacienteId == null) RedirectToAction("Login", "Paciente");
            List<Exame> listaExames = new List<Exame>();
            int pacienteId = Convert.ToInt32(PacienteId);
            listaExames = exam.GetAllExamesByPacienteId(pacienteId).ToList();
            Paciente paciente = paci.GetPaciente(PacienteId);
            ViewData["PacienteNome"] = paciente.Nome;
            ViewData["pacienteId"] = paciente.Id;
            List<string> datas = new List<string>();
            foreach(var item in listaExames)
            {
                DataDisponivel dataDisponivel = data.GetDataDisponivel(item.DataId);
                datas.Add(dataDisponivel.Data.ToString());
            }
            ViewBag.datas = datas;
            return View(listaExames);
        }

        [HttpGet]
        public IActionResult AgendarExame(int PacienteId)
        {
            List<DataDisponivel> listaDatasDisponiveis = new List<DataDisponivel>();
            listaDatasDisponiveis = data.GetAllDatasDisponiveis().ToList();
            ViewData["PacienteId"] = PacienteId;
            ViewData["listDatasDisponiveis"] = listaDatasDisponiveis as IEnumerable<Models.DataDisponivel>;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgendarExame(int PacienteId, int DataId)
        {
            exam.AddExame(PacienteId, DataId);
            return RedirectToAction("Index", new {pacienteId = PacienteId });
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (Id == null) NotFound();
            Exame exame = exam.GetExame(Id);
            if (exame == null) NotFound();
            List<DataDisponivel> listaDatasDisponiveis = new List<DataDisponivel>();
            listaDatasDisponiveis = data.GetAllDatasDisponiveis().ToList();
            ViewData["listDatasDisponiveis"] = listaDatasDisponiveis as IEnumerable<Models.DataDisponivel>;
            ViewData["pacienteId"] = exame.PacienteId;
            return View(exame);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Exame exame)
        {
            if (id != exame.Id) NotFound();
            if (ModelState.IsValid)
            {
                exam.UpdateExame(exame);
                return RedirectToAction("Index", new {PacienteId = exame.PacienteId});
            }
            return View(exame);
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            if (Id == null) NotFound();
            Exame exame = exam.GetExame(Id);
            if (exame == null) NotFound();
            DataDisponivel dataDisponivel = data.GetDataDisponivel(exame.DataId);
            if (dataDisponivel == null) NotFound();
            ViewData["dataDisponivel"] = dataDisponivel.Data;
            ViewData["pacienteId"] = exame.PacienteId;
            return View(exame);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            if (Id == null) NotFound();
            Exame exame = exam.GetExame(Id);
            if (exame == null) NotFound();
            DataDisponivel dataDisponivel = data.GetDataDisponivel(exame.DataId);
            if (dataDisponivel == null) NotFound();
            ViewData["dataDisponivel"] = dataDisponivel.Data;
            ViewData["pacienteId"] = exame.PacienteId;
            return View(exame);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            Exame exame = exam.GetExame(Id);
            exam.DeleteExame(Id);
            return RedirectToAction("Index", new { pacienteId = exame.PacienteId });
        }
    }
}
