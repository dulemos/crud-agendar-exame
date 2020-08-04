using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    [Table("DatasDisponiveis")]
    public class DataDisponivel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Data { get; set; }
    }
}
