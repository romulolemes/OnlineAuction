# OnlineAuction

OnlineAuction é um MVP (Minimum Viable Product) de uma aplicação web criada para o gerenciamento de leilões online. 
Nesse MVP você irá encontrar a solução backend e frontend do produto. 
Abaixo encontrasse a lista de funcionalidaes implementadas:

- Login:
  - Função: autenticação na aplicação para acesso as funcionalidades
  - Campos: usuário, senha
- Restrições:
  - Todos os campos são obrigatórios
  - Usuários desativados não poderão acessar o sistema

- Logout:
  - Função: logoff da aplicação
  - Restrições:
    - Nenhum serviço pode ser acessado após o logout da aplicação

- Cadastro de leilão (CRUD):
  - Função: Cadastrar/Visualizar/Editar e Excluir novos leilões
  - Campos: nome do leilão, valor inicial, indicador se o item leiloado é "usado", usuário responsável pelo leilão, data de abertura e finalização
  - Restrições:
    - Todos os campos são obrigatórios

- Listagem de leilões
  - Função: Listar todos os leilões existentes
  - Campos: nome do leilão, valor inicial, indicador se o item leiloado é "usado" e usuário responsável pelo leilão, indicador se o leilão está foi finalizado

## Pré-requisito
* [Angular CLI](https://cli.angular.io/)
* [Node.js](https://nodejs.org/en/)
* [NPM](https://www.npmjs.com/)
* [.NET Core](https://docs.microsoft.com/pt-br/dotnet/core/)

## Configuração

#### Angular 2 Application
##### Download das dependências do projeto
Use `npm install` para realizar o download das depenedências necessárias para esse projeto.

#### Angular CLI Commands
##### Development server
Execute `ng serve` em um terminal para startup o servidor da aplicação. Navegue para `http://localhost:4200/`.

#### .NET Application
Usando a IDE de sua preferencia (Visual Studio, Rider) abra a Solução `OnlineAuction.sln`. Em seguida após a abertura do projeto e realizado o Build do mesmo, execute as aplicações `OnlineAuction.API` e `Security.Auth`. Tal procedimento irá startup o backend da aplicação.

## Autor
Rômulo Rocha Lemes
