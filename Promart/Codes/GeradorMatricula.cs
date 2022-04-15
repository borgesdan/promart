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
        static Random random = new Random();

        public static string Get(string nome)
        {
            string[] split = nome.ToUpper().Split(' ');
            string anoCorrente = DateTime.Now.Year.ToString();
            string guid = Guid.NewGuid().ToString().Split('-')[1];
            char element1;
            char element2;
            char element3;

            if(split.Length > 2)
            {
                element1 = split[0].First();
                element2 = split[1].First();
                element3 = split[2].First();
            }
            else
            {
                element1 = Convert.ToChar(random.Next(65, 91));
                element2 = Convert.ToChar(random.Next(65, 91));
                element3 = Convert.ToChar(random.Next(65, 91));
            }

            string resultado = string.Concat(anoCorrente, element1, element2, element3, guid).ToUpper();
            return resultado;
        }
     
        public static string Get()
        {
            Guid guid = Guid.NewGuid();
            string[] partes = guid.ToString().Split('-');
            string resultado = $"{DateTime.Now.Year}{partes[0]}";
            return resultado.ToUpper();
        }
    }
}
