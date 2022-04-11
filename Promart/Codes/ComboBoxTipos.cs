using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promart.Codes
{
    public static class ComboBoxTipos
    {
        public static List<string> TipoSexoNumerado { get; } = new List<string>()
            {
                "0 - Masculino",
                "1 - Feminino",
            };

        public static List<string> TipoSexoNaoNumerado { get; } = new List<string>()
            {
                "Masculino",
                "Feminino",
            };

        public static List<string> TipoBeneficiarioNumerado { get; } = new List<string>()
            {
                "0 - Beneficiário",
                "1 - Não Beneficiário",
            };

        public static List<string> TipoBeneficiarioNaoNumerado { get; } = new List<string>()
            {
                "Beneficiário",
                "Não Beneficiário",
            };

        public static List<string> TipoTurnoEscolarNumerado { get; } = new List<string>()
            {
                "0 - Matutino",
                "1 - Vespertino",
            };

        public static List<string> TipoTurnoEscolarNaoNumerado { get; } = new List<string>()
            {
                "Matutino",
                "Vespertino",
            };

        public static List<string> TipoVinculoFamiliarNumerado { get; } = new List<string>()
            {
                "0 - Pais",
                "1 - Avós",
                "2 - Somente a mãe",
                "3 - Somente o pai",
                "4 - Mãe e Padrasto",
                "5 - Pai e Madrasta",
                "6 - Tio",
                "7 - Outro",
            };

        public static List<string> TipoVinculoFamiliarNaoNumerado { get; } = new List<string>()
            {
                "Pais",
                "Avós",
                "Somente a mãe",
                "Somente o pai",
                "Mãe e Padrasto",
                "Pai e Madrasta",
                "Tio",
                "Outro",
            };

        public static List<string> TipoMoradiaNumerado { get; } = new List<string>()
            {
                "0 - Própria",
                "1 - Alugada",
                "2 - Cedida pela família",
                "3 - Cedida pelo empregador",
                "4 - Cedida de outra forma",
                "5 - Outro tipo",
            };


        public static List<string> TipoMoradiaNaoNumerado { get; } = new List<string>()
            {
                "Própria",
                "Alugada",
                "Cedida pela família",
                "Cedida pelo empregador",
                "Cedida de outra forma",
                "Outro tipo",
            };

        public static List<string> TipoRendaNumerado { get; } = new List<string>()
            {
                "0 - Menor que 1/2 Salário Mínimo",
                "1 - Meio salário",
                "2 - 1 Salário Mínimo",
                "3 - 1 salário e meio",
                "4 - 2 Salários",
                "5 - Maior que 2 Salários Minímos",
            };

        public static List<string> TipoRendaNaoNumerado { get; } = new List<string>()
            {
                "Menor que 1/2 Salário Mínimo",
                "Meio salário",
                "Salário Mínimo",
                "1 salário e meio",
                "2 Salários",
                "Maior que 2 Salários Minímos",
            };

        public static List<string> TipoAnoEscolarNumerado { get; } = new List<string>()
            {
                "0 - 1º Ano Ensino Funtamental",
                "1 - 2º Ano Ensino Funtamental",
                "2 - 3º Ano Ensino Funtamental",
                "3 - 4º Ano Ensino Funtamental",
                "4 - 5º Ano Ensino Funtamental",
                "5 - 6º Ano Ensino Funtamental",
                "6 - 7º Ano Ensino Funtamental",
                "7 - 8º Ano Ensino Funtamental",
                "8 - 9º Ano Ensino Funtamental",
                "9 - 1º Ano Ensino Funtamental",
                "10 - 1º Ano Ensino Médio",
                "11 - 2º Ano Ensino Médio",
                "12 - 3º Ano Ensino Médio",
                "13 - 4º Ano Ensino Médio",
            };

        public static List<string> TipoAnoEscolarNaoNumerado { get; } = new List<string>()
            {
                "1º Ano Ensino Funtamental",
                "2º Ano Ensino Funtamental",
                "3º Ano Ensino Funtamental",
                "4º Ano Ensino Funtamental",
                "5º Ano Ensino Funtamental",
                "6º Ano Ensino Funtamental",
                "7º Ano Ensino Funtamental",
                "8º Ano Ensino Funtamental",
                "9º Ano Ensino Funtamental",
                "1º Ano Ensino Funtamental",
                "1º Ano Ensino Médio",
                "2º Ano Ensino Médio",
                "3º Ano Ensino Médio",
                "4º Ano Ensino Médio",
            };


        public static List<string> TipoAlunoSituacaoNumerado { get; } = new List<string>()
            {
                "0 - Inscrito",
                "1 - Aprovado",
                "2 - Em Espera",
                "3 - Matriculado",
                "4 - Não Aprovado",
                "5 - Desistente",
                "6 - Não Especificado"
            };

        public static List<string> TipoAlunoSituacaoNaoNumerado { get; } = new List<string>()
            {
                "Inscrito",
                "Aprovado",
                "Em Espera",
                "Matriculado",
                "Não Aprovado",
                "Desistente",
                "Não Especificado"
            };
    }
}
