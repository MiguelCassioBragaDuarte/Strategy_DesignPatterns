using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SistemaDePagamentos_Strategy.Context;
using SistemaDePagamentos_Strategy.Strategies;

namespace SistemaDePagamentos_Strategy;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnPagar_Click(object sender, RoutedEventArgs e)
    {
        PagamentoContext context = new PagamentoContext();

        double valor;
        if (!double.TryParse(txtValor.Text, out valor))
        {
            txtResultado.Text = "Digite um valor válido.";
            return;
        }

        var item = (cbPagamento.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();

        switch (item)
        {
            case "Pix":
                context.DefinirStrategy(new Pix());
                break;

            case "Cartão":
                context.DefinirStrategy(new Cartao());
                break;

            case "Boleto":
                context.DefinirStrategy(new Boleto());
                break;

            case "Dinheiro":
                context.DefinirStrategy(new Dinheiro());
                break;

            default:
                txtResultado.Text = "Selecione uma forma de pagamento.";
                return;
        }

        txtResultado.Text = context.ExecutarPagamento(valor);
    }
}