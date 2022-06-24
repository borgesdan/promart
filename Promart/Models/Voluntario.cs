using Promart.Codes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table("Voluntarios")]
    [Dapper.Contrib.Extensions.Table("Voluntarios")]
    public class Voluntario
    {
        public int Id { get; set; }

        //------ Dados Pessoais ------//        
        public string? NomeCompleto { get; set; }
        public DateTime? DataNascimento { get; set; }
        /// <summary>
        /// [0] Masculino
        /// [1] Feminino
        /// [2] Não Informado
        /// </summary>
        public int Sexo { get; set; }
        public string? Education { get; set; }
        public string? Profissao { get; set; }
        public string? Escolaridade { get; set; }
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

        //---------- Outros ------------//
        public string? Observacoes { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string? FotoUrl { get; set; }

        //Propriedades para somente consulta

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public DateOnly? DataNascimentoValue
        {
            get => DataNascimento.HasValue ? DateOnly.FromDateTime(DataNascimento.Value) : null;
        }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public int IdadeValue { get => DataNascimento.HasValue ? Helper.Util.ObterIdade(DataNascimento.Value) : 0; }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public string? SexoValue { get => ComboBoxTipos.TipoSexoNaoNumerado[Sexo]; }

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public DateOnly? DataCadastroValue
        {
            get => DataCadastro.HasValue ? DateOnly.FromDateTime(DataCadastro.Value) : null;
        }

        public override string ToString()
        {
            return NomeCompleto ?? string.Empty;
        }

        public static Voluntario DynamicConverter(dynamic obj)
        {
            return new Voluntario()
            {
                Contato1 = obj.Contato1,
                Contato2 = obj.Contato2,
                CPF = obj.CPF,
                DataCadastro = obj.DataCadastro,
                DataNascimento = obj.DataNascimento,
                Email = obj.Email,
                EnderecoBairro = obj.EnderecoBairro,
                EnderecoCEP = obj.EnderecoCEP,
                EnderecoCidade = obj.EnderecoCidade,
                EnderecoComplemento = obj.EnderecoComplemento,
                EnderecoEstado = obj.EnderecoEstado,
                EnderecoNumero = obj.EnderecoNumero,
                EnderecoRua = obj.EnderecoRua,
                Escolaridade = obj.Escolaridade,
                FotoUrl = obj.FotoUrl,
                Id = obj.Id,
                NomeCompleto = obj.NomeCompleto,
                Observacoes = obj.Observacoes,
                Profissao = obj.Profissao,
                RG = obj.RG,
                Sexo = obj.Sexo,                
            };
        }
    }
}
