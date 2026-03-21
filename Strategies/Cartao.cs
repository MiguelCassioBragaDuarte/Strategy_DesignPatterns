namespace SistemaDePagamentos_Strategy.Strategies
{
    public class Cartao:IPagamentoStrategy
    {
        public string Pagar(double valor)
        {
            double taxa = valor * 0.05;
            double total = valor + taxa;

            return $"Cartão selecionado.\n" +
                   $"Valor: R$ {valor:F2}\n" +
                   $"Taxa (5%): R$ {taxa:F2}\n" +
                   $"Total: R$ {total:F2}";
        }
    }
}

