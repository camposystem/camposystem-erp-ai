# ADR-0005 – Adoção do Result Pattern para tratamento de falhas esperadas
- **Status:** Aceita
- **Data:** 27/07/2026
- **Autor:** Alexandre de Campos
---
## Contexto:
Result Pattern para falhas de negócio esperadas;

## Decisão: 

Utilizar Result Pattern para representar erros de negócio.

Exceptions serão utilizadas apenas para falhas inesperadas ou de infraestrutura.

## Consequências:

Positivas

- API previsível
- Melhor performance
- Fluxo explícito

Negativas

- Mais código

