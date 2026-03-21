using SistemaDePagamentos_Strategy.Strategies;

namespace SistemaDePagamentos_Strategy.Context
{
    public class PagamentoContext
    {
        private IPagamentoStrategy _strategy;

        public void DefinirStrategy(IPagamentoStrategy strategy)
        {
            _strategy = strategy;
        }

        public string ExecutarPagamento(double valor)
        {
            if (_strategy == null)
                return "Selecione uma forma de pagamento.";

            return _strategy.Pagar(valor);
        }
    }
}

