# Strategy Pattern com C# e WPF — Sistema de Pagamentos 💳

No desenvolvimento de software moderno, é fundamental construir sistemas que sejam organizados, reutilizáveis e de fácil manutenção. Nesse contexto, os Padrões de Projeto (Design Patterns) desempenham um papel essencial, fornecendo soluções consolidadas para problemas recorrentes na engenharia de software.

Os padrões foram formalizados no livro “Design Patterns: Elements of Reusable Object-Oriented Software”, do grupo conhecido como Gang of Four (GoF), e são amplamente utilizados em sistemas profissionais.

## O que são Padrões de Projeto (Design Patterns)?

Design Patterns são modelos de solução que auxiliam desenvolvedores a resolver problemas comuns de forma estruturada e eficiente.

Eles não representam código pronto, mas sim boas práticas reutilizáveis, promovendo:

- Redução de acoplamento
- Maior organização do código
- Facilidade de manutenção
- Escalabilidade do sistema

## Justificativa da Escolha do Strategy Pattern

O padrão Strategy foi escolhido devido à necessidade de implementar diferentes formas de pagamento em um sistema, cada uma com regras específicas (como taxas e cálculos distintos).

Sem o uso de um padrão adequado, o sistema dependeria de múltiplas estruturas condicionais (if/else), tornando o código:

- Difícil de manter
- Pouco escalável
- Altamente acoplado

Com o Strategy, cada comportamento é encapsulado em uma classe independente, permitindo a troca dinâmica de estratégias em tempo de execução, sem alterar o código principal.

Além disso, o padrão segue o princípio Open/Closed, permitindo que novas funcionalidades sejam adicionadas sem modificar o código existente.

## Descrição do Projeto

O projeto consiste em um sistema de pagamentos desenvolvido em C# com WPF, onde o usuário pode escolher diferentes formas de pagamento.

Cada método de pagamento representa uma estratégia distinta.

- Métodos disponíveis:
- Pix (sem taxa)
- Dinheiro (sem taxa)
- Cartão (taxa de 5%)
- Boleto (taxa fixa).

## O que é o Strategy Pattern?

O Strategy Pattern é um padrão de projeto do tipo comportamental que permite definir uma família de algoritmos, encapsular cada um deles em classes separadas e torná-los intercambiáveis em tempo de execução.

Esse padrão possibilita que o comportamento de um objeto seja alterado dinamicamente, sem a necessidade de modificar sua estrutura interna ou o código cliente que o utiliza.

Na prática, ele promove a separação entre o que é feito (comportamento) e como é feito (implementação), delegando a responsabilidade das diferentes variações de um algoritmo para classes específicas.



Dessa forma, o Strategy reduz o uso de estruturas condicionais complexas (como múltiplos if/else), favorecendo um design mais modular, extensível e aderente ao princípio Open/Closed, onde novas funcionalidades podem ser adicionadas sem alterar o código existente.

#### Problema que o padrão resolve

Sem o uso do Strategy, seria comum termos estruturas como:

```

private void BtnPagar_Click(object sender, RoutedEventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtValor.Text))
    {
        MessageBox.Show("Digite um valor.");
        return;
    }

    if (!double.TryParse(txtValor.Text, out double valor))
    {
        MessageBox.Show("Valor inválido.");
        return;
    }

    if (cbPagamento.SelectedIndex == -1)
    {
        MessageBox.Show("Selecione uma forma de pagamento.");
        return;
    }

    var tipo = (cbPagamento.SelectedItem as ComboBoxItem)?.Content.ToString();

    string resultado = "";

    if (tipo == "Pix")
    {
        resultado = $"PIX selecionado.\nValor final: R$ {valor:F2} (sem taxa)";
    }
    else if (tipo == "Dinheiro")
    {
        resultado = $"Dinheiro selecionado.\nValor final: R$ {valor:F2} (sem taxa)";
    }
    else if (tipo == "Cartão")
    {
        double taxa = valor * 0.05;
        double total = valor + taxa;

        resultado = $"Cartão selecionado.\n" +
                    $"Valor: R$ {valor:F2}\n" +
                    $"Taxa (5%): R$ {taxa:F2}\n" +
                    $"Total: R$ {total:F2}";
    }
    else if (tipo == "Boleto")
    {
        double taxa = 2.50;
        double total = valor + taxa;

        resultado = $"Boleto selecionado.\n" +
                    $"Valor: R$ {valor:F2}\n" +
                    $"Taxa fixa: R$ {taxa:F2}\n" +
                    $"Total: R$ {total:F2}";
    }

    MessageBox.Show(resultado, "Pagamento realizado");
}

```

