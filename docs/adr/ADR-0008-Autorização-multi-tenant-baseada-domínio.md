# ADR-0008 — Autorização multi-tenant baseada no domínio

- **Status:** Aceito
- **Data:** 25/10/2026
- **Contexto:** Camposystem ERP
- **Decisão relacionada:** ADR-0007 — Autenticação baseada em OIDC/OAuth 2.0, Auth0 como provedor de identidade inicial e autorização de negócio no Camposystem.

## Contexto

O Camposystem ERP é uma aplicação multi-tenant na qual usuários podem participar de diferentes organizações e possuir permissões distintas em cada uma delas.

O Auth0 é responsável pela autenticação e utiliza Organizations para associar identidades a organizações. O Camposystem mantém sua própria entidade `Tenant` e é responsável pelas regras de autorização de negócio.

O domínio já possui as entidades `User`, `Tenant`, `TenantMembership`, `Role` e `Permission`. A entidade `TenantMembership` implementa o método `HasPermission(string permissionName)`, que verifica as permissões concedidas pelos papéis associados à participação do usuário.

É necessário definir como essas regras serão persistidas, consultadas e integradas à autorização do ASP.NET Core, preservando o isolamento entre organizações e a separação de responsabilidades da Clean Architecture.

## Decisões

### 1. Autorização de negócio no Camposystem

O Auth0 será responsável pela autenticação. O Camposystem será responsável por determinar se o usuário autenticado pode executar uma operação de negócio.

As claims `sub` e `org_id` do JWT validado serão utilizadas para localizar, respectivamente, a identidade interna do usuário e o tenant correspondente.

### 2. Papéis específicos por tenant

Cada `Role` pertencerá a um único `Tenant`.

Diferentes tenants poderão definir papéis com o mesmo nome, mas com conjuntos de permissões distintos.

A atribuição de papéis ocorrerá por meio de `TenantMembership`, e não diretamente por `User`.

### 3. Catálogo global de permissões

A entidade `Permission` representará capacidades globais do sistema, como `products.create` e `products.list`.

Os papéis específicos de cada tenant determinarão quais dessas capacidades serão concedidas aos usuários associados.

### 4. Domínio como referência da regra de autorização

A regra de autorização permanecerá centralizada em `TenantMembership.HasPermission(string permissionName)` e `TenantMembership.HasPermission()` que suporta a avaliação das permissões solicitadas pela operação, inclusive múltiplas permissões.

A infraestrutura será responsável por recuperar as entidades e os relacionamentos necessários. A consulta deverá respeitar a associação entre o usuário e o tenant solicitado.

### 5. EF Core para persistência e consulta

A implementação inicial utilizará EF Core e PostgreSQL, sem introduzir uma view de autorização como fonte independente da decisão de negócio.

### 6. Contexto de persistência separado

Será criado um `IdentityDbContext` para persistir as entidades de identidade e autorização, mantendo o `ProductDbContext` dedicado à persistência de produtos.

Os contextos poderão utilizar a mesma conexão PostgreSQL, sem exigir bancos de dados distintos.

### 7. Relacionamentos

O modelo deverá contemplar:

- `User` para `TenantMembership`: um para muitos.
- `Tenant` para `TenantMembership`: um para muitos.
- `Tenant` para `Role`: um para muitos.
- `TenantMembership` para `Role`: muitos para muitos.
- `Role` para `Permission`: muitos para muitos.

As relações muitos-para-muitos deverão ser mapeadas explicitamente no EF Core, com tabelas de junção e restrições de integridade apropriadas.

### 8. Autorização declarativa na API

Os endpoints declararão as permissões necessárias por meio das políticas do ASP.NET Core.

O endpoint de criação de produtos exigirá a política `products.create`. A autorização será integrada por um `AuthorizationHandler`, que consultará o mecanismo de autorização da aplicação.

## Alternativas consideradas

### A. EF Core e domínio — escolhida

Permite aproveitar o modelo existente e manter a regra de autorização no domínio. A eficiência das consultas será avaliada por meio de testes e, se necessário, medições de desempenho.

### B. View PostgreSQL

Poderia simplificar determinadas consultas relacionais, mas introduziria uma projeção adicional e exigiria cuidados para não duplicar as regras de negócio.

Não será adotada na implementação inicial. Poderá ser reavaliada caso existam evidências de necessidade.

## Consequências positivas

- Centralização das regras de autorização no domínio.
- Isolamento dos papéis por tenant.
- Catálogo consistente de permissões.
- Separação entre autenticação e autorização de negócio.
- Separação da persistência de identidade e produtos.
- Possibilidade de testar regras de negócio independentemente da infraestrutura.

## Consequências e custos

- Necessidade de mapear as entidades de identidade no EF Core.
- Necessidade de configurar tabelas de junção, chaves estrangeiras e restrições de unicidade.
- Aumento da complexidade de configuração e teste dos contextos de persistência.
- Necessidade de garantir que a atribuição de papéis respeite o tenant da associação.

## Critérios de validação

A implementação deverá demonstrar que:

1. Usuários sem associação válida não recebem permissões.
2. Papéis de um tenant não podem ser atribuídos a associações de outro tenant.
3. A autorização considera o tenant identificado pelo token validado.
4. As permissões são avaliadas pelo domínio.
5. Endpoints protegidos diferenciam falhas de autenticação de falhas de autorização.
6. As alterações de persistência podem ser aplicadas e testadas sem comprometer o contexto de produtos.

## Resultado da implementação

A implementação foi validada com:

- Autenticação JWT via Auth0.
- Autorização baseada em políticas do ASP.NET Core.
- Isolamento de permissões por tenant.
- Resposta HTTP 401 para requisições sem autenticação válida.
- Resposta HTTP 403 para usuários autenticados sem permissão.
- Criação de produto autorizada: HTTP 201.
- Consulta de produtos: HTTP 200.
- Testes automatizados: 163 aprovados, 0 falhas.