using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Promart.Models
{
    [Table(name: "AlunoOficinas")]
    [Dapper.Contrib.Extensions.Table("AlunoOficinas")]
    public class AlunoOficina
    {  
        [Dapper.Contrib.Extensions.ExplicitKey()]        
        public int IdAluno { get; set; }
        [Dapper.Contrib.Extensions.ExplicitKey()]
        public int IdOficina { get; set; }   

        public static AlunoOficina DynamicConvert(dynamic obj)
        {
            return new AlunoOficina()
            {
                IdAluno = obj.IdAluno,
                IdOficina = obj.IdOficina,
            };
        }
    }
}
