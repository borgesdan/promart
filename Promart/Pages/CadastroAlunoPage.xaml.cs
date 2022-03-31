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
using Promart.Data;
using Promart.Codes;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for CadastrarAlunoPage.xaml
    /// </summary>
    public partial class CadastroAlunoPage : Page
    {        
        public Aluno Aluno { get; private set; }                

        public CadastroAlunoPage() : this(new Aluno())
        {            
        }

        public CadastroAlunoPage(Aluno aluno)
        {
            InitializeComponent();
            Aluno = aluno;
            ConfirmarButton.Click += (object sender, RoutedEventArgs e) => ConfirmarPagina();
            NascimentoData.SelectedDateChanged += NascimentoData_SelectedDateChanged;
        }

        private void NascimentoData_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            if(sender != null)
            {
                DatePicker date = ((DatePicker)sender);

                if(date.SelectedDate != null && date.SelectedDate.HasValue)
                {
                    DateTime nascimento = date.SelectedDate.Value;
                    IdadeLabel.Content = string.Concat(Helper.Util.ObterIdade(nascimento), " anos");
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Carrega o nome do aluno
            NomeText.Text = Aluno.NomeCompleto;
            NomeText.Focus();            

            //Popula a lista de oficinas
            var oficinas = SqlAccess.GetDados<Oficina>();
            List<CheckBox> checkBoxes = new();
            
            foreach (var o in oficinas)
            {
                CheckBox checkBox = new();                
                checkBox.Content = o;
                checkBoxes.Add(checkBox);
            }

            TipoOficinasList.ItemsSource = checkBoxes;            
        }

        public void ConfirmarPagina()
        {
            Aluno.NomeCompleto = NomeText.Text.Trim();

            Aluno.DataNascimento = NascimentoData.SelectedDate;
            Aluno.RG = RGText.Text;
            Aluno.CPF = CPFText.Text;
            Aluno.Certidao = CertidaoText.Text;

            Aluno.VinculoFamiliar = TipoVinculoCombo.SelectedIndex;
            Aluno.NomeResponsavel = NomeResponsavelText.Text;
            Aluno.IsBeneficiario = BeneficiarioCheck.IsChecked ?? false;
            Aluno.TipoCasa = TipoMoradiaCombo.SelectedIndex;
            Aluno.Renda = TipoRendaCombo.SelectedIndex;
            Aluno.Contato1 = Telefone1Text.Text;
            Aluno.Contato2 = Telefone2Text.Text;

            Aluno.EscolaNome = NomeEscolaText.Text;
            Aluno.TurnoEscolar = TipoTurnoEscolarCombo.Text;
            Aluno.AnoEscolar = TipoAnoEscolarCombo.SelectedIndex;

            Aluno.EnderecoRua = RuaText.Text;
            Aluno.EnderecoBairro = BairroText.Text;
            Aluno.EnderecoNumero = NumeroText.Text;
            Aluno.EnderecoComplemento = ComplementoText.Text;
            Aluno.EnderecoCidade = "Ipiaú";
            Aluno.EnderecoEstado = "Bahia";
            Aluno.EnderecoCEP = 45570000;

            Aluno.SituacaoProjeto = SituacaoProjetoCombro.SelectedIndex;
            Aluno.TurnoProjeto = TipoTurnoEscolarCombo.Text;
            Aluno.Observacoes = ObservacoesText.Text;
            //TODO Aluno Oficinas

            if(Aluno.Id == 0)
            {
                SqlAccess.Inserir(Aluno);
            } 
            else
            {
                SqlAccess.Atualizar(Aluno);
            }
        }
    }
}
