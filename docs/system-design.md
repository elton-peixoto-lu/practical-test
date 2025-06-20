# System Design: Book Recommendation Service

## High-Level Architecture

- **Frontend:** Web/Mobile App
- **API Gateway:** Gerencia autenticação, rate limiting, roteamento.
- **Serviço de Recomendações:** Microserviço dedicado, pode usar Machine Learning.
- **Serviço de Usuários:** CRUD de usuários, autenticação.
- **Serviço de Livros:** CRUD de livros, metadados.
- **Banco de Dados:** 
  - Usuários/Livros: SQL (PostgreSQL, MySQL)
  - Recomendações: NoSQL (Redis, Cassandra)
- **Fila de Mensagens:** Kafka/RabbitMQ para processar eventos (ex: novas avaliações).
- **Cache:** Redis para respostas rápidas.
- **Monitoramento:** Prometheus, Grafana.
- **Escalabilidade:** Kubernetes/Containers.

## Diagrama (sugestão)

```
[User] -> [API Gateway] -> [Recommendation Service] -> [NoSQL DB]
                                 |-> [User Service] -> [SQL DB]
                                 |-> [Book Service] -> [SQL DB]
                                 |-> [Cache]
                                 |-> [Message Queue]
```

## Tecnologias e padrões:
- **Microservices** para escalabilidade independente.
- **API Gateway** para centralizar segurança e roteamento.
- **Cache** para performance.
- **Fila de Mensagens** para desacoplar processamento.
- **Banco relacional** para dados estruturados, **NoSQL** para performance em recomendações.
- **Kubernetes** para orquestração e alta disponibilidade.

## Considerações:
- **Escalabilidade:** Microserviços e banco NoSQL para recomendação.
- **Disponibilidade:** Replicação, balanceamento de carga, fallback.
- **Performance:** Cache, consultas otimizadas, processamento assíncrono. 
