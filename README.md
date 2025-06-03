# fiap.cloud-games.net

Antes de compilar o projeto, certifique-se que tenha instalado:
- [.NET Core SDK 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [Node.js](https://nodejs.org/en/)
- [Docker](https://www.docker.com/products/docker-desktop)

## Inicializar o submódulo
Antes de compilar o projeto, é necessário inicializar o submódulo do repositório.

```
$ git submodule update --init --recursive
```

## Git bash
Todos os comandos que iniciam com `$` devem ser executados em um console com suporte ao bash script, como o `Git bash`.

## Atribuindo variáveis de ambiente
É recomendado que as variáveis de ambiente: 

- `ASPNETCORE_ENVIRONMENT` 
- `DOTNET_EnableWriteXorExecute`

Sejam definidas nas variáveis de ambiente do sistema operacional.  
Podem ser definidas temporariamente no terminal com os comandos abaixo: 

```
$ export ASPNETCORE_ENVIRONMENT=Local
$ echo $ASPNETCORE_ENVIRONMENT

$ export DOTNET_EnableWriteXorExecute=0
$ echo $DOTNET_EnableWriteXorExecute
```

ou no PowerShell:

```
$env:ASPNETCORE_ENVIRONMENT="Local"
$env:DOTNET_EnableWriteXorExecute=0
dir env:
```	

Para os sistemas Linux, execute o comando abaixo para salvar permanentemente a variável no profile do bash

```
echo ASPNETCORE_ENVIRONMENT=Local >> ~/.bashrc
echo DOTNET_EnableWriteXorExecute=0 >> ~/.bashrc
```

## Antes de criar migrations, é necessário instalar o ef tools
```
$ dotnet tool install --global dotnet-ef --version 8.0.16
```

## Criando e Atualizando tabelas na base de dados
### Para criar uma nova migration:
```
$ cd src/Core/Fiap.Cloud.Games.Core.Infra
$ dotnet ef --startup-project ../../UI/Fiap.Cloud.Games.UI.Api migrations add migration_name_here --output-dir Repositories/EF/Migrations --verbose
```

### Como atualizar o banco de dados com as migrations criadas
```
$ cd src/Core/Fiap.Cloud.Games.Core.Infra
$ dotnet ef --startup-project ../../UI/Fiap.Cloud.Games.UI.Api database update --output-dir Repositories/EF/Migrations --verbose
```

## Definindo infraestrutura para execução do projeto
### Criação da rede no docker
Esse comando precisa ser executado apenas uma vez, caso já possua a `fiap-cloud` criada, pule para o próximo passo.  
Todos os containeres precisam ficar na mesma rede no docker para que possam trocar informações.  
Para isso, vamos criar uma rede chamada fiap-cloud executando o comando abaixo:

```
$ docker network create fiap-cloud
```

### Criação do SQL Server no docker
Esse comando precisa ser executado apenas uma vez, caso já possua o container do `SQL Server`, pule para o próximo passo.  
Criação do volume para persistência dos dados:

```
$ docker volume create sqlvolume
```

Criação do container do SQL Server:

```
$ docker run --network fiap-cloud -e "ACCEPT_EULA=Y" -e 'MSSQL_SA_PASSWORD=@Abc1234' -p 1433:1433 --name sql-database -v sqlvolume -d mcr.microsoft.com/mssql/server:2022-latest 
```