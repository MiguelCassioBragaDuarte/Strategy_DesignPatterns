# Strategy Pattern com C# e WPF — Sistema de Pagamentos

No desenvolvimento de software profissional, é comum enfrentar problemas recorrentes relacionados à organização, manutenção e escalabilidade do código. Para solucionar esses desafios, surgiram os Padrões de Projeto (Design Patterns), que são soluções reutilizáveis e testadas para problemas comuns no desenvolvimento de software.

Os Design Patterns foram popularizados pelo livro "Design Patterns: Elements of Reusable Object-Oriented Software", escrito pelo grupo conhecido como Gang of Four (GoF). Esses padrões ajudam desenvolvedores a criar sistemas mais organizados, flexíveis e de fácil manutenção.

## O que são Padrões de Projeto (Design Patterns)?

Padrões de Projeto são modelos de soluções que podem ser aplicados para resolver problemas frequentes na engenharia de software. Eles não são códigos prontos, mas sim estruturas conceituais que orientam a organização do sistema.

Seu uso proporciona:

Reutilização de soluções
Padronização do código
Facilidade de manutenção
Redução de acoplamento

## Justificativa da Escolha do Strategy Pattern

O padrão Strategy foi escolhido para este projeto devido à necessidade de implementar diferentes formas de pagamento dentro de um sistema, permitindo a troca dinâmica de comportamento em tempo de execução.

Em sistemas reais, como plataformas de e-commerce ou aplicações financeiras, é comum a existência de múltiplas formas de pagamento (Pix, Cartão, Boleto). Sem o uso de um padrão adequado, isso pode resultar em estruturas condicionais complexas (if/else), dificultando a manutenção e a escalabilidade do sistema.

O Strategy resolve esse problema ao encapsular cada forma de pagamento em uma classe separada, permitindo que novas estratégias sejam adicionadas sem modificar o código existente, seguindo o princípio Open/Closed da programação orientada a objetos.

## Descrição do Projeto

Este projeto tem como objetivo demonstrar a aplicação do padrão de projeto Strategy, utilizando C# com WPF.
A aplicação simula um sistema de pagamentos, permitindo ao usuário escolher diferentes formas de pagamento (Pix, Cartão, Boleto), onde cada método representa uma estratégia diferente.

### Objetivo

Aplicar na prática o padrão Strategy, demonstrando:

Baixo acoplamento
Facilidade de manutenção
Flexibilidade na troca de comportamentos em tempo de execução

## O que é o Strategy Pattern?

O Strategy Pattern é um padrão de projeto comportamental que permite definir uma família de algoritmos, encapsular cada um deles e torná-los intercambiáveis.

Ou seja, ele permite trocar o comportamento de um sistema sem alterar o código principal.

#### Problema que o padrão resolve

Sem o uso do Strategy, seria comum termos estruturas como:

if (tipoPagamento == "Pix") { ... }
else if (tipoPagamento == "Cartao") { ... }
else if (tipoPagamento == "Boleto") { ... }

#### Isso gera:

Código difícil de manter
Alto acoplamento
Dificuldade para adicionar novas formas de pagamento

#### Solução com Strategy

O padrão Strategy resolve esse problema separando cada comportamento em classes diferentes.

Cada forma de pagamento se torna uma "estratégia".

#### Estrutura do Strategy

O padrão é composto por:

1. Strategy (Interface)

Define o comportamento comum:

IPagamentoStrategy

2. Concrete Strategies

Implementações da interface:

PixStrategy
CartaoStrategy
BoletoStrategy

3. Context

Classe que utiliza a estratégia:

PagamentoContext
⚙️ Funcionamento no Projeto
O usuário escolhe o tipo de pagamento na interface
O sistema define a estratégia correspondente
O pagamento é executado através do contexto
🖥️ Tecnologias Utilizadas
C#
WPF (.NET)
Programação Orientada a Objetos
📂 Estrutura do Projeto
/Strategies
   IPagamentoStrategy.cs
   PixStrategy.cs
   CartaoStrategy.cs
   BoletoStrategy.cs

/Context
   PagamentoContext.cs

/MainWindow.xaml
/MainWindow.xaml.cs

## Comparação: Com e Sem Strategy

### Sem Strategy
- Uso excessivo de if/else
- Código difícil de expandir

### Com Strategy
- Código modular
- Fácil de adicionar novas formas de pagamento
- Baixo acoplamento

## Vantagens e desvantagens

### Vantagens
- Flexibilidade
- Reutilização de código
- Facilidade de manutenção
- Extensibilidade

### Desvantagens
- Aumento no número de classes
- Pode ser excessivo para sistemas simples

🌍 Exemplos Reais de Uso
- Sistemas de pagamento (Pix, crédito, débito)
- Aplicativos de e-commerce
- Sistemas bancários
- APIs de pagamento


## Autor

Miguel Cássio Braga Duarte - Desenvolvedor de sistemas
