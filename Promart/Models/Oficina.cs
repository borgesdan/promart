using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table(name: "Oficinas")]
    [Dapper.Contrib.Extensions.Table("Oficinas")]
    public class Oficina
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        public override string ToString() => Nome ?? "";

        [NotMapped]
        [Dapper.Contrib.Extensions.Write(false)]
        public List<Aluno>? Alunos { get; set; }

        //Reforço Escolar
        //Balé
        //Informática
        //Libras
        //Espanhol
        //Violão
        //Flauta
        //Orientação para a vida
        //Educação física
    }
}
