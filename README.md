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

### 0. Criar rede Docker (necessário para que os containers se comuniquem) - Abra o terminal e execute:

`docker network create tasklist-network`

### 1. Clone o repositório e acesse o diretório do projeto

`git clone https://github.com/Th1Ag011/TaskListApp.git`
`cd TaskListApp`

### 2. Gere a imagem Docker do back-end

Agora, vamos compilar e publicar a API com o perfil de container Docker.

### 2.1 Acesse a pasta onde está o projeto da API:
`cd TaskListApp.Api` 

### 2.2 Entre na subpasta onde está o .csproj da API:
`cd TaskListApp`

### 2.3 Gere a imagem Docker do back-end
`dotnet publish -p:PublishProfile=DefaultContainer` 

### 2.4 Execute o contêiner do back-end:
`docker run --name tasklist-backend --network tasklist-network -p 7054:8080 tasklist-api:latest`

### 3. Crie o contêiner do banco de dados PostgreSQL

### 3.1 Volte para a raiz do projeto back-end (TaskListApp.Api) e execute:

`docker run --name postgres-tasklist --network tasklist-network -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=thiagoTaskList -e POSTGRES_DB=TaskListDB -p 5432:5432 -d postgres`

### 4. Rode as migrations para criar as tabelas no banco

### 4.1 Volte novamente para a pasta onde está o .csproj da API:

`cd .\TaskListApp\ ` 

### 4.2 Vá ate o arquivo appsettings.json localizado no TaskListApp e modifique a Host do DefaultConnection para Host=localhost, depois de salvado. 
### Execute:

`dotnet ef database update`

### 5. (Opcional) Acesse o banco de dados manualmente, dentro da pasta do back-end TaskListApp.Api execute

`docker exec -it postgres-tasklist psql -U postgres -d TaskListDB`

### 5.1 Listar tabelas:
`\dt`

### 5.2 Consultar registros:
`SELECT * FROM "Tasks";`

### 6.0 Gere e execute a imagem do front-end

Volte para a raiz do projeto e acesse a pasta do front-end:

`cd ../TaskListApp.FrontEnd`

### 6.1 Crie a imagem Docker do front-end:

`docker build -t tasklist-frontend .`

### 6.3 Execute o contêiner do front:

`docker run -p 4200:4200 --name tasklist-frontend tasklist-frontend`

### 7. Acesse a aplicação no navegador

Com todos os contêineres rodando, abra o navegador e acesse:

`http://localhost:4200`
