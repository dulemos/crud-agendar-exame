using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaConsultas.Models
{
    [Table("Table")]
    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }


    }
}
