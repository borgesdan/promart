using Dapper;
using Dapper.Contrib.Extensions;
using Promart.Codes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Promart.Data
{
    public class SqlAccess
    {
        //Grava as mensagens de erro do banco de dados no arquivo de log.
        static void SetErrorLog(string mensagem, Exception ex)
        {
            DateTime now = DateTime.Now;
            LogFile.WriteLine($"[{now}][ERRO] Banco de dados: {mensagem}, [EX] {ex.Message}");
        }

        //Obtém o nome da tabela do banco de dados através da classe modelo.
        static string GetTableName<TTable>()
        {
            var attrib = typeof(TTable).GetCustomAttributes(typeof(TableAttribute), false);            

            if (attrib.Length > 0)
                return ((TableAttribute)attrib[0]).Name;
            else
                return nameof(TTable);
        }

        /// <summary>
        /// Obtém a string de conexão através de seu id.
        /// </summary>
        public static string GetConnectionString(string id = "Default")
        {   
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }        

        /// <summary>
        /// Retorna os dados selecionados ao informar o ID para consulta.
        /// </summary>
        public static async Task<T?> GetAsync<T>(int id) where T : class
        {
            try
            {
                using SqlConnection conn = new(GetConnectionString());
                await conn.OpenAsync();
                return await conn.GetAsync<T>(id);
            }
            catch (Exception ex)
            {
                SetErrorLog("Ocorreu um erro ao receber as informações do banco de dados", ex);
            }

            return null;
        }

        /// <summary>
        /// Retorna todos os dados de uma tabela.
        /// </summary>
        public static async Task<IEnumerable<T>?> GetAllAsync<T>() where T : class
        {
            try
            {
                using SqlConnection conn = new(GetConnectionString());
                await conn.OpenAsync();
                return await conn.GetAllAsync<T>();
            }
            catch (Exception ex)
            {
                SetErrorLog("Ocorreu um erro ao receber as informações do Banco de Dados", ex);
            }

            return null;
        }

        /// <summary>
        /// Retorna os dados selecionados através de um SELECT ao informar a tabela (TTable); o Id para consulta (entityPropertyIdName) 
        /// que deve ser uma propriedade da entidade (entity); e o nome da coluna desejada da tabela (tableColName).
        /// </summary>
        public static async Task<IEnumerable<dynamic>?> GetAllAsync<TTable, TEntity>(TEntity entity, string entityPropertyIdName, string tableColName) where TTable : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                await conn.OpenAsync();

                string tableName = GetTableName<TTable>();

                return await conn.QueryAsync(@$"SELECT * FROM {tableName} 
                            WHERE {tableColName} = @{entityPropertyIdName}", entity);
            }
            catch (Exception ex)
            {
                SetErrorLog("Ocorreu um erro ao deletar as informações no banco de dados", ex);
            }

            return null;
        }

        /// <summary>
        /// Insere os dados de uma tabela.
        /// </summary>
        public static async Task<long> InsertAsync<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new(GetConnectionString());
                await conn.OpenAsync();
                return await conn.InsertAsync(dado);
            }
            catch (Exception ex)
            {
                SetErrorLog("Ocorreu um erro ao inserir as informações no banco de dados", ex);
            }

            return -1;
        }

        /// <summary>
        /// Atualiza os dados de uma tabela.
        /// </summary>
        public static async Task<bool> UpdateAsync<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new(GetConnectionString());
                await conn.OpenAsync();

                return await conn.UpdateAsync(dado);
            }
            catch (Exception ex)
            {
                SetErrorLog("Ocorreu um erro ao atualizar as informações no banco de dados", ex);
            }

            return false;
        }

        /// <summary>
        /// Deleta os dados selecionados ao informar o ID para consulta.
        /// </summary>
        public static async Task<bool> DeleteAsync<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new(GetConnectionString());
                await conn.OpenAsync();
                
                return await conn.DeleteAsync(dado);
            }
            catch (Exception ex)
            {
                SetErrorLog("Ocorreu um erro ao deletar as informações no banco de dados", ex);
            }

            return false;
        }

        /// <summary>
        /// Retorna todos os dados de uma tabela.
        /// </summary>
        public static async Task<bool> DeleteAllAsync<T>() where T : class
        {
            try
            {
                using SqlConnection conn = new(GetConnectionString());
                await conn.OpenAsync();
                
                return await conn.DeleteAllAsync<T>();
            }
            catch (Exception ex)
            {
                SetErrorLog("Ocorreu um erro ao deletar as informações no banco de dados", ex);
            }

            return false;
        }

        /// <summary>
        /// Deleta os dados selecionados através de um DELETE ao informar a tabela (TTable); o Id para consulta (entityPropertyIdName) 
        /// que deve ser uma propriedade da entidade (entity); e o nome da coluna desejada da tabela (tableColName).
        /// </summary>
        public static async Task<IEnumerable<dynamic>?> DeleteAllAsync<TTable, TEntity>(TEntity entity, string entityPropertyIdName, string tableColName) where TTable : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                await conn.OpenAsync();

                string tableName = GetTableName<TTable>();

                return await conn.QueryAsync(@$"DELETE FROM {tableName} 
                            WHERE {tableColName} = @{entityPropertyIdName}", entity);
            }
            catch (Exception ex)
            {
                SetErrorLog("Ocorreu um erro ao deletar as informações no banco de dados", ex);
            }

            return null;
        }               

    }
}
