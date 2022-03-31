using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table(name: "AlunoOficinas")]
    public class AlunoOficina
    {
        public int IdAluno { get; set; }
        public int IdOficina { get; set; }
    }
}
