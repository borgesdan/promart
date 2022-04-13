using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table("AlunoVinculos")]
    public class AlunoVinculo
    {
        public int IdAluno { get; set; }
        public string? NomeFamiliar { get; set; }
        public ushort Idade { get; set; }
        public string? Parentesco { get; set; }
        public string? Ocupacao { get; set; }
        public string? Escolaridade { get; set; }          
        public ushort Renda { get; set; }
    }
}
