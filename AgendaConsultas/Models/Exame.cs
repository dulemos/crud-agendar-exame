using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    [Table("Exame")]
    public class Exame
    {
        [Key]
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int DataId { get; set; }
    }
}
