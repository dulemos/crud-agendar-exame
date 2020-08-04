using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Adicionar disponibilidade da data do exame na tabela DatasDisponiveis

namespace AgendaConsultas.Models
{
    public interface IExameDAL
    {
        IEnumerable<Exame> GetAllExamesByPacienteId(int PacienteId);
        void AddExame(int PacienteId, int DataId);
        void DeleteExame(int ExameId);
        Exame GetExame(int Id);
        void UpdateExame(Exame exame);
    }
}
