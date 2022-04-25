using Promart.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace Promart.Codes
{
    public class RelatorioOficinas
    {
        public class RelatorioOficinaResultado
        {
            public string? Oficina { get; set; }
            public string? Aluno { get; set; }
        }

        IEnumerable<Aluno> alunos;
        IEnumerable<Oficina> oficinas;
        IEnumerable<AlunoOficina> alunoOficinas;

        public List<string> TiposFiltro { get; } = new List<string>()
        {
            //Texto
            "Oficina",
            "Aluno",
            
            //Combobox
        };

        public RelatorioOficinas(IEnumerable<Aluno> alunos, IEnumerable<Oficina> oficinas,
            IEnumerable<AlunoOficina> alunoOficinas)
        {
            this.alunos = alunos;
            this.oficinas = oficinas;
            this.alunoOficinas = alunoOficinas;
            
            foreach(var oficina in oficinas)
            {
                oficina.Alunos = alunos.Where(a => alunoOficinas.Where(ao => ao.IdAluno == a.Id && ao.IdOficina == oficina.Id).Any()).ToList();
            }
        }

        public IEnumerable<Aluno> Filtrar(string nomeFiltro, string valor)
        {
            switch (nomeFiltro)
            {
                case "Oficina":
                    //return alunos.Where(a => a.NomeCompleto != null
                    //    && a.NomeCompleto.Contains(valor));



                    return new List<Aluno>();
                case "Aluno":
                    return alunos.Where(a => a.NomeResponsavel != null
                        && a.NomeResponsavel.Contains(valor));
            }

            return new List<Aluno>();
        }
    }
}
