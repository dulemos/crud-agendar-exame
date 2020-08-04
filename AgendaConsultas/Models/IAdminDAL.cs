using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    public interface IAdminDAL
    {
        Admin GetAdminByEmail(string email);
    }
}
