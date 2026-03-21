namespace SistemaDePagamentos_Strategy.Strategies
{
    public class Dinheiro: IPagamentoStrategy
    {
        public string Pagar(double valor)
        {
            return $"Pagamento de R$ {valor} realizado no Dinhero.";
        }
    }
}

