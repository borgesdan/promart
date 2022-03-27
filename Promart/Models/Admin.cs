using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promart.Models
{
    public class Admin
    {
        public int IdAdmin { get; set; }
        public string? Senha { get; set; }
        public List<DateTime>? Acessos { get; set; }
    }
}
