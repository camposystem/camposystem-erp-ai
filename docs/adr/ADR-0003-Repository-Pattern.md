# ADR-0003 – Adoção do Repository Pattern
- **Status:** Aceita
- **Data:** 27/07/2026
- **Autor:** Alexandre de Campos
---

## Contexto

O projeto adotará o Repository Pattern para evitar acoplamento com o Entity Framework Core e permitir a substituição da tecnologia de persistência no futuro.

## Decisão: 

A camada Application dependerá apenas de abstrações (IProductRepository). A implementação utilizando EF Core ficará na Infrastructure.

## Justificativa:

- A Application não conhece EF Core.
- Facilita testes.
- Permite trocar a persistência futuramente.
- Segue Dependency Inversion (SOLID).
