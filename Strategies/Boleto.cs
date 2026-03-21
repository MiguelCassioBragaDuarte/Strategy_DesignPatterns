namespace SistemaDePagamentos_Strategy.Strategies
{
    public class Boleto: IPagamentoStrategy
    {
        public string Pagar(double valor)
        {
            return $"Boleto gerado no valor de R$ {valor}.";
        }
    }
}

