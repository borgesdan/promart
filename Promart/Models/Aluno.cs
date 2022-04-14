using Promart.Codes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    //TODO: Implementar controle de faltas


    [Table(name: "Alunos")]
    public class Aluno
    {
        public int Id { get; set; }
        
        //------ Dados Pessoais ------//

        public string? NomeCompleto { get; set; }    
        public DateTime? DataNascimento { get; set; }       
        
        /// <summary>
        /// [0] Masculino
        /// [1] Feminino
        /// [3] Não Informado
        /// </summary>
        public int Sexo { get; set; }        
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
        /// [8] Não Informado
        /// </summary>
        public int VinculoFamiliar { get; set; }        
        public string? Contato1 { get; set; }
        public string? Contato2 { get; set; }        
        public bool IsBeneficiario { get; set; }
        /// <summary>
        /// [0] Própria
        /// [1] Alugada
        /// [2] Cedida pela família
        /// [3] Cedida pelo empregador
        /// [4] Cedida de outra forma
        /// [5] Outro tipo
        /// [6] Não Informado
        /// </summary>
        public int TipoMoradia { get; set; }
        /// <summary>
        /// [0] Menor que 1/2 Salário Mínimo
        /// [1] 1/2 SM
        /// [2] 1 SM
        /// [3] 1 e 1/2 SM
        /// [4] 2 SM
        /// [5] Maior que 2 SM
        /// [6] Não Informado
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
        /// [14] Não Informado
        /// </summary>      
        public int AnoEscolar { get; set; }
        /// <summary>
        /// [0] Matutino
        /// [1] Vespertino
        /// [3] Não Informado
        /// </summary>
        public int TurnoEscolar { get; set; }        
        
        //---------- Endereço ----------//

        public string? EnderecoRua { get; set; }
        public string? EnderecoBairro { get; set; }
        public string? EnderecoNumero { get; set; }
        public string? EnderecoComplemento { get; set; }
        public string? EnderecoCidade { get; set; }
        public string? EnderecoEstado { get; set; }
        public string? EnderecoCEP { get; set; }

        //------ Dados Projeto ------//        
        /// <summary>
        /// [0] Inscrito
        /// [1] Aprovado
        /// [2] Em Espera
        /// [3] Matriculado
        /// [4] Não Aprovado
        /// [5] Desistente
        /// [6] Ex-aluno
        /// [7] Não Especificado
        /// </summary>
        public int SituacaoProjeto { get; set; }
        /// <summary>
        /// [0] Matutino
        /// [1] Vespertino
        /// [3] Não Informado
        /// </summary>
        public int TurnoProjeto { get; set; }        
        public string? Matricula { get; set; }        
        public string? Observacoes { get; set; }
        public string? FotoUrl { get; set; }

        //Propriedades para somente consulta

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public DateOnly? DataNascimentoValue { get => DataNascimento.HasValue ? DateOnly.FromDateTime(DataNascimento.Value) : null; }
        
        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)] 
        public int IdadeValue { get => DataNascimento.HasValue ? Helper.Util.ObterIdade(DataNascimento.Value) : 0; }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public string? SexoValue { get => ComboBoxTipos.TipoSexoNaoNumerado[Sexo]; }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public string? VinculoFamiliarValue { get => ComboBoxTipos.TipoVinculoFamiliarNaoNumerado[VinculoFamiliar]; }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public string? TipoMoradiaValue { get => ComboBoxTipos.TipoMoradiaNaoNumerado[TipoMoradia]; }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public string? RendaValue { get => ComboBoxTipos.TipoRendaNaoNumerado[Renda]; }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public string? AnoEscolarValue { get => ComboBoxTipos.TipoAnoEscolarNaoNumerado[AnoEscolar]; }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public string? SituacaoProjetoValue { get => ComboBoxTipos.TipoAlunoSituacaoNaoNumerado[SituacaoProjeto]; }

        public override string ToString()
        {
            return NomeCompleto ?? string.Empty;
        }  
    }    
}
