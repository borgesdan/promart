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
    [Dapper.Contrib.Extensions.Table("AlunoVinculos")]
    public class AlunoVinculo
    {
        [Dapper.Contrib.Extensions.ExplicitKey()]
        public int IdAluno { get; set; }
        public string? NomeFamiliar { get; set; }
        public int Idade { get; set; }
        public string? Parentesco { get; set; }
        public string? Ocupacao { get; set; }
        public string? Escolaridade { get; set; }          
        public int Renda { get; set; }

        public static AlunoVinculo DynamicConverter(dynamic obj)
        {
            return new AlunoVinculo()
            {
                IdAluno = obj.IdAluno,
                NomeFamiliar = obj.NomeFamiliar,
                Idade = obj.Idade,
                Parentesco = obj.Parentesco,
                Ocupacao = obj.Ocupacao,
                Escolaridade = obj.Escolaridade,
                Renda = obj.Renda,
            };
        }
    }
}
