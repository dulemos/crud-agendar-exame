using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    public interface IDataDisponivelDAL
    {
        IEnumerable<DataDisponivel> GetAllDatasDisponiveis();
        void AddDataDisponivel(DataDisponivel dataDisponivel);
        void UpdateDataDisponivel(DataDisponivel dataDisponivel);
        DataDisponivel GetDataDisponivel(int? id);
        void DeleteDataDisponivel(int? id);
    }
}
