using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promart.Models
{
    public class Aluno
    {
        public int IdAluno { get; set; }
        
        //------ Dados Pessoais ------//

        public string? NomeCompleto { get; set; }       
        public DateTime DataNascimento { get; set; }
        public string? RG { get; set; }
        public string? CPF { get; set; }
        public string? Certidao { get; set; }

        //------ Dados Familiares ------//

        public string? NomeResponsavel { get; set; }
        //Pais, avós, Somente a mãe, Somente o Pai, Tios, Mãe e Padrasto, Pai e Madrasta, Outro
        public string? VinculoFamiliar { get; set; }
        public string? ContatoFamilia1 { get; set; }
        public string? ContatoFamilia2 { get; set; }        
        public bool IsBeneficiario { get; set; }
        public string? TipoCasa { get; set; }
        //TODO: Implementar Renda
        //public int Renda { get; set; }

        //------ Dados Escolares ------//

        public Escola? EscolaNome { get; set; }
        public string? AnoEscolar { get; set; }
        public string? TurnoEscolar { get; set; }        
        
        //---------- Endereço ----------//

        public string? EnderecoRua { get; set; }
        public string? EnderecoBairro { get; set; }
        public string? EnderecoNumero { get; set; }
        public string? EnderecoComplemento { get; set; }
        public string? EnderecoCidade { get; set; }
        public string? EnderecoEstado { get; set; }
        public string? EnderecoCEP { get; set; }

        //------ Dados Projeto ------//
        //TODO: Implementar Situação
        //Situação: Aprovado, Em Espera, Matriculado, Não Aprovado, Desistente
        public string? TurnoProjeto { get; set; }
        public List<Oficina>? Oficinas { get; set; }
        //TODO: Implementar controle de faltas
        //public int NumeroFaltas { get; set; }                       
        public string? Observacoes { get; set; }
        public string? FotoUrl { get; set; }                
    }    
}
