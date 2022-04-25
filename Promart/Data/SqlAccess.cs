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
using System.Windows;

namespace Promart.Data
{
    public class SqlAccess
    {
        static void MostrarErro(string mensagem, Exception ex)
        {
            string texto = string.Concat(mensagem, Environment.NewLine, Environment.NewLine, $"Erro: {ex.Message}");
            MessageBox.Show(texto, "Erro no Banco de Dados", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static string GetConnectionString(string id = "Default")
        {   
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static List<T>? GetDados<T>() where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();
                return conn.GetAll<T>().ToList();
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao receber as informações do banco de dados", ex);
            }

            return null;
        }

        public static T? GetDado<T>(int id) where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();

                return conn.Get<T>(id);
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao receber as informações do banco de dados", ex);
            }

            return null;
        }

        public static long Inserir<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();
                return conn.Insert(dado);
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao inserir as informações no banco de dados", ex);
            }

            return -1;
        }

        public static bool Atualizar<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();

                return conn.Update(dado);
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao atualizar as informações no banco de dados", ex);
            }

            return false;
        }

        public static bool Deletar<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();

                return conn.Delete(dado);
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao deletar as informações no banco de dados", ex);
            }

            return false;
        }

        public static bool DeletarTudo<T>() where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();

                return conn.DeleteAll<T>();
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao deletar as informações no banco de dados", ex);
            }

            return false;
        }

        public static async Task<T?> GetDadoAsync<T>(int id) where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                await conn.OpenAsync();

                return await conn.GetAsync<T>(id);
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao receber as informações do banco de dados", ex);
            }

            return null;
        }

        public static async Task<IEnumerable<T>?> GetDadosAsync<T>() where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                return await conn.GetAllAsync<T>();
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao receber informações do Banco de Dados", ex);
            }

            return null;
        }

        public static async Task<long> InserirAsync<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                return await conn.InsertAsync(dado);
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao inserir as informações no banco de dados", ex);
            }

            return -1;
        }

        public static async Task<bool> AtualizarAsync<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                await conn.OpenAsync();

                return await conn.UpdateAsync(dado);
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao atualizar as informações no banco de dados", ex);
            }

            return false;
        }

        public static async Task<bool> DeletarAsync<T>(T dado) where T : class
        {
            try
            {
                using SqlConnection conn = new SqlConnection(GetConnectionString());
                await conn.OpenAsync();

                return await conn.DeleteAsync(dado);
            }
            catch (Exception ex)
            {
                MostrarErro("Ocorreu um erro ao deletar as informações no banco de dados", ex);
            }

            return false;
        }

        public static class TAlunoOficinas
        {
            public static async Task<IEnumerable<dynamic>?> DeletarAsync(Aluno aluno)
            {
                try
                {
                    using SqlConnection conn = new SqlConnection(GetConnectionString());
                    await conn.OpenAsync();

                    return await conn.QueryAsync(@"DELETE FROM AlunoOficinas 
                            WHERE IdAluno = @Id", aluno);
                }
                catch (Exception ex)
                {
                    MostrarErro("Ocorreu um erro ao deletar as informações no banco de dados", ex);
                }

                return null;
            }

            public static async Task<IEnumerable<dynamic>?> RemoverAlunosDaOficina(Oficina oficina)
            {
                try
                {
                    using SqlConnection conn = new SqlConnection(GetConnectionString());
                    await conn.OpenAsync();

                    return await conn.QueryAsync(@"DELETE FROM AlunoOficinas 
                            WHERE IdOficina = @Id", oficina);
                }
                catch (Exception ex)
                {
                    MostrarErro("Ocorreu um erro ao deletar as informações no banco de dados", ex);
                }

                return null;
            }

            public static async Task<IEnumerable<AlunoOficina>?> GetAsync(Aluno aluno)
            {
                try
                {
                    using SqlConnection conn = new SqlConnection(GetConnectionString());
                    await conn.OpenAsync();

                    var resultado = await conn.QueryAsync(@"SELECT IdAluno, IdOficina FROM AlunoOficinas WHERE IdAluno = @Id", aluno);
                    List<AlunoOficina> oficinas = new List<AlunoOficina>();

                    foreach(var item in resultado)
                    {
                        AlunoOficina alunoOficina = new AlunoOficina();
                        alunoOficina.IdAluno = item.IdAluno;
                        alunoOficina.IdOficina = item.IdOficina;
                        oficinas.Add(alunoOficina);
                    }

                    return oficinas;
                }
                catch (Exception ex)
                {
                    MostrarErro("Ocorreu um erro ao obter as informações no banco de dados", ex);
                }

                return null;
            }
        }

        public static class TAlunoVinculos
        {
            public static async Task<IEnumerable<dynamic>?> DeletarAsync(Aluno aluno)
            {
                try
                {
                    using SqlConnection conn = new SqlConnection(GetConnectionString());
                    await conn.OpenAsync();

                    return await conn.QueryAsync(@"DELETE FROM AlunoVinculos 
                            WHERE IdAluno = @Id", aluno);
                }
                catch (Exception ex)
                {
                    MostrarErro("Ocorreu um erro ao deletar as informações no banco de dados", ex);
                }

                return null;
            }

            public static async Task<IEnumerable<AlunoVinculo>?> GetAsync(Aluno aluno)
            {
                try
                {
                    using SqlConnection conn = new SqlConnection(GetConnectionString());
                    await conn.OpenAsync();

                    var resultado = await conn.QueryAsync(@"SELECT IdAluno, NomeFamiliar, Idade, Parentesco, 
                        Ocupacao, Escolaridade, Renda FROM AlunoVinculos WHERE IdAluno = @Id", aluno);

                    List<AlunoVinculo> vinculos = new List<AlunoVinculo>();
                    foreach (var item in resultado)
                    {
                        AlunoVinculo vinculo = new AlunoVinculo();
                        vinculo.IdAluno = item.IdAluno;
                        vinculo.NomeFamiliar = item.NomeFamiliar;
                        vinculo.Idade = item.Idade;
                        vinculo.Parentesco = item.Parentesco;
                        vinculo.Ocupacao = item.Ocupacao;
                        vinculo.Escolaridade = item.Escolaridade;
                        vinculo.Renda = item.Renda;

                        vinculos.Add(vinculo);
                    }

                    return vinculos;
                }
                catch (Exception ex)
                {
                    MostrarErro("Ocorreu um erro ao obter as informações no banco de dados", ex);
                }

                return null;
            }
        }

        public static class TVoluntarioOficinas
        {
            public static async Task<IEnumerable<dynamic>?> DeletarAsync(Voluntario voluntario)
            {
                try
                {
                    using SqlConnection conn = new SqlConnection(GetConnectionString());
                    await conn.OpenAsync();

                    return await conn.QueryAsync(@"DELETE FROM VoluntarioOficinas 
                            WHERE IdVoluntario = @Id", voluntario);
                }
                catch (Exception ex)
                {
                    MostrarErro("Ocorreu um erro ao deletar as informações no banco de dados", ex);
                }

                return null;
            }

            public static async Task<IEnumerable<dynamic>?> RemoverVoluntariosDaOficina(Oficina oficina)
            {
                try
                {
                    using SqlConnection conn = new SqlConnection(GetConnectionString());
                    await conn.OpenAsync();

                    return await conn.QueryAsync(@"DELETE FROM VoluntarioOficinas 
                            WHERE IdOficina = @Id", oficina);
                }
                catch (Exception ex)
                {
                    MostrarErro("Ocorreu um erro ao deletar as informações no banco de dados", ex);
                }

                return null;
            }

            public static async Task<IEnumerable<VoluntarioOficina>?> GetAsync(Voluntario voluntario)
            {
                try
                {
                    using SqlConnection conn = new SqlConnection(GetConnectionString());
                    await conn.OpenAsync();

                    var resultado = await conn.QueryAsync(@"SELECT IdVoluntario, IdOficina FROM VoluntarioOficinas WHERE IdVoluntario = @Id", voluntario);
                    List<VoluntarioOficina> oficinas = new List<VoluntarioOficina>();

                    foreach (var item in resultado)
                    {
                        VoluntarioOficina voluntarioOficina = new VoluntarioOficina();
                        voluntarioOficina.IdVoluntario = item.IdVoluntario;
                        voluntarioOficina.IdOficina = item.IdOficina;
                        oficinas.Add(voluntarioOficina);

                        return oficinas;
                    }
                }
                catch (Exception ex)
                {
                    MostrarErro("Ocorreu um erro ao obter as informações no banco de dados", ex);
                }

                return null;
            }
        }        

    }
}
