# SistemaRestaurante

## Descrição do Projeto

Sistema de gerenciamento de restaurante, com controle de mesas, pedidos, produtos e garçons.
Feito em .NET 6 utilizando Entity Framework Core em forma de API REST e cliente em Razor Pages.

## Pré-requisitos

- .NET 6

## Como rodar a aplicação

- Clone este repositório
```bash
git clone https://github.com/gabrieldamke/SistemaRestaurante.git
```

- Navegue até a pasta do projeto
```bash
cd SistemaRestaurante
```

- Entre na pasta do projeto `Data`
```bash
cd src\Data
```

- Execute o comando para criar o banco de dados
```bash
dotnet ef database update -s ..\Api
```

- Volte para a pasta raiz do projeto
```bash
cd ..
```

### API

- Execute o comando para rodar a aplicação
```bash
dotnet run -p .\Api
```

- Acesse a aplicação em `https://localhost:5001` ou `http://localhost:5000`

### Web

- Execute o comando para rodar a aplicação
```bash
dotnet run -p .\Web
```

- Acesse a aplicação em `https://localhost:3001` ou `http://localhost:3000`