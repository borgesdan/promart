using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table("AlunoVinculos")]
    public class AlunoVinculo
    {
        public int IdAluno { get; set; }
        public string? NomeFamiliar { get; set; }
        public int Idade { get; set; }
        public string? Parentesco { get; set; }
        public string? Ocupacao { get; set; }
        public string? Escolaridade { get; set; }
        /// <summary>
        /// [0] Menor que 1/2 Salário Mínimo
        /// [1] 1/2 SM
        /// [2] 1 SM
        /// [3] 1 e 1/2 SM
        /// [4] 2 SM
        /// [5] Maior que 2 SM
        /// </summary>      
        public int Renda { get; set; }
    }

    public class AlunoVinculoData
    {
        public string? Nome { get; set;}
        public int Idade { get; set;}
        public List<string> Parentesco { get; set; }
        public string Ocupacao { get; set; }
        public string Escolaridade { get; set; }

        public AlunoVinculoData()
        {
            Parentesco = new List<string>()
            {
                "Mãe",
                "Pai",
                "Irmão",
                "Avô",
                "Avó",
                "Tio",
                "Sobrinho",
                "Primo",
                "Enteado",
                "Neto",
                "Padrasto",
                "Madastra"
            };
        }
    }
}
