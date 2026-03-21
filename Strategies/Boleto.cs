namespace SistemaDePagamentos_Strategy.Strategies
{
    public class Boleto: IPagamentoStrategy
    {
        public string Pagar(double valor)
        {
            double taxa = 2.50;
            double total = valor + taxa;

            return $"Boleto selecionado.\n" +
                   $"Valor: R$ {valor:F2}\n" +
                   $"Taxa fixa: R$ {taxa:F2}\n" +
                   $"Total: R$ {total:F2}";
        }
    }
}