### Isso gera:

- Código difícil de manter
- Alto acoplamento
- Dificuldade para adicionar novas formas de pagamento

## Solução com Strategy

O padrão Strategy resolve esse problema separando cada comportamento em classes diferentes.

Cada forma de pagamento se torna uma "estratégia".

### Estrutura do Strategy

O padrão é composto por:

```
/Strategies
IPagamentoStrategy.cs

/Context
PagamentoContext.cs

/MainWindow.xaml
/MainWindow.xaml.cs

```

1. Strategy (Interface)

- Define o contrato comum para todas as estratégias.

2. Concrete Strategies
   
- Implementações específicas.
- Cada classe encapsula sua própria lógica e regras de negócio.

3. Context

- Classe responsável por utilizar a estratégia.
- Ela recebe uma estratégia e executa o pagamento sem conhecer os detalhes da implementação.

## Uso no projeto

### Estrutura do projeto
```
/Strategies
IPagamentoStrategy.cs
Pix.cs
Cartao.cs
Boleto.cs
Dinheiro.cs

/Context
PagamentoContext.cs

/MainWindow.xaml
/MainWindow.xaml.cs
```
### IPagamentoStrategy.cs - Define o contrato comum para todas as estratégias.
```
namespace SistemaDePagamentos_Strategy.Strategies
{
    public interface IPagamentoStrategy
    {
        string Pagar(double valor);
    }
}

```

### Cartao.cs - Implementações específicas.
```
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


```

### PagamentoContext.cs - Classe responsável por utilizar a estratégia.
```
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



```
## Com e Sem Strategy

### Sem Strategy
- Uso excessivo de if/else
- Código difícil de manter
- Alto acoplamento
- Baixa escalabilidade

### Com Strategy
- Código modular
- Baixo acoplamento
- Fácil manutenção
- Alta extensibilidade

## Vantagens e desvantagens

### Vantagens
- Flexibilidade na troca de comportamento
- Separação de responsabilidades
- Facilidade de expansão
- Reutilização de código

### Desvantagens
- Aumento no número de classes
- Pode ser desnecessário para sistemas muito simples

## Análise Crítica

O padrão Strategy mostrou-se extremamente eficiente para resolver problemas de acoplamento e facilitar a manutenção do sistema.

Entretanto, sua utilização pode aumentar a complexidade estrutural devido à criação de múltiplas classes. Em sistemas pequenos, essa abordagem pode ser considerada excessiva.

No contexto deste projeto, o uso do padrão foi justificado pela necessidade de lidar com múltiplas regras de negócio de forma organizada e escalável.

## Exemplos Reais de Uso
- Sistemas de pagamento (Pix, crédito, débito)
- Aplicativos de e-commerce
- Sistemas bancários
- APIs de pagamento

## Referências

Erich Gamma; Richard Helm; Ralph Johnson; John Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Boston: Addison-Wesley, 1994.
Eric Freeman; Elisabeth Robson. Head First Design Patterns. Sebastopol: O’Reilly Media, 2004.
Refactoring Guru. Strategy Pattern. Disponível em: https://refactoring.guru/design-patterns/strategy
GeeksforGeeks. Strategy Design Pattern in C#. Disponível em: https://www.geeksforgeeks.org/

## Autor

Miguel Cássio Braga Duarte - Desenvolvedor de sistemas
