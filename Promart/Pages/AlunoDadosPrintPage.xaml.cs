using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Promart.Models;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for AlunoDadosPrintPage.xaml
    /// </summary>
    public partial class AlunoDadosPrintPage : Page
    {
        CadastroAlunoPage aluno;

        public AlunoDadosPrintPage(CadastroAlunoPage aluno)
        {
            InitializeComponent();
            this.aluno = aluno;

            DadosPessoaisGroup.Header = $"Dados Pessoais | Matrícula: {aluno.MatriculaText.Text} Data {aluno.DataMatriculaText.Text}";

            Nome.Content = aluno.NomeText.Text;
            Nascimento.Content = aluno.NascimentoData.SelectedDate;
            Sexo.Content = aluno.SexoCombo.SelectedValue;
            RG.Content = aluno.RGText.Text;
            CPF.Content = aluno.CPFText.Text;
            Certidao.Content = aluno.CertidaoText.Text;
            Responsável.Content = aluno.NomeResponsavelText.Text;
            Vinculo.Content = aluno.VinculoCombo.SelectedValue;
            Moradia.Content = aluno.MoradiaCombo.SelectedValue;
            Renda.Content = aluno.RendaCombo.SelectedValue;
            Beneficiario.Content = aluno.BeneficiarioCheck.IsChecked == true ? "Sim, recebe Bolsa Família." : "Não recebe Bolsa Família.";
            Telefone1.Content = aluno.Telefone1Text.Text;
            Telefone2.Content = aluno.Telefone2Text.Text;

            Rua.Content = aluno.RuaText.Text;
            Bairro.Content = aluno.BairroText.Text;
            Numero.Content = aluno.NumeroText.Text;
            Complemento.Content = aluno.ComplementoText.Text;
            Escola.Content = aluno.NomeEscolaText.Text;
            Turno.Content = aluno.TurnoEscolarCombo.SelectedValue;
            Ano.Content = aluno.AnoEscolarCombo.SelectedValue;

            Situacao.Content = aluno.SituacaoProjetoCombo.SelectedValue;
            TurnoProjeto.Content = aluno.TurnoProjetoCombo.SelectedValue;

            foreach(CheckBox cb in aluno.OficinasList.Items)
            {
                if (cb.IsChecked == true)
                {
                    TextBox txt = new TextBox();
                    txt.Text = cb.Content.ToString();      
                    txt.MinWidth = 50;
                    txt.Margin = new Thickness(0, 0, 5, 5);
                    
                    Oficinas.Children.Add(txt);
                }
            }

            foreach(AlunoVinculo av in aluno.ComposicaoDataGrid.ItemsSource)
            {
                TextBox txt = new TextBox();
                txt.Text = $"{av.NomeFamiliar}, {av.Parentesco}, {av.Idade}, {av.Escolaridade}, {av.Ocupacao}, {av.Renda} de renda";
                txt.Margin = new Thickness(0,0,0,1);
                txt.BorderThickness = new Thickness(0);
                Composicao.Children.Add(txt);
            }

            Observacoes.Text = aluno.ObservacoesText.Text;
        }
    }
}
