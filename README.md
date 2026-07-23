# CampoSystem ERP AI

![Ci](https://github.com/camposystem/camposystem-erp-ai/actions/workflows/ci.yml/badge.svg)


> ERP moderno desenvolvido em .NET 10, com foco em arquitetura limpa, escalabilidade e boas práticas de Engenharia de Software.

## Objetivos

O CampoSystem ERP AI é um projeto desenvolvido para demonstrar a construção de um ERP moderno utilizando tecnologias atuais do ecossistema .NET.

Além da implementação das funcionalidades de negócio, o projeto tem como foco:

- Arquitetura de Software
- Clean Architecture
- Domain-Driven Design (DDD)
- Integração Contínua (CI)
- DevOps
- Cloud Ready
- Inteligência Artificial
- Boas práticas de Engenharia de Software

## Arquitetura

A solução está sendo construída utilizando:

- Modular Monolith
- Clean Architecture
- Vertical Slice Architecture
- ASP.NET Core Minimal APIs
- Domain-Driven Design (DDD)

## Tecnologias

| Categoria  | Tecnologia                          |
| ---------- | ----------------------------------- |
| Linguagem  | C#                                  |
| Plataforma | .NET 10                             |
| API        | ASP.NET Core Minimal APIs           |
| Banco      | PostgreSQL *(planejado)*            |
| ORM        | Entity Framework Core *(planejado)* |
| Testes     | xUnit                               |
| CI         | GitHub Actions                      |
| Gestão     | Azure DevOps                        |

## Estrutura da Solução
```text
CampoSystem.ErpAI

src
├── Api
├── Application
├── Domain
├── Infrastructure
└── SharedKernel

tests
├── UnitTests
└── IntegrationTests

docs
└── adr
```

 ## Roadmap

- [x] Foundation
- [ ] API Foundation
- [ ] Persistence
- [ ] Customers
- [ ] Authentication
- [ ] Docker
- [ ] Cloud
- [ ] Observability
- [ ] Artificial Intelligence

## Como Executar

```bash
git clone https://github.com/camposystem/camposystem-erp-ai.git

cd CampoSystem.ErpAI

dotnet restore
dotnet build
dotnet test
```

## Fluxo de Desenvolvimento

O projeto utiliza Git Flow.

- main → versões estáveis
- develop → integração
- feature/* → desenvolvimento

O fluxo de desenvolvimento inclui:

- GitHub Actions
- Azure DevOps
- Pull Requests
- Code Review

## ADRs

As decisões arquiteturais são documentadas em ADRs.

- ADR-0001 – Arquitetura Inicial


## Princípios do Projeto

- Entender antes de implementar.
- Adicionar tecnologias apenas quando resolverem um problema real.
- Documentar decisões arquiteturais.
- Evoluir o software de forma incremental.
- Priorizar simplicidade sem abrir mão da qualidade.

## Status do Projeto

✅ Release 0.1.0 (Foundation)

🟡 Sprint 2 - API Foundation (Próxima)

## Autor

**Alexandre de Campos**

Desenvolvedor Backend .NET

- LinkedIn: https://linkedin.com/in/alexandre-camposystem
- GitHub: https://github.com/camposystem

## Licença

Este projeto está licenciado sob a licença MIT.