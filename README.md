# Practical Test

## Como rodar a API
- Por padrão, usa banco in-memory (não precisa Oracle).
- Para requests funcionarem no Heroku/local, basta rodar normalmente.
- Para usar Oracle real, configure a variável de ambiente `OracleConnection`.

## Endpoints
- GET /api/transactions
- GET /api/transactions/{id}
- POST /api/transactions
- PUT /api/transactions/{id}
- DELETE /api/transactions/{id}

## Testes
- Execute `dotnet test` para rodar os testes unitários.

## SQL
- Scripts de criação e PL/SQL estão na pasta `/sql`.

## System Design
- Veja o diagrama e explicação em `/docs/system-design.md`.

## Code Review
- Veja análise em `/docs/code-review.md`.

## Algoritmo
- Veja solução em `/docs/algorithm.md`.

## Deploy no Heroku (Docker)

Este projeto já está pronto para deploy no Heroku usando Docker, seguindo o padrão mais moderno e recomendado:

- **Não é necessário heroku.yml**
- **Não use git push heroku main**
- O deploy é feito via Container Registry do Heroku, usando o workflow do GitHub Actions.

### Como funciona o deploy

1. O workflow `.github/workflows/heroku-docker-deploy.yml` faz:
   - Login no Container Registry do Heroku
   - Build da imagem Docker
   - Push da imagem para o Heroku
   - Release da imagem para o app
2. O deploy é disparado automaticamente ao dar push na branch `main` ou manualmente pelo GitHub Actions.

### Pré-requisitos

- O segredo `HEROKU_API_KEY` deve estar configurado em **Settings > Secrets and variables > Actions** do repositório no GitHub.
- O nome do app no workflow (`practical-test-api`) deve ser igual ao nome do app criado no Heroku.

### Observações

- Não é necessário buildpack, heroku.yml ou deploy via git.
- O Dockerfile já está pronto para produção e expõe a porta correta para o Heroku.

Se precisar rodar o deploy manualmente, basta acessar o GitHub Actions, selecionar o workflow "Deploy Docker to Heroku" e clicar em "Run workflow".

<!-- Trigger redeploy: ajuste forçado para garantir build/push Docker no Heroku -->

## Sessão de Testes da API

### 1. Listar todas as transações
- **GET** `/api/transactions`
- **Retorno esperado:** array de transações (mock/Sales.txt)

### 2. Buscar uma transação por ID
- **GET** `/api/transactions/{id}`
- **Exemplo:** `/api/transactions/T1`
- **Retorno esperado:** transação encontrada ou 404

### 3. Criar uma nova transação
- **POST** `/api/transactions`
- **Body exemplo:**
```json
{
  "transactionID": "T3",
  "accountID": "A3",
  "transactionAmount": 123.45,
  "transactionCurrencyCode": "USD",
  "localHour": 10,
  "transactionScenario": "Online",
  "transactionType": "Purchase",
  "transactionIPaddress": "127.0.0.1",
  "ipState": "NY",
  "ipPostalCode": "10001",
  "ipCountry": "US",
  "isProxyIP": false,
  "browserLanguage": "en-US",
  "paymentInstrumentType": "CreditCard",
  "cardType": "Visa",
  "paymentBillingPostalCode": "10001",
  "paymentBillingState": "NY",
  "paymentBillingCountryCode": "US",
  "shippingPostalCode": "10001",
  "shippingState": "NY",
  "shippingCountry": "US",
  "cvvVerifyResult": "M",
  "digitalItemCount": 1,
  "physicalItemCount": 1,
  "transactionDateTime": "2024-06-19T20:20:51.3083268Z"
}
```
- **Retorno esperado:** transação criada (201)

### 4. Atualizar uma transação existente
- **PUT** `/api/transactions/{id}`
- **Body:** igual ao POST, alterando algum campo
- **Retorno esperado:** transação atualizada ou 404

### 5. Deletar uma transação
- **DELETE** `/api/transactions/{id}`
- **Retorno esperado:** 204 se deletou, 404 se não existir

### 6. Buscar transação inexistente
- **GET** `/api/transactions/ID_QUE_NAO_EXISTE`
- **Retorno esperado:** 404

### 7. Criar transação inválida
- **POST** `/api/transactions`
- **Body:** omita campos obrigatórios ou envie valores inválidos
- **Retorno esperado:** 400 com mensagem de erro de validação

> Use o Swagger (`/swagger`) para testar facilmente todos os endpoints.
