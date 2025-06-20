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
