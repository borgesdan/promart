using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Promart.Codes;

namespace Promart.Models
{
    public class AlunoExibicao
    {
        public string? NomeCompleto { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public string? Sexo { get; set; }
        public string? RG { get; set; }
        public string? CPF { get; set; }
        public string? Certidao { get; set; }
        public string? NomeResponsavel { get; set; }
        public string? VinculoFamiliar { get; set; }
        public string? Contato1 { get; set; }
        public string? Contato2 { get; set; }
        public bool IsBeneficiario { get; set; }
        public string? TipoCasa { get; set; }
        public string? Renda { get; set; }
        public string? AnoEscolar { get; set; }
        public string? TurnoEscolar { get; set; }
        public string? EnderecoRua { get; set; }
        public string? EnderecoBairro { get; set; }
        public string? EnderecoNumero { get; set; }
        public string? EnderecoComplemento { get; set; }
        public string? EnderecoCidade { get; set; }
        public string? EnderecoEstado { get; set; }
        public string? EnderecoCEP { get; set; }
        public string? SituacaoProjeto { get; set; }
        public string? TurnoProjeto { get; set; }
        public string? Observacoes { get; set; }

        AlunoExibicao(Aluno aluno)
        {
            NomeCompleto = aluno.NomeCompleto;

            if (aluno.DataNascimento.HasValue)
            {
                DataNascimento = DateOnly.FromDateTime(aluno.DataNascimento.Value);
            }

            Sexo = ComboBoxTipos.TipoSexoNaoNumerado[aluno.Sexo];
            RG = aluno.RG;
            CPF = aluno.CPF;
            Certidao = aluno.Certidao;
            NomeResponsavel = aluno.NomeResponsavel;
            VinculoFamiliar = ComboBoxTipos.TipoVinculoFamiliarNaoNumerado[aluno.VinculoFamiliar];
            Contato1 = aluno.Contato1;
            Contato2 = aluno.Contato2;
            IsBeneficiario = aluno.IsBeneficiario;
            TipoCasa = ComboBoxTipos.TipoMoradiaNaoNumerado[aluno.TipoMoradia];
            Renda = ComboBoxTipos.TipoRendaNaoNumerado[aluno.Renda];
            AnoEscolar = ComboBoxTipos.TipoAnoEscolarNaoNumerado[aluno.AnoEscolar];
            TurnoEscolar = ComboBoxTipos.TipoTurnoEscolarNaoNumerado[aluno.TurnoEscolar];
            EnderecoRua = aluno.EnderecoRua;
            EnderecoBairro = aluno.EnderecoBairro;
            EnderecoNumero = aluno.EnderecoNumero;
            EnderecoComplemento = aluno.EnderecoComplemento;
            EnderecoCidade = aluno.EnderecoCidade;
            EnderecoEstado = aluno.EnderecoEstado;
            EnderecoCEP = aluno.EnderecoCEP;
            SituacaoProjeto = ComboBoxTipos.TipoAlunoSituacaoNaoNumerado[aluno.SituacaoProjeto];
            Observacoes = aluno.Observacoes;
        }
    }
}
