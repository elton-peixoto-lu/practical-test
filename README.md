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
