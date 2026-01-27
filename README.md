#
# Ambev Developer Evaluation

Projeto de avaliação desenvolvido em .NET 8 com Docker.

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [Docker](https://www.docker.com/get-started) (versão 20.10 ou superior)
- [Docker Compose](https://docs.docker.com/compose/install/) (versão 2.0 ou superior)

## 🚀 Como executar o projeto

Siga os passos abaixo para executar o projeto localmente:

### 1. Iniciar o banco de dados

Primeiro, inicie o serviço do banco de dados em background:

```bash
docker-compose up -d ambev.developerevaluation.database
```

### 2. Build da API

Em seguida, faça o build da aplicação Web API sem utilizar cache:

```bash
docker-compose build --no-cache ambev.developerevaluation.webapi
```

### 3. Iniciar todos os serviços

Por fim, inicie todos os serviços:

```bash
docker-compose up
```

## 📚 Acessando a documentação da API

Após executar os comandos acima, a aplicação estará disponível e você poderá acessar a documentação Swagger através do navegador:

```
http://localhost:8080/swagger
```

## 🛑 Parando os serviços

Para parar todos os serviços em execução:

```bash
docker-compose down
```

Para parar e remover os volumes (dados do banco serão perdidos):

```bash
docker-compose down -v
```

## 🔧 Tecnologias utilizadas

- .NET 8
- Docker
- Docker Compose
- Swagger/OpenAPI

## ✉️ Contato

Para mais informações, entre em contato através de [pedrolyradev@outlook.com](mailto:pedrolyradev@outlook.com).