# PRODUTO

Os Produtos de um ERP varejista modular, é preciso garantir a integridade dos dados operacionais e deixar os ganchos prontos para as integrações fiscais e contábeis.

Abaixo estão os requisitos e regras de negócio essenciais para este domínio, considerando as propriedades e as expansões necessárias para a operação do dia a dia.

## Requisitos Funcionais (RF)
__RF01__ - Cadastro de Produto: O sistema deve permitir a criação, edição e visualização dos produtos com os atributos mínimos: Nome, SKU, Preço, Descrição e Status (Ativo/Inativo).

__RF02__ - Gerenciamento de Status: O sistema deve permitir ativar ou inativar um produto a qualquer momento.

__RF03__ - Busca e Filtragem: O sistema deve permitir buscar produtos por SKU, Nome ou Status (IsActive).

__RF04__ - Histórico de Alteração de Preço: O sistema deve registrar o histórico de alterações do preço de venda com data, hora e usuário responsável.

## Regras de Negócio (RN)
### 1. Identificação e Código (SKU)
__RN01__ - Unicidade do SKU: O SKU deve ser único em todo o sistema. Não podem existir dois produtos cadastrados com o mesmo SKU.

__RN02__ - Formato do SKU: O SKU não pode conter espaços em branco ou caracteres especiais. Deve aceitar apenas letras, números e hífen/underline (ex: PROD-12345).

__RN03__ - Imutabilidade do SKU: Após a criação e associação a movimentações operacionais (vendas, estoque ou compras), o SKU não pode ser alterado.

### 2. Nome e Descrição
__RN04__ - Obrigatoriedade do Nome: O Name é obrigatório, devendo ter entre 3 e 120 caracteres.

__RN05__ - Descrição Opcional: A Description é opcional, mas se preenchida deve ter um limite máximo (ex: 1.000 caracteres).

### 3. Precificação (Price)
__RN06__ - Valor Mínimo: O Price pode ser nulo na criação do produto. Não é permitido cadastrar produtos com preço negativo ou zerado.

__RN07__ - Precisão Monetária: O valor deve aceitar até 2 casas decimais.

### 4. Ciclo de Vida e Operação (IsActive)
__RN08__ - Padrão de Cadastro: Todo novo produto deve ser cadastrado por padrão com IsActive = false.

__RN09__ - Para o produto IsActive = true, price deve ser > 0 e Nome e SKU valido não nulo.

__RN10__ - Bloqueio de Comercialização (Inativo): Produtos com IsActive = False:

- Não podem ser adicionados a novos Pedidos de Venda ou Orcamentos no PDV.

- Não podem ser adicionados a novas Ordens de Compra.

__RN11__ - Proibição de Exclusão Física (Soft Delete): O sistema não deve permitir a exclusão física (DELETE) de produtos que já possuem histórico de vendas, compras ou movimentação de estoque. Nesses casos, o produto deve ser apenas inativado (IsActive = False).