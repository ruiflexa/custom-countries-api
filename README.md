# Countries API
Desafio Técnico - Countries API

# Acesso e Autenticação
A API utiliza autenticação Basic e contém um usuário pré-existente (usuário: teste, senha: test) 

# Arquitetura da Aplicação
A solução da API baseia-se em projetos .NET Core 3.1, tendo sua arquitetura definida conforme as camadas a seguir:
![image](https://user-images.githubusercontent.com/23639567/116620225-ea966980-a917-11eb-8bbe-bbe85729c9bf.png)

Segue abaixo a lista dos principais pacotes/plugins que a API utiliza, todos instalados via NuGet:

Swagger - Responsável pela documentação online de uso e versionamento da API;
GraphQL.Client - Client para realizar conexão com o GraphQL
JsonFlatDataStore - Utilizado para manipulação de dados em arquivos json
xUnit, AspNetCore.TestHost e FluentAssertions - Pacotes utilizados nos testes de integraçao
Dockerfile - Arquivo para preparar a imagem caso queira subir a API em Docker
