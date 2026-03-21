using SistemaDePagamentos_Strategy.Strategies;

namespace SistemaDePagamentos_Strategy.Strategies
{
    public class Pix: IPagamentoStrategy
    {
        public string Pagar(double valor)
        {
            return $"Pagamento de R$ {valor} realizado via PIX.";
        }
    }
}

