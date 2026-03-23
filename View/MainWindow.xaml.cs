using MahApps.Metro.Controls;
using SistemaDePagamentos_Strategy.ViewModels;

namespace SistemaDePagamentos_Strategy.View
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}