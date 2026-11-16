# CampoSystem ERP AI

![CI](https://github.com/camposystem/camposystem-erp-ai/actions/workflows/ci.yml/badge.svg)

> ERP moderno desenvolvido em .NET 10, com foco em arquitetura limpa, segurança, organização de domínio e boas práticas de Engenharia de Software.

## Sobre o projeto

O **CampoSystem ERP AI** é um projeto de desenvolvimento de um ERP utilizando tecnologias do ecossistema .NET.

O objetivo é demonstrar, na prática, a aplicação de conceitos de arquitetura de software, Domain-Driven Design, persistência de dados, autenticação, autorização e testes automatizados.

O projeto evolui de forma incremental, por meio de Sprints e decisões arquiteturais documentadas.

## Arquitetura

A solução utiliza:

- Modular Monolith
- Clean Architecture
- Vertical Slice Architecture
- Domain-Driven Design (DDD)
- ASP.NET Core Minimal APIs
- Separação de responsabilidades entre Domain, Application, Infrastructure e API

## Tecnologias

| Categoria | Tecnologia |
|---|---|
| Linguagem | C# |
| Plataforma | .NET 10 |
| API | ASP.NET Core Minimal APIs |
| Banco de dados | PostgreSQL |
| ORM | Entity Framework Core |
| Autenticação | Auth0 / JWT |
| Autorização | ASP.NET Core Authorization |
| Testes | xUnit |
| Testes de integração | PostgreSQL / Respawn |
| CI | GitHub Actions |
| Gestão do projeto | Azure DevOps |

## Segurança e controle de acesso

A aplicação utiliza Auth0 como provedor de identidade e JWT Bearer para autenticação das requisições.

A autorização é controlada internamente pela aplicação, utilizando permissões persistidas no PostgreSQL.

O modelo de autorização contempla:

- User
- Tenant
- TenantMembership
- Role
- Permission
- MembershipRole

O relacionamento entre usuários, empresas e permissões permite controlar o acesso de acordo com o Tenant e as permissões atribuídas ao usuário.

### Fluxo de autorização

1. Auth0 autentica o usuário.
2. A API valida o JWT.
3. O sistema identifica o usuário e o Tenant por meio das claims.
4. O PermissionAuthorizationHandler verifica a política de autorização.
5. O AuthorizationUserService consulta as permissões no PostgreSQL.
6. A operação é autorizada ou negada.

### Resultados validados

| Cenário | Resposta |
|---|---|
| Requisição sem token | 401 Unauthorized |
| Usuário autenticado sem permissão | 403 Forbidden |
| Usuário autenticado com `products.create` | 201 Created |
| Consulta de produtos | 200 OK |

Os resultados acima foram validados durante os testes e as chamadas reais pelo Postman.

## Estrutura da solução

```text
CampoSystem.ErpAI
│
├── src
│   ├── Api
│   ├── Application
│   ├── Domain
│   ├── Infrastructure
│   └── SharedKernel
│
├── tests
│   ├── UnitTests
│   └── IntegrationTests
│
└── docs
    └── adr
```

## Testes

O projeto utiliza testes unitários e de integração.

Os testes de integração utilizam PostgreSQL e Respawn para preparar e limpar os dados de teste, mantendo os históricos de migrations.

Para executar os testes:

```bash
dotnet test
```

## Como executar localmente

### Pré-requisitos

- .NET 10 SDK
- PostgreSQL
- Conta e configuração de aplicação no Auth0
- Git

### Clonar o repositório

```bash
git clone https://github.com/camposystem/camposystem-erp-ai.git

cd camposystem-erp-ai
```

### Restaurar dependências e compilar

```bash
dotnet restore

dotnet build
```

### Configurar o ambiente

Configure a conexão PostgreSQL e os parâmetros do Auth0 por meio de configurações locais ou variáveis de ambiente.

Parâmetros necessários:

- `ConnectionStrings:PostgreSQL`
- `Auth0:Authority`
- `Auth0:Audience`

Não publique Client Secrets, tokens ou senhas no repositório.

### Executar os testes

```bash
dotnet test
```

### Executar a API

Execute o projeto da API pelo Visual Studio ou pela CLI do .NET, utilizando a configuração de ambiente local.

## Gestão e fluxo de desenvolvimento

O projeto utiliza Azure DevOps para organização das Sprints, tarefas e acompanhamento da evolução.

O fluxo de desenvolvimento utiliza:

- Git Flow
- GitHub Actions
- Pull Requests
- Code Review
- Testes automatizados
- Documentação de decisões arquiteturais

## ADRs — Architecture Decision Records

As decisões arquiteturais são registradas em documentos ADR.

Decisões documentadas:

- ADR-0001 — Arquitetura Inicial

Novas decisões são adicionadas conforme a evolução da solução.

## Roadmap

O projeto evolui de forma incremental.

Próximas etapas serão definidas conforme as necessidades funcionais e arquiteturais identificadas durante o desenvolvimento.

## Status do projeto

- Foundation: concluída.
- API e persistência: implementadas.
- Identidade e controle de acesso: implementados.
- Sprint 5 — Controle de Acesso: implementação funcional e testes principais validados.
- T07 — Documentação e revisão: em fechamento.

## Princípios do projeto

- Entender antes de implementar.
- Adicionar tecnologias quando resolverem problemas reais.
- Documentar decisões arquiteturais.
- Evoluir o software de forma incremental.
- Priorizar simplicidade sem abrir mão da qualidade.
- Testar o comportamento antes de considerar uma funcionalidade concluída.

## Autor

**Alexandre de Campos**

Desenvolvedor Backend .NET

- LinkedIn: https://linkedin.com/in/alexandre-camposystem
- GitHub: https://github.com/camposystem

## Licença

Este projeto está licenciado sob a licença MIT.
