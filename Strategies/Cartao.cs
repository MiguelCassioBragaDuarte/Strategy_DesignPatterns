namespace SistemaDePagamentos_Strategy.Strategies;

public class Cartao:IPagamentoStrategy
{
    public string Pagar(double valor)
    {
        return $"Pagamento de R$ {valor} realizado via Cartão.";
    }
}