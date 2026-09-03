# ADR-0006 – Adoção de Domínio Rico
- **Status:** Aceita
- **Data:** 27/07/2026
- **Autor:** Alexandre de Campos
---

## Contexto:

O projeto adotará uma abordagem de Domínio Rico, respeitando o nível de complexidade de cada regra de negócio. O objetivo é manter as invariantes e os comportamentos diretamente nas entidades e Value Objects, evitando que a lógica de negócio fique dispersa em Handlers ou outras camadas da aplicação.

## Decisão: 

Adotar uma abordagem de Domínio Rico de forma pragmática, encapsulando regras de negócio nas entidades e Value Objects, sem adicionar complexidade desnecessária.

## Justificativa:

- Entidades encapsulam comportamento.
- Value Objects representam conceitos do domínio.
- Invariantes permanecem protegidas no domínio.
- Commands validam apenas a entrada da aplicação.
- Handlers orquestram os casos de uso, sem implementar regras de negócio.

## Consequências

### Positivas

- Maior encapsulamento das regras de negócio.
- Redução de duplicidade de validações.
- Código mais coeso e de fácil manutenção.
- Facilidade para evolução do domínio.

### Negativas

- Maior número de classes (Entities e Value Objects).
- Curva de aprendizado maior para novos desenvolvedores.