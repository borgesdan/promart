using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table("Voluntarios")]
    public class Voluntario
    {
        public int Id { get; set; }

        //------ Dados Pessoais ------//

        public string? NomeCompleto { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Profissao { get; set; }
        public string? RG { get; set; }
        public string? CPF { get; set; }
        public string? Email { get; set; }
        public string? Contato1 { get; set; }
        public string? Contato2 { get; set; }

        //------ Dados Residenciais ------//

        public string? EnderecoRua { get; set; }
        public string? EnderecoBairro { get; set; }
        public string? EnderecoNumero { get; set; }
        public string? EnderecoComplemento { get; set; }
        public string? EnderecoCidade { get; set; }
        public string? EnderecoEstado { get; set; }
        public string? EnderecoCEP { get; set; }

        //---------- Outros ------------//Id
        public string? Observacoes { get; set; }
        public string? FotoUrl { get; set; }
    }
}
