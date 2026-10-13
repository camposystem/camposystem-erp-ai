
# ADR-0007 – Evolução Arquiteturial para Controle de Acesso, Auditoria e Mensageria 
- **Status:** Aceita
- **Data:** 01/10/2026
- **Autor:** Alexandre de Campos
---

## Contexto
Precisamos evoluir o ERP para:
- controlar quem pode executar operações;
- definir permissões;
- rastrear operações relevantes;
- desacoplar o processamento de auditoria;
- introduzir mensageria de forma incremental.
E temos a necessidade de estudar RabbitMQ sem introduzir tecnologia sem justificativa de negócio.
---

## Decisão

O Camposystem utilizará um Identity Provider compatível com OpenID Connect (OIDC) e OAuth 2.0 para autenticação. Auth0 Organizations será utilizado para representar a associação de identidades às organizações/empresas. O Camposystem manterá a entidade Tenant para representar o contexto empresarial e suas regras de negócio. O org_id recebido no contexto de autenticação será utilizado para resolver o Tenant correspondente, enquanto o módulo de autorização do Camposystem será responsável pelas permissões de negócio.

Vamos evoluir incrementalmente:

Controle de acesso
       ↓
Auditoria
       ↓
RabbitMQ
       ↓
Worker
       ↓
ACK/NACK
       ↓
Retry
       ↓
DLQ
       ↓
Idempotência

O RabbitMQ será inicialmente utilizado para processamento assíncrono relacionado à auditoria.
---

## Alternativas consideradas

**Processamento síncrono**
API → Auditoria

**RabbitMQ**
API → RabbitMQ → Worker → Auditoria

**Kafka**
API → Kafka → consumidores
Kafka não será introduzido neste momento. Sua adoção será reavaliada após a implementação da mensageria e do Audit Worker, de acordo com as necessidades reais do negócio e da arquitetura.

---

## Consequências
Positivas:
- desacoplamento;
- processamento assíncrono;
- possibilidade de retry;
- DLQ;
- maior rastreabilidade;
- laboratório real de mensageria.
Custos:
- maior complexidade;
- RabbitMQ passa a ser uma dependência;
- necessidade de observabilidade;
- tratamento de mensagens duplicadas;
- necessidade de idempotência.
---

