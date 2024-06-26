# Razor-MVC
## Introdução
Este é um projeto ASP.NET Core em padrão MVC, com intuito de demonstrar operações CRUD utilizando _Entity Framework_ em um banco de dados local SQLite, com validações de dados _client-side_ e _server-side_.

## Como Funciona
Por seguir o padrão MVC, o projeto é dividido em Models, Views e Controllers. Os dois principais Controllers são ```ProdutosController``` e ```FornecedoresController```, responsáveis pela definição de ações relacionadas ao banco de dados, algumas das quais com suas respectivas Views (ao exemplo de _Index_, _Create_, _Edit_ e _Delete_). As classes ```Produto``` e ```Fornecedor```, por sua vez, consistem nos principais Models do projeto, onde são definidas as estruturas dos dados com suas respectivas anotações.

Quanto aos dados, o projeto também conta com uma classe intitulada ```StorageContext```, dentro da pasta ```Data```, com a responsabilidade de estabelecer o contexto do banco de dados. Além disso, também é responsável por estabelecer algumas regras internas ao banco através da sobrecarga do método ```OnModelCreating()```. Resumidamente, o banco SQLite foi estruturado da seguinte forma:
1. Tabela _Produtos_:
   -
   A tabela de produtos conta com seis campos diferentes:
   
   - **Id:** É a chave primária, do tipo ```INTEGER```. Seu equivalente no Model ```Produto``` também é do tipo ```int```. Por padrão, esta propriedade não pode ser nula.
   - **Nome:** Propriedade do tipo ```TEXT```, com equivalente ```string``` no Model, onde conta com uma anotação ```Required```. Por motivos de identificação, não pode ser nula e precisa ser única.
   - **Descrição:** Tipo ```TEXT```, com equivalente ```string``` no Model, passível de ser nula.
   - **Preço:** Propriedade do tipo ```INTEGER```, respeitando o padrão em SQLite de armazenar valores em moeda nesse formato. No respectivo Model, sua propriedade equivalente é do tipo ```decimal```. A conversão acontece da seguinte forma: o valor '1' equivale a '0,01' no Model, ou seja, um centavo. Além de não poder ser nula, essa propriedade também conta com um CHECK, de acordo com o qual o preço precisa ser maior do que zero. Da mesma forma, a propriedade "Preço" no Model conta com uma anotação do tipo ```Range``` para validar se o valor é positivo e se não extrapola o máximo permitido pelo banco.
   - **FornecedorId:** É uma chave estrangeira, do tipo ```INTEGER```, responsável por relacionar cada produto com a tabela de fornecedores. Caso um fornecedor seja excluído, a chave vinculada ao produto é alterada para _null_.
   - **DataDeCriação:** Propriedade do tipo ```TEXT```, respeitando o padrão de armazenar datas em texto no SQLite. Sua propriedade equivalente no Model é do tipo ```DateTime```. O valor padrão da data de criação é definido no momento em que determinado dado é inserido no banco.
2. Tabela _Fornecedores_:
   - 
   A tabela de fornecedores, por sua vez, conta com apenas três campos:
   
   - **Id**: É a chave primária, do tipo ```INTEGER```. Seu equivalente no Model ```Fornecedor``` também é do tipo ```int```. Por padrão, esta propriedade não pode ser nula.
   - **Nome**: Propriedade do tipo ```TEXT```, com equivalente ```string``` no Model. Assim como em Produtos, não pode ser nula e deve ser única por motivos de identificação, contando no Model com uma anotação ```Required```.
   - **Telefone**: Propriedade do tipo ```INTEGER```, com equivalente do tipo ```int``` no Model. Pode ser nula; no entanto, caso inclusa, conta com um CHECK que verifica se o número fornecido tem nove dígitos. A propriedade equivalente no Model também inclui uma anotação do tipo ```Range```, para verificar se os nove dígitos estão presentes e também se o primeiro dígito é '9', já que este é o padrão brasileiro.

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
>>**ATENÇÃO:** Caso as ferramentas da interface de linha de comando do Entity Framework Core não estejam instaladas no seu sistema, o arquivo _.bat_ as instalará antes de executar os comandos necessários.
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
