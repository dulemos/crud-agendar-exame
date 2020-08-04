using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    public interface IPacienteDAL
    {
        IEnumerable<Paciente> GetAllPacientes();
        void AddPaciente(Paciente paciente);
        void UpdatePaciente(Paciente paciente);
        Paciente GetPaciente(int? id);
        Paciente GetPacienteByEmail(string? email);
        void DeletePaciente(int? id);
    }
}
