
# ADR-0002 – Adoção do CQRS
- **Status:** Aceita
- **Data:** 27/07/2026
- **Autor:** Alexandre de Campos
---

## Contexto

O projeto seguirá CQRS desde o início para separar operações de escrita e leitura, favorecendo organização por caso de uso.

## Decisão: 

Utilizar CQRS sem MediatR.

## Justificativa:

- Projeto sem dependência externa.
- Clareza do padrão sem biblioteca.
- Aderência à arquitetura própria.

## Consequências

Positivas

- Maior organização do código.
- Baixo acoplamento.
- Facilita a evolução da solução.

Negativas

- Mais classes.
- Curva de aprendizado maior.