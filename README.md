# Projeto de APIs para Gerenciamento de Vendas

Este projeto disponibiliza um conjunto de APIs para gerenciar vendas, incluindo funcionalidades de criação, atualização, exclusão e recuperação de vendas. Foi desenvolvido utilizando .NET Core 8 e utiliza PostgreSQL como banco de dados. O projeto também inclui Swagger para facilitar o teste e a documentação das APIs.

## Tecnologias Utilizadas

- **.NET Core 8**
- **PostgreSQL**
- **Swagger**

## Configuração do Ambiente

### 1. Configurar o Banco de Dados

Instale o PostgreSQL e configure a string de conexão no arquivo `appsettings.json`. Certifique-se de que o banco de dados esteja rodando corretamente antes de proceder.

### 2. Executar as Migrações

Abra o Package Manager Console e execute os seguintes comandos para criar as tabelas necessárias no banco de dados:

```bash
Add-Migration AddSaleTable
Update-Database

### 2. Executar as apis pelo swagger ou postman