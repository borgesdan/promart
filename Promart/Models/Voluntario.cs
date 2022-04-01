using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promart.Models
{
    public class Voluntario
    {
        public int IdVoluntario { get; set; }

        //------ Dados Pessoais ------//

        public string? NomeCompleto { get; set; }
        public string? RG { get; set; }
        public string? CPF { get; set; }
        public string? Email { get; set; }
        public string? Telefone1 { get; set; }
        public string? Telefone2 { get; set; }
        public DateTime? DataNascimento { get; set; }

        //------ Dados Residenciais ------//

        public string? EnderecoRua { get; set; }
        public string? EnderecoBairro { get; set; }
        public string? EnderecoNumero { get; set; }
        public string? EnderecoComplemento { get; set; }
        public string? EnderecoCidade { get; set; }
        public string? EnderecoEstado { get; set; }
        public string? EnderecoCEP { get; set; }

        //---------- Outros ------------//
        public List<Oficina>? Oficinas { get; set; }
        public string? Observacoes { get; set; }
        public string? FotoUrl { get; set; }
    }
}
