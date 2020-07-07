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

- SDK do .NET Core.
Link para instação https://docs.microsoft.com/pt-br/dotnet/core/install/sdk

- Download dependencia do projeto
Use `npm install` para baixar as dependencias necessárias para esse projeto.

## Build e execução

Usando a IDE de sua preferencia (Visual Studio, Rider) abra a Solução `OnlineAuction.sln`. Em seguida após a abertura do projeto e realizado o Build do mesmo, execute as aplicações `OnlineAuction.API` e `Security.Auth`.
Tal procedimento irá startup o backend da aplicação.

Em seguida, abra no terminal na pasta raiz da aplicação e execute a linha de comando a seguir para realizar o startup do frontend da aplicação
Execute `ng serve` no terminal para startup o servidor da aplicação frontend. 
 
Após realizar os procedimento acima, você estará apto a utilizar as funcionalidades da aplicação que podem ser acessadas através do link *https://localhost:4200/*


## Autor
Rômulo Rocha Lemes
