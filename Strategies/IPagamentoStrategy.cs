namespace SistemaDePagamentos_Strategy.Strategies
{
    public interface IPagamentoStrategy
    {
        string Pagar(double valor);
    }
}

