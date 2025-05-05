# Isthmus.Produtos.Api

API REST em .NET 8 para cadastro, edição, consulta e desativação lógica de produtos.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) instalado e em execução

## Executando a aplicação com Docker Compose

1. Abra o terminal na raiz da pasta `Isthmus.Produtos.Api` (onde está localizado o `docker-compose.yml` e o `Dockerfile`).
2. Execute os comandos abaixo:

```bash
docker compose build
docker compose up
```

O serviço da API será exposto na porta `8080` e poderá ser acessado via:

[http://localhost:8081/swagger](http://localhost:8081/swagger)

## Endpoints disponíveis

A API expõe as seguintes rotas via Swagger:

- `POST /api/produtos` — Cadastrar ou atualizar produto
- `PUT /api/produtos/{codigo}` — Editar produto existente
- `GET /api/produtos/pesquisar?texto=...` — Pesquisar produto por código ou nome
- `DELETE /api/produtos/{codigo}` — Remoção lógica do produto

## Observações

- O container do SQL Server é criado automaticamente ao subir a aplicação.
- O nome da base de dados utilizada é `ProdutosDb`.
- A conexão é feita por `Server=db;Database=ProdutosDb;User Id=sa;Password=Your_password123;`.

---

Desenvolvido com .NET 8 e Docker
