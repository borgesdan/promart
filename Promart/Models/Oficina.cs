using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promart.Models
{
    public class Oficina
    {
        public int IdOficina { get; set; }
        public string? Nome { get; set; }
        public bool IsAtivo { get; set; }
        public string? Descricao { get; set; }        

        //(     ) Reforço Escolar() Balé() Informática() Libras() Espanhol() Violão
//() Flauta()  Orientação para a vida() Educação física
    }
}
