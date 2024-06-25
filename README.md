# Razor-MVC
## Introdução
Este é um projeto ASP.NET Core em padrão MVC, com intuito de demonstrar operações CRUD utilizando _Entity Framework_ em um banco de dados local SQLite, com validações de dados _client-side_ e _server-side_.

## Como Funciona
Por seguir o padrão MVC, o projeto é dividido em Models, Views e Controllers. No caso, os dois principais Controllers são: ```ProdutosController``` e ```FornecedoresController```.

## Decisões de Design
O projeto foi estilizado com a framework CSS **Bootstrap**, com a implementação de ícones Bootstrap. Para tornar o projeto mais interativo e amigável ao usuário, foram inclusos _toasts_ e _modal dialogs_, cuja ativação se dá por Javascript. Ambos estão implementados na subpasta Shared, junto com as demais Views do projeto.

## Como Configurar e Executar
Por padrão, o projeto já está configurado com uma migração inicial e um banco de dados local pré-preenchido. Nesse caso, há duas principais formas de executar o projeto:
1. Debug no Visual Studio (ou na IDE de sua preferência):
   - Clonar o repositório, abrir o projeto com sua IDE e executá-lo em _debug_.
2. Publicando o projeto:
   - Caso prefira publicar o projeto localmente, é preciso abrir a pasta em que o projeto foi publicado e então, com um _prompt_ de comando aberto, executar o comando ```dotnet RazorMVC.dll```. Em seguida, basta abrir o navegador de sua preferência e acessar o caminho do _localhost_ indicado no _prompt_ (por exemplo: ```http://localhost:5000```).


> [!NOTE]
>Caso, por algum motivo, seja necessário recriar a migração e o banco de dados desde o início, foi incluso um arquivo _.bat_ na pasta _src_ do projeto chamado ```AddInitialMigration.bat```, o qual, resumidamente, apaga o banco de dados e migrações existentes, depois cria uma nova migração e atualiza o banco.
>>ATENÇÃO: Caso as ferramentas da interface de linha de comando do Entity Framework Core não estejam instaladas no seu sistema, o arquivo _.bat_ as instalará antes de executar os comandos necessários.
> 
> Alternativamente, também é possível executar as ações manualmente no Package Manager Console no Visual Studio. Para isso, é preciso executar, em ordem:
> 
> 1. Para apagar o banco de dados:
> ```
> Drop-Database
> ```
> 2. Para remover as migrações existentes:
> ```
> Remove-Migration
> ```
> 3. Para adicionar uma nova migração, chamada ```InitialCreate```:
>```
> Add-Migration InitialCreate
> ```
>4. Por fim, para adicionar o novo banco de dados:
> ```
> Update-Database
> ```
> Pronto! Depois disso, basta executar o projeto da maneira como preferir. Caso o banco de dados ```Storage.db``` não contenha nenhum dado, ele será populado pela Model ```SeedDatabase.cs``` automaticamente.
