# ADR-0001 – Arquitetura Inicial 
- **Status:** Aceita
- **Data:** 23/07/2026
- **Autor:** Alexandre de Campos

---

## Contexto

O CampoSystem ERP AI é um ERP moderno desenvolvido em .NET 10 com o objetivo de servir como um projeto de portfólio e uma base sólida para evolução futura.

Os principais objetivos do projeto são:

- Demonstrar boas práticas de arquitetura e desenvolvimento.
- Simular um ambiente corporativo utilizando GitHub, Azure DevOps e documentação técnica.
- Facilitar a evolução para cloud, inteligência artificial e microsserviços sem aumentar desnecessariamente a complexidade inicial.

Diante desses objetivos, foi necessário definir uma arquitetura que equilibrasse simplicidade, organização e capacidade de crescimento.

---

## Escopo 
Esta ADR registra apenas as decisões arquiteturais iniciais da solução.

Tecnologias e decisões futuras serão documentadas em ADRs específicas.

---
## Decisão

A solução será implementada inicialmente como um **Modular Monolith**, seguindo princípios da **Clean Architecture**, organizando as funcionalidades por **Vertical Slice Architecture**,  com **ASP.NET Core Minimal APIs** e **.NET 10**.

A estrutura da solução será:

```text
CampoSystem.ErpAI

src
├── CampoSystem.ErpAI.Api
├── CampoSystem.ErpAI.Application
├── CampoSystem.ErpAI.Domain
├── CampoSystem.ErpAI.Infrastructure
└── CampoSystem.ErpAI.SharedKernel

tests
├── CampoSystem.ErpAI.UnitTests
└── CampoSystem.ErpAI.IntegrationTests

docs
└── adr
```

### Responsabilidade de cada projeto

| Projeto | Responsabilidade |
|----------|------------------|
| Api | Endpoints, configuração da aplicação e composição dos serviços |
| Application | Casos de uso, comandos, consultas e regras de aplicação |
| Domain | Regras de negócio, entidades, agregados e contratos do domínio |
| Infrastructure | Persistência, integrações externas e implementações técnicas |
| SharedKernel | Componentes compartilhados entre os projetos |

---

## Decisões Complementares

Foram adotadas as seguintes decisões:

- Utilizar .NET 10 como plataforma principal.
- Utilizar ASP.NET Core Minimal APIs.
- Utilizar xUnit para testes automatizados.
- Utilizar Entity Framework Core como ORM.
- Organizar o código por funcionalidades (Vertical Slice).
- Manter o Domain independente das demais camadas.
- Utilizar GitHub para hospedagem do código-fonte.
- Utilizar Azure DevOps para gestão do backlog, sprints e planejamento.
- O PostgreSQL foi definido como banco de dados padrão do projeto, embora sua implementação ocorra em uma sprint posterior.
---

## Justificativas

 - **Modular Monolith:** visando reduzir complexidade, facilitar evolução (permite futura migração para microsserviços).
 - **Minimal APIs:** recomendado para APIs modernas, menos código e melhor integração com Vertical Slice.
 - **GitHub:** para versionamento, integração com GitHub Actions além de visibilidade (portfólio).
 - **Azure DevOps:** para gestão do projeto fazendo uso de uma metodologia ágil (gerenciamento do backlog, Sprint, Boards) simulando ambiente corporativo.
 - **GitHub Actions:**  realizando integração contínua, validação automática do build e evitar código quebrado.

## Alternativas Consideradas

### Microsserviços desde o início

**Rejeitada.**

Embora favoreça escalabilidade, aumentaria significativamente a complexidade do projeto, exigindo infraestrutura distribuída, observabilidade, mensageria e orquestração antes mesmo da implementação das funcionalidades de negócio.

### Arquitetura em Três Camadas

**Rejeitada.**

Apesar de simples, tende a concentrar muita responsabilidade em Services e Controllers, dificultando a organização por caso de uso.

### Utilização imediata do MediatR

**Adiada.**

Optou-se por compreender primeiro os conceitos de CQRS e Vertical Slice sem depender de bibliotecas externas. A adoção do MediatR será reavaliada futuramente.

---

## Consequências

### Positivas

- Baixo acoplamento entre as camadas.
- Facilidade para manutenção.
- Estrutura preparada para crescimento.
- Maior facilidade para testes automatizados.
- Evolução gradual para arquitetura distribuída.

### Negativas

- Maior número de projetos na solução.
- Necessidade de disciplina para manter as dependências corretas.
- Algumas funcionalidades de infraestrutura serão implementadas manualmente antes da adoção de ferramentas especializadas.

---

## Critérios Arquiteturais

As seguintes regras deverão ser respeitadas durante o desenvolvimento:

- O projeto **Domain** não poderá depender de nenhum outro projeto.
- O projeto **Application** poderá depender apenas de **Domain** e **SharedKernel**.
- O projeto **Infrastructure** implementará contratos definidos nas camadas superiores.
- O projeto **Api** será responsável apenas pela composição da aplicação e exposição dos endpoints.

---
## Próximas ADRs

- ADR-0002 – Estratégia para Minimal APIs
- ADR-0003 – Persistência com Entity Framework Core
- ADR-0004 – Observabilidade
- ADR-0005 – Autenticação e Autorização
- ADR-0006 – Docker e Containers
- ADR-0007 – Integração com Inteligência Artificial

---
## Referências

- Clean Architecture — Robert C. Martin
- Domain-Driven Design — Eric Evans
- Architecture Decision Records (ADR)
- Microsoft Learn - ASP.NET Core Minimal APIs