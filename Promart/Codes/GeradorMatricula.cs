using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Promart.Models;
using Promart.Data;

namespace Promart.Codes
{
    public static class GeradorMatricula
    {
        public static string Get()
        {
            Guid guid = Guid.NewGuid();
            string[] partes = guid.ToString().Split('-');
            string resultado = $"{DateTime.Now.Year}{partes[0]}";
            return resultado.ToUpper();
        }
    }
}
