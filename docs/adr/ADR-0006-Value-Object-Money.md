# ADR-0006 – Adoção do Value Object Money
- **Status:** Aceita
- **Data:** 06/08/2026
- **Autor:** Alexandre de Campos
---

## Contexto:

Diversas regras monetárias tendem a ser duplicadas (Round, descontos, multiplicações, arredondamentos).

## Decisão:

Introduzir um Value Object Money para encapsular operações monetárias.

## Consequências:

- centralização;
- legibilidade;
- eliminação de Math.Round espalhado;
- possibilidade de evolução futura.

## Alternativas consideradas
- Manter decimal em todo o domínio.
- Criar apenas métodos utilitários estáticos.
- Utilizar uma biblioteca externa de Money.

Motivo da rejeição:

- decimal permite regras inconsistentes.
- Métodos utilitários não encapsulam comportamento.
- Biblioteca externa adiciona dependências desnecessárias para um projeto didático.
- Princípios adotados
- Value Object (DDD)
- Imutabilidade
- Encapsulamento
- Ubiquitous Language
- Single Source of Truth para regras monetárias

## Decisões futuras
- Suporte a múltiplas moedas (Currency).
- Estratégias de arredondamento configuráveis.
- Formatação por cultura (pt-BR, en-US, etc.).
- Distribuição de centavos (Allocate).