using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table(name: "Alunos")]
    public class Aluno
    {
        public int Id { get; set; }
        
        //------ Dados Pessoais ------//

        public string? NomeCompleto { get; set; }    
        public DateTime? DataNascimento { get; set; }
        public string? RG { get; set; }
        public string? CPF { get; set; }
        public string? Certidao { get; set; }

        //------ Dados Familiares ------//

        public string? NomeResponsavel { get; set; }        
        /// <summary>
        /// [0] Pais
        /// [1] Avós
        /// [2] Somente a mãe
        /// [3] Somente o pai
        /// [4] Mãe e Padrasto
        /// [5] Pai e Madrasta
        /// [6] Tio
        /// [7] Outro
        /// </summary>
        public int VinculoFamiliar { get; set; }
        public string? Contato1 { get; set; }
        public string? Contato2 { get; set; }        
        public bool IsBeneficiario { get; set; }
        //Própria, Alugada, Cedida, Outra
        /// <summary>
        /// [0] Própria
        /// [1] Alugada
        /// [2] Cedida pela família
        /// [3] Cedida pelo empregador
        /// [4] Cedida de outra forma
        /// [5] Outro tipo
        /// </summary>
        public int TipoCasa { get; set; }
        /// <summary>
        /// [0] Menor que 1/2 Salário Mínimo
        /// [1] 1/2 SM
        /// [2] 1 SM
        /// [3] 1 e 1/2 SM
        /// [4] 2 SM
        /// [5] Maior que 2 SM
        /// </summary>      
        public int Renda { get; set; }

        //------ Dados Escolares ------//

        public string? EscolaNome { get; set; }
        /// <summary>
        /// [0] 1º Ano Ensino Fundamental
        /// [1] 2º Ano Ensino Fundamental
        /// [2] 3º Ano Ensino Fundamental
        /// [3] 4º Ano Ensino Fundamental
        /// [4] 5º Ano Ensino Fundamental
        /// [5] 6º Ano Ensino Fundamental
        /// [6] 7º Ano Ensino Fundamental
        /// [7] 8º Ano Ensino Fundamental
        /// [8] 9º Ano Ensino Fundamental
        /// [9] 1º Ano Ensino Fundamental
        /// [10] 1º Ano Ensino Médio
        /// [11] 2º Ano Ensino Médio
        /// [12] 3º Ano Ensino Médio
        /// [13] 4º Ano Ensino Médio
        /// </summary>      
        public int AnoEscolar { get; set; }
        public string? TurnoEscolar { get; set; }        
        
        //---------- Endereço ----------//

        public string? EnderecoRua { get; set; }
        public string? EnderecoBairro { get; set; }
        public string? EnderecoNumero { get; set; }
        public string? EnderecoComplemento { get; set; }
        public string? EnderecoCidade { get; set; }
        public string? EnderecoEstado { get; set; }
        public int EnderecoCEP { get; set; }

        //------ Dados Projeto ------//        
        /// <summary>
        /// [0] Inscrito
        /// [1] Aprovado
        /// [2] Em Espera
        /// [3] Matriculado
        /// [4] Não Aprovado
        /// [5] Desistente
        /// </summary>
        public int SituacaoProjeto { get; set; }
        public string? TurnoProjeto { get; set; }
        //TODO: Implementar controle de faltas
        //public int NumeroFaltas { get; set; }                       
        public string? Observacoes { get; set; }
        public string? FotoUrl { get; set; }

        public override string ToString()
        {
            return NomeCompleto ?? string.Empty;
        }
    }    
}
