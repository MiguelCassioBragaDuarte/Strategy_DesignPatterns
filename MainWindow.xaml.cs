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
using MahApps.Metro.Controls;
using SistemaDePagamentos_Strategy.Context;
using SistemaDePagamentos_Strategy.Strategies;

namespace SistemaDePagamentos_Strategy;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnPagar_Click(object sender, RoutedEventArgs e)
    {
        PagamentoContext context = new PagamentoContext();

        double valor;
        if (!double.TryParse(txtValor.Text, out valor) || string.IsNullOrWhiteSpace(txtValor.Text))
        {
            MessageBox.Show("Digite um valor Valido!",
                "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (cbPagamento.SelectedIndex == -1)
        {
            MessageBox.Show("Selecione a forma de pagamento!",
                "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                
        }
        
        string resultado = context.ExecutarPagamento(valor);

        MessageBox.Show(resultado, "Pagamento realizado", MessageBoxButton.OK, MessageBoxImage.Information);

        btnPagar.Visibility = Visibility.Collapsed;
        btnNovo.Visibility = Visibility.Visible;
    }
    private void BtnNovo_Click(object sender, RoutedEventArgs e)
    {
        txtValor.Text = "";
        cbPagamento.SelectedIndex = -1;
        
        btnNovo.Visibility = Visibility.Collapsed;
        btnPagar.Visibility = Visibility.Visible;

        txtValor.Focus();
    }
}