# CampoSystem ERP AI

![CI](https://github.com/camposystem/camposystem-erp-ai/actions/workflows/ci.yml/badge.svg)

### Engenharia de Software, Arquitetura e Soluções SaaS

O **CampoSystem ERP AI** é um projeto de engenharia de software que demonstra, na prática, a construção e evolução de soluções empresariais utilizando tecnologias modernas do ecossistema .NET.

O projeto reúne experiências práticas em arquitetura de software, Domain-Driven Design, segurança, persistência de dados, autenticação, autorização e testes automatizados.

Sua evolução é conduzida de forma incremental, por meio de Sprints, requisitos funcionais e decisões arquiteturais documentadas.

## Propósito

Demonstrar a aplicação de boas práticas de Engenharia de Software na construção de soluções empresariais, com foco em organização de domínio, qualidade técnica, segurança e evolução sustentável.

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

## Experiência prática em engenharia

O desenvolvimento contempla:

- Modelagem de domínio e Value Objects.
- Construção de APIs REST.
- Persistência de dados com Entity Framework Core.
- Autenticação e autorização.
- Multi-tenancy.
- Controle de acesso baseado em permissões.
- Testes unitários e de integração.
- Documentação de decisões arquiteturais.
- Evolução incremental por Sprints.

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

Os resultados foram validados durante os testes e as chamadas reais pelo Postman.

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

Para executar:

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

Decisão documentada:

- ADR-0001 — Arquitetura Inicial

Novas decisões são adicionadas conforme a evolução da solução.

## Ecossistema Camposystem

### Camposystem Stock

O **Camposystem Stock** é uma iniciativa de produto SaaS da Camposystem, voltada à gestão de estoque, armazenagem, movimentações e rastreabilidade.

Seu desenvolvimento representa a evolução da experiência de engenharia aplicada em soluções empresariais para um produto comercial especializado.

O Camposystem Stock possui estratégia de desenvolvimento e repositório próprios, com código-fonte comercial mantido em ambiente privado.

## Roadmap e evolução

O projeto evolui de forma incremental, com entregas organizadas por Sprints.

A evolução contempla arquitetura, domínio, APIs, segurança, persistência, testes e documentação técnica.

## Status do projeto

- Foundation: concluída.
- API e persistência: implementadas.
- Identidade e controle de acesso: implementados.
- Sprint 5 — Controle de Acesso: implementação funcional e testes principais validados.
- T07 — Documentação e revisão: em fechamento.

## Princípios de engenharia

- Entender antes de implementar.
- Adicionar tecnologias quando resolverem problemas reais.
- Documentar decisões arquiteturais.
- Evoluir o software de forma incremental.
- Priorizar simplicidade sem abrir mão da qualidade.
- Testar o comportamento antes de considerar uma funcionalidade concluída.

## Autor

**Alexandre de Campos**

Desenvolvedor Backend .NET | Engenharia de Software

- LinkedIn: https://linkedin.com/in/alexandre-camposystem
- GitHub: https://github.com/camposystem

## Direitos autorais e propriedade intelectual

© 2026 Camposystem. Todos os direitos reservados.

Este repositório é disponibilizado publicamente para fins de demonstração técnica, estudo e apresentação de arquitetura de software.

A disponibilização pública do código não representa concessão de licença para utilização comercial, reprodução, modificação ou redistribuição, salvo autorização expressa do titular dos direitos.

**Camposystem — Engineering & SaaS Solutions**
