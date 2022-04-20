using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Windows;

// https://docs.microsoft.com/pt-br/sql/relational-databases/backup-restore/quickstart-backup-restore-database?view=sql-server-ver15

namespace Promart.Data
{
    public static class Backup
    {
        public static async Task<bool> Criar(string caminhoCompleto)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(SqlAccess.GetConnectionString());
                await conn.OpenAsync();

                await conn.ExecuteAsync(@$"                    
                    BACKUP DATABASE [PromartBD]
                    TO DISK = N'{caminhoCompleto}' 
                    WITH NOFORMAT, NOINIT,
                    NAME = N'PromarDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10;
                ");                

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar realizar o backup.\n\n{ex.Message}");
                return false;
            }
        }

        public static async Task<bool> Restaurar(string caminhoCompleto)
        {
            try
            {                
                using SqlConnection conn = new SqlConnection(SqlAccess.GetConnectionString("NoDatabase"));
                await conn.OpenAsync();

                await conn.ExecuteAsync(@$"                    
                    RESTORE DATABASE [PromartBD] 
                    FROM DISK = N'{caminhoCompleto}' WITH  FILE = 1, NOUNLOAD, STATS = 5;
                ");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar realizar a restauração.\n\n{ex.Message}");
                return false;
            }
        }
    }
}
