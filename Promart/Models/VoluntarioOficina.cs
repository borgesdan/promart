using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table("VoluntarioOficinas")]
    [Dapper.Contrib.Extensions.Table("VoluntarioOficinas")]
    public class VoluntarioOficina
    {
        [Dapper.Contrib.Extensions.ExplicitKey()]
        public int IdVoluntario { get; set; }
        [Dapper.Contrib.Extensions.ExplicitKey()]
        public int IdOficina { get; set; }
    }
}
