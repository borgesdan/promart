using System.IO;

namespace Promart.Codes
{
    /// <summary>
    /// Gerencia as entradas do arquivo de log.
    /// </summary>
    public static class LogFile
    {
        static readonly string logPath = $"{Helper.Diretorios.ATUAL}\\log.txt";

        /// <summary>
        /// Cria um arquivo de log caso não exista.
        /// </summary>
        public static void Create()
        {
            if (!File.Exists(logPath))
            {
                File.Create(logPath);
            }
        }

        /// <summary>
        /// Escreve no arquivo de log.
        /// </summary>
        public static void WriteLine(string text)
        {
            Create();

            using StreamWriter stream = File.AppendText(logPath);
            stream.WriteLine(text);
        }
    }
}
