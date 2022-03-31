using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Promart.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Promart.Data
{
    public class SqlAccess
    {
        static string GetConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static List<T> GetDados<T>() where T : class
        {
            using SqlConnection conn = new SqlConnection(GetConnectionString());
            conn.Open();

            return conn.GetAll<T>().ToList();
        }

        public static T GetDado<T>(int id) where T : class
        {
            using SqlConnection conn = new SqlConnection(GetConnectionString());
            conn.Open();

            return conn.Get<T>(id);
        }

        /// <summary>
        /// Insere e retorna o ID ou a quantidade de linhas afetadas.
        /// </summary>
        public static long Inserir<T>(T dado) where T : class
        {
            using SqlConnection conn = new SqlConnection(GetConnectionString());
            conn.Open();

            return conn.Insert(dado);
        }

        /// <summary>
        /// Atualiza e retorna se houve sucesso.
        /// </summary>
        public static bool Atualizar<T>(T dado) where T : class
        {
            using SqlConnection conn = new SqlConnection(GetConnectionString());
            conn.Open();

            return conn.Update(dado);
        }
    }
}
