using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SistemaDePagamentos_Strategy.Context;
using SistemaDePagamentos_Strategy.Data;
using SistemaDePagamentos_Strategy.Models;
using SistemaDePagamentos_Strategy.Strategies;

namespace SistemaDePagamentos_Strategy.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _valor;
        private object _pagamentoSelecionado;
        private ObservableCollection<Pagamento> _pagamentos;

        public string Valor
        {
            get => _valor;
            set
            {
                _valor = value;
                OnPropertyChanged();
            }
        }

        public object PagamentoSelecionado
        {
            get => _pagamentoSelecionado;
            set
            {
                _pagamentoSelecionado = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Pagamento> Pagamentos
        {
            get => _pagamentos;
            set
            {
                _pagamentos = value;
                OnPropertyChanged();
            }
        }

        public ICommand PagarCommand { get; }
        public ICommand NovoCommand { get; }

        public MainViewModel()
        {
            PagarCommand = new RelayCommand(Pagar);
            NovoCommand = new RelayCommand(Novo);
        }

        private void Pagar(object obj)
        {
            if (string.IsNullOrWhiteSpace(Valor))
            {
                MessageBox.Show("Digite um valor.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(Valor, out double valor))
            {
                MessageBox.Show("Digite um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PagamentoSelecionado == null)
            {
                MessageBox.Show("Selecione uma forma de pagamento.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var tipo = (PagamentoSelecionado as ComboBoxItem)?.Content.ToString();

            PagamentoContext context = new PagamentoContext();

            double valorFinal = valor;

            switch (tipo)
            {
                case "Pix":
                    context.DefinirStrategy(new Pix());
                    break;

                case "Cartão":
                    context.DefinirStrategy(new Cartao());
                    valorFinal = valor * 1.05;
                    break;

                case "Boleto":
                    context.DefinirStrategy(new Boleto());
                    valorFinal = valor + 2.5;
                    break;

                case "Dinheiro":
                    context.DefinirStrategy(new Dinheiro());
                    break;
            }

            string resultado = context.ExecutarPagamento(valor);

            var pagamento = new Pagamento
            {
                Tipo = tipo,
                Valor = valor,
                ValorFinal = valorFinal,
                Data = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            };

            var repository = new PagamentoRepository();
            repository.Inserir(pagamento);

            CarregarPagamentos();

            MessageBox.Show(resultado, "Pagamento realizado", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CarregarPagamentos()
        {
            var repository = new PagamentoRepository();
            var lista = repository.Listar();

            Pagamentos = new ObservableCollection<Pagamento>(lista);
        }

        private void Novo(object obj)
        {
            Valor = "";
            PagamentoSelecionado = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}