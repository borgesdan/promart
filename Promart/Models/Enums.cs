using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promart.Models
{
    public enum SexoTipo
    {
        Masculino = 0,
        Feminino = 1,
        NaoInformado = 2
    }

    public enum VinculoFamiliarTipo
    {
        Pais = 0,
        Avos,
        SomenteMae,
        SomentePai,
        MaePadrasto,
        PaiMadrasta,
        Tio,
        Outro,
        NaoInformado,
    }

    public enum TurnoEscolar
    {
        Matutino = 0,
        Vespertino = 1,
        NaoInformado = 2
    }

    public enum MoradiaTipo
    {
        Propria = 0,
        Alugada,
        CedidaFamilia,
        CedidaEmpregador,
        CedidaOutro,
        Outro,
        NaoInformado,
    }

    public enum RendaTipo
    {
        MenorMeio = 0,
        MeioMinimo,
        Minimo,
        MinimoMeio,
        DoisMinimos,
        MaiorDoisMinimos,
        NaoInformado
    }

    public enum AnoEscolarTipo
    {
         Funtamental1 = 0,
         Funtamental2,
         Funtamental3,
         Funtamental4,
         Funtamental5,
         Funtamental6,
         Funtamental7,
         Funtamental8,
         Funtamental9,
         Medio1,
         Medio2,
         Medio3,
         Medio4,
         NaoInformado
    }

    public enum SituacaoAlunoTipo
    {
        Inscrito = 0,
        Aprovado,
        EmEspera,
        Matriculado,
        NaoAprovado,
        Desistente,
        Exaluno,
        NaoEspecificado
    }
}