# Requisitos — Identidade, autorização e isolamento multi-tenant

## 1. Objetivo

Definir os requisitos para controlar o acesso às operações de negócio do Camposystem ERP considerando a identidade autenticada, a organização solicitada e as permissões atribuídas ao usuário.

## 2. Requisitos funcionais

### RF-AUTH-001 — Autenticação

A API deverá validar os tokens JWT emitidos pelo provedor de identidade configurado, incluindo emissor, audiência, assinatura e validade.

**Critério de aceitação:** requisições sem token válido para endpoints protegidos deverão receber HTTP 401.

### RF-AUTH-002 — Identificação do usuário

O sistema deverá utilizar a claim `sub` do token validado para localizar o usuário interno por meio de `ExternalIdentityId`.

**Critério de aceitação:** a identidade externa não poderá ser substituída por um identificador arbitrário fornecido no corpo da requisição.

### RF-AUTH-003 — Identificação do tenant

O sistema deverá utilizar a claim `org_id` do token validado para localizar o tenant interno por meio de `ExternalId`.

**Critério de aceitação:** um tenant inexistente ou uma associação inexistente não poderá resultar em autorização concedida.

### RF-AUTH-004 — Associação entre usuário e tenant

O sistema deverá localizar a associação `TenantMembership` correspondente ao usuário e ao tenant identificados.

**Critério de aceitação:** a autorização deverá considerar exclusivamente os papéis associados à participação correspondente.

### RF-AUTH-005 — Avaliação de permissões

O sistema deverá avaliar as permissões concedidas pelos papéis da associação por meio da regra de domínio `TenantMembership.HasPermission()`.

**Critério de aceitação:** a permissão solicitada deverá ser verificada sem distinção entre maiúsculas e minúsculas, conforme a implementação atual do domínio.

### RF-AUTH-006 — Papéis por tenant

O sistema deverá permitir que cada tenant possua papéis próprios, inclusive papéis com nomes iguais aos de outros tenants.

**Critério de aceitação:** o nome de um papel não poderá determinar sua identidade globalmente; o tenant deverá fazer parte do contexto do papel.

### RF-AUTH-007 — Catálogo global de permissões

O sistema deverá manter um catálogo global de capacidades, como `products.create` e `products.list`.

**Critério de aceitação:** uma permissão será reutilizável por papéis de diferentes tenants, sem que sua existência no catálogo conceda acesso automaticamente.

### RF-AUTH-008 — Proteção do cadastro de produtos

O endpoint `POST /api/products` deverá exigir a permissão `products.create`.

**Critério de aceitação:** uma requisição autenticada sem essa permissão deverá receber HTTP 403; uma requisição autorizada poderá prosseguir para o processamento da operação.

### RF-AUTH-009 — Proteção da listagem de produtos

A listagem de produtos deverá ser protegida pela permissão `products.list`, após sua inclusão explícita na política de autorização.

**Observação:** o endpoint deverá ser atualizado para exigir essa política antes de considerar o requisito implementado.

### RF-AUTH-010 — Persistência de identidade

As entidades de identidade e autorização deverão ser mapeadas por um `IdentityDbContext`, separado do `ProductDbContext`.

**Critério de aceitação:** as configurações e migrations de identidade deverão ser administráveis separadamente das migrations de produtos.

## 3. Requisitos não funcionais

### RNF-AUTH-001 — Isolamento entre tenants

A autorização deverá impedir que papéis ou permissões de uma organização sejam utilizados para autorizar operações em outra organização.

### RNF-AUTH-002 — Integridade referencial

O banco deverá aplicar chaves estrangeiras, restrições de unicidade e demais mecanismos de integridade necessários para proteger os relacionamentos de identidade e autorização.

### RNF-AUTH-003 — Testabilidade

As regras de domínio, as consultas de autorização e a integração com o ASP.NET Core deverão possuir testes automatizados adequados às respectivas responsabilidades.

### RNF-AUTH-004 — Atualização de permissões

A revogação de uma permissão ou de um papel deverá ser refletida nas próximas avaliações de autorização, conforme a estratégia de consistência adotada. Não deverá existir cache de permissões sem política explícita de invalidação.

### RNF-AUTH-005 — Separação de responsabilidades

A API deverá declarar as permissões exigidas; a Application deverá coordenar a consulta; a Infrastructure deverá acessar os dados; e o domínio deverá manter a regra de autorização de negócio.

### RNF-AUTH-006 — Observabilidade e segurança

Falhas de autorização poderão ser registradas para auditoria, sem expor tokens, credenciais ou dados sensíveis nos logs.

## 4. Critérios gerais de teste

A suíte de testes deverá contemplar, no mínimo:

- Token ausente ou inválido.
- Usuário interno inexistente.
- Tenant inexistente.
- Associação inexistente.
- Usuário com permissão no tenant correto.
- Usuário sem a permissão solicitada.
- Usuário com permissão em outro tenant, mas não no tenant solicitado.
- Tentativa de atribuir papel de outro tenant.
- Revogação de permissão e comportamento nas avaliações seguintes.

# Regras de negócio — Identidade e autorização multi-tenant

## RN-IDENT-001 — Identidade interna

Cada usuário interno deverá ser associado à sua identidade externa por `ExternalIdentityId`.

O identificador externo deverá ser único no contexto do provedor de identidade utilizado.

## RN-IDENT-002 — Identificação do tenant

Cada tenant deverá possuir um `ExternalId` que permita relacioná-lo à organização correspondente no Auth0.

O identificador externo deverá ser único.

## RN-IDENT-003 — Associação usuário-tenant

Um usuário poderá possuir associações com múltiplos tenants.

Deverá existir, no máximo, uma `TenantMembership` para cada combinação de `UserId` e `TenantId`.

## RN-IDENT-004 — Papéis específicos por tenant

Cada papel deverá pertencer a exatamente um tenant.

Dois tenants poderão possuir papéis com o mesmo nome e permissões diferentes.

A combinação de `TenantId` e `Name` deverá ser única, conforme a política de nomenclatura adotada.

## RN-IDENT-005 — Atribuição de papéis

Uma `TenantMembership` somente poderá receber papéis pertencentes ao mesmo tenant da associação.

Uma tentativa de atribuição incompatível deverá ser rejeitada.

## RN-IDENT-006 — Catálogo global de permissões

Cada permissão representará uma capacidade definida pelo sistema e terá um nome globalmente único.

Exemplos:

- `products.create`
- `products.list`
- `products.update`

A existência de uma permissão no catálogo não concede acesso por si só.

## RN-IDENT-007 — Concessão de permissões

As permissões efetivas de uma associação serão determinadas pelas permissões dos papéis atribuídos a essa associação.

Um usuário somente possuirá uma permissão se pelo menos um de seus papéis na associação do tenant solicitado conceder essa permissão.

## RN-IDENT-008 — Verificação de permissão

A decisão de autorização de negócio deverá utilizar a regra `TenantMembership.HasPermission()`.

A comparação do nome da permissão será insensível a maiúsculas e minúsculas, conforme o comportamento atual do domínio.

## RN-IDENT-009 — Isolamento de autorização

Uma permissão concedida a um usuário em um tenant não deverá ser considerada válida automaticamente em outro tenant.

Toda avaliação deverá estar vinculada à associação entre o usuário identificado e o tenant solicitado.

## RN-IDENT-010 — Ausência de concessão implícita

A autenticação bem-sucedida, a existência do usuário ou a associação do usuário a um tenant não concederão permissões automaticamente.

A autorização dependerá dos papéis e permissões efetivamente atribuídos.

## RN-IDENT-011 — Falha segura

Se o usuário, o tenant ou a associação não puderem ser localizados, ou se a permissão não for encontrada nos papéis da associação, a autorização deverá ser negada.

Falhas de infraestrutura não deverão ser interpretadas como concessão de acesso.

## RN-IDENT-012 — Proteção por operação

Cada endpoint protegido deverá declarar a permissão necessária à operação.

Para o cadastro de produtos, a permissão exigida será `products.create`.

Para a listagem de produtos, a permissão prevista será `products.list`.

## RN-IDENT-013 — Distinção entre autenticação e autorização

A ausência de credenciais válidas em um endpoint protegido deverá resultar em HTTP 401.

A presença de identidade autenticada sem autorização suficiente deverá resultar em HTTP 403.

## RN-IDENT-014 — Revogação

A remoção de um papel de uma associação ou a remoção de uma permissão de um papel deverá impedir que essa concessão seja considerada nas avaliações subsequentes, respeitadas eventuais transações em andamento e a estratégia explícita de cache.

## RN-IDENT-015 — Integridade das relações

O sistema deverá preservar a integridade dos relacionamentos entre usuário, tenant, associação, papel e permissão, tanto nas regras de domínio quanto na persistência, quando aplicável.

As restrições de banco deverão impedir duplicidades e referências inválidas de acordo com o modelo relacional definido.