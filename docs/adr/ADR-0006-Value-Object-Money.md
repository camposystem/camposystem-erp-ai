# ADR-0006 – Adoção do Value Object Money
- **Status:** Aceita
- **Data:** 06/08/2026
- **Autor:** Alexandre de Campos
- **Atualização:** 18/08/2026
---

## Contexto:

Diversas regras monetárias tendem a ser duplicadas (Round, descontos, multiplicações, arredondamentos).

O domínio precisa representar valores monetários de forma explícita, evitando que regras financeiras sejam implementadas diretamente sobre `decimal` em diferentes pontos do sistema.

O escopo atual do projeto é nacional, com foco em **BRL (Real brasileiro)**. A necessidade de suporte a múltiplas moedas poderá ser tratada futuramente, sem limitar a evolução do Value Object.

## Decisão:

Introduzir um Value Object `Money` para encapsular operações monetárias, mantendo o objeto imutável e responsável pelas regras fundamentais de dinheiro.

Para o escopo atual:

- `Money` representa valores em **BRL**;
- a entrada monetária válida possui **2 casas decimais**;
- `null` representa ausência de valor e não deve ser convertido silenciosamente para zero;
- operações monetárias com `null` são rejeitadas por `MoneyException`;
- `+` e `-` operam exclusivamente entre `Money`;
- `*` e `/` operam entre `Money` e `decimal`;
- divisão por zero é rejeitada por `MoneyException`;
- o acesso ao valor primitivo fora do domínio é realizado pela propriedade `Value`;
- o arredondamento monetário é centralizado no `Money`;
- a política padrão de arredondamento adotada é `MidpointRounding.ToEven` (Banker's Rounding);
- `Round()` é uma operação explícita e não altera a instância original;
- multiplicações e divisões devem respeitar a política monetária definida para BRL, evitando `Math.Round` espalhado pelo domínio.

## Consequências:

- centralização;
- legibilidade;
- imutabilidade;
- igualdade por valor;
- eliminação de `Math.Round` espalhado;
- redução de cálculos monetários diretamente sobre `decimal`;
- regras monetárias mais explícitas e testáveis;
- possibilidade de evolução futura para múltiplas moedas (`Currency`) e regras específicas por moeda.

## Não adotado neste momento:

- suporte a múltiplas moedas;
- regras de câmbio;
- configuração por moeda;
- políticas de arredondamento específicas por contexto de negócio.
