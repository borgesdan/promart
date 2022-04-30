namespace Promart.Codes
{
    /// <summary>
    /// Representa uma página da navegação de abas do programa.
    /// </summary>
    public interface IMainWindowPage
    {
        /// <summary>
        /// Obtém ou define o título de exibição da página.
        /// </summary>
        public string TitleHeader { get; set; }

        /// <summary>Retorna TRUE caso a aba possa ser fechada.</summary>
        public bool CanClose { get; }

        /// <summary>
        /// Mensagem de aviso caso a aba não deva ser fechada se CanClose seja FALSE, onde 
        /// será exibida uma janela de confirmação com os botões SIM/NÃO para aceite do usuário.
        /// </summary>
        public string CloseWarging { get; }
    }
}