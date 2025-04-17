# TaskListApp

Aplicação **full stack** de lista de tarefas, composta por:

| Camada   | Tecnologia                              | Container (Docker)    |
|----------|-----------------------------------------|-----------------------|
| Backend  | ASP.NET 8 (DDD + Clean Code)            | **tasklist-backend**  |
| Frontend | Angular 19                              | **tasklist-frontend** |
| Banco    | PostgreSQL                              | **postgres-tasklist** |

Funcionalidades principais:

* **API REST** em .NET para CRUD de tarefas  
* **Interface web** em Angular  
* **Banco de Dados** em PostgreSQL  
* **Swagger** para documentação dos endpoints  
* Backend estruturado em **DDD e princípios Clean Code**

Docker orquestra os três serviços para um setup rápido e reprodutível.

---

## Pré‑requisitos

| Ferramenta | Versão mínima | Observação                                  |
|------------|---------------|---------------------------------------------|
| Docker     | 24            | Docker Desktop ativo                        |
| Git        | —             | Clonar o repositório                        |
| .NET       | 8             | Se quiser desenvolver o backend localmente  |
| Node/NPM   | 22.14.0       | Se quiser desenvolver o frontend localmente |
| Angular    | 19.2.7        | Se quiser desenvolver o frontend localmente |

---


## Como executar o projeto

Siga o passo a passo abaixo para rodar o projeto completo (back-end, banco de dados e front-end) utilizando Docker.

### 0. Criar rede Docker (necessário para que os containers se comuniquem)

Antes de subir os containers do banco e da API, crie a rede compartilhada:

`docker network create tasklist-network`

### 1. Clone o repositório e acesse o diretório do projeto

`git clone https://github.com/Th1Ag011/TaskListApp.git`
`cd TaskListApp`

### 2. Gere a imagem Docker do back-end

Entre na pasta do projeto back-end e publique a imagem com o perfil de container configurado:

`cd TaskListApp`
`dotnet publish -p:PublishProfile=DefaultContainer`

### 2.1 Execute o contêiner do back-end:

`docker run --name tasklist-backend --network tasklist-network -p 7054:8080 tasklist-api:latest`

### 3. Crie o contêiner do banco de dados PostgreSQL

Volte para a raiz do projeto (TaskListApp) e execute o comando abaixo para iniciar o banco:

`docker run --name postgres-tasklist --network tasklist-network -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=thiagoTaskList -e POSTGRES_DB=TaskListDB -p 5432:5432 -d postgres`

### 4. Rode as migrations para criar as tabelas no banco

Volte para a pasta do back-end `cd TaskListApp` e execute:

`dotnet ef database update`


### 5. (Opcional) Acesse o banco de dados manualmente, dentro da pasta do back-end rode

`docker exec -it postgres-tasklist psql -U postgres -d TaskListDB`

### 5.1 Listar tabelas:
`\dt`

### 5.2 Consultar registros:
`SELECT * FROM "Tasks";`

### 6.0 Gere e execute a imagem do front-end (Angular)

Acesse a pasta do front-end:

`cd ../TaskListApp.FrontEnd`

### 6.1 Crie a imagem:

`docker build -t tasklist-frontend .`

### 6.2 Execute o contêiner:

`docker run -p 4200:4200 --name tasklist-frontend tasklist-frontend`

### 7. Acesse a aplicação no navegador

Com todos os contêineres rodando, abra o navegador e acesse:

`http://localhost:4200`


