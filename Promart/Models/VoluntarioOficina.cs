using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.Models
{
    [Table("VoluntarioOficinas")]
    public class VoluntarioOficina
    {
        public int IdVoluntario { get; set; }
        public int IdOficina { get; set; }
    }
}
