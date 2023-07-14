<a href="https://dotnet.microsoft.com/pt-br/download/dotnet/6.0" target="_blank"><img alt=".NET Badge" src="https://img.shields.io/badge/.NET-v06-blue"><a/>

# teste-nivelamento-csharp-api

Este repositório foi criado para realizar um teste de nivelamento em C# com .NET CORE, GIT e banco de dados. O foco desse projeto é avaliar minhas habilidades e conhecimentos. Nesse teste contém 5 questões, três desafios práticos de codificação, um desafio pratico usando GIT e um desafio utilizando banco de dados.

Agradeço antecipadamente pela atenção e a oportunidade de demonstrar meus conhecimentos.

## :computer: Sobre o projeto

Este é um projeto em .NET 6, portanto, certifique-se de ter o SDK e os recursos utilizados correspondente instalado em sua máquina antes de prosseguir.

### Recursos utilizados

Antes de começar, verifique se sua máquina atende aos seguintes recursos utilizados:

* [.NET 6 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/)
* [GIT](https://git-scm.com/downloads)

## :fire: Como baixar e executar o projeto

Siga as etapas abaixo para baixar e executar o projeto em sua máquina:

1. Clone este repositório em sua máquina local:
 ```
 https://github.com/ffresanto/teste-nivelamento-csharp-api.git
 ```

2. Navegue até o diretório do projeto

3. Abra a solution __Exercicio.sln__
   
4. Escolha qual projeto da questão que será analisada, conforme exemplo abaixo.

![startproject](https://github.com/ffresanto/teste-nivelamento-csharp-api/assets/44379238/98dfad4f-368c-4d5b-8efb-9b80a2440e74)

5. Faça o build no projeto e depois só executar.

## 📋 Questão 1

<img src="https://github.com/ffresanto/teste-nivelamento-csharp-api/assets/44379238/a279cec6-d548-43b1-8c80-7f1cb87639ae" width="520">

## 📋 Questão 2

<img src="https://github.com/ffresanto/teste-nivelamento-csharp-api/assets/44379238/a5029cf3-7258-48a7-b550-fa0ad551243d" width="520">

## 📋 Questão 3

A questão 3 era apenas para realizar a sequência de comandos de GIT e ver o resultado final que ficara na pasta, no meu caso o resultado foi a reposta abaixo:

```
[ ] script.js e style.css, apenas.
[ ] default.html e style.css, apenas.
[x] style.css, apenas.
[ ] default.html e script.js, apenas.
[ ] default.html, script.js e style.css.
 ```

## 📋 Questão 4

A questão 4 era apenas para montar a query correspondente a pergunta que está no documento da pasta da Questão 4.

```
SELECT at.assunto, at.ano, COUNT(*) quantidade 
  FROM atendimentos at
GROUP BY at.assunto, at.ano
HAVING COUNT(*) > 3
ORDER BY at.ano, COUNT(*) DESC;
```

## 📋 Questão 5

A questão 5 envolve a criação de uma API com dois serviços básicos, utilizando boas práticas de codificação, CQRS com Mediator, Dapper, Testes unitários e Swagger. Mesmo que seja apenas dois serviços básicos, gostei bastante desse exercício, pois me permitiu aprender mais sobre o NSubstitute nos testes unitario e CQRS com Mediator, além de aprender novas práticas de desenvolvimento.

## 🔴 Endpoints

### Criar uma movimentação

- **URL**
  `/api/movement`
  
Endereço | Método | Descrição
---|---|---
/api/movement/| POST | Movement

- **Corpo da Requisição**

 O corpo da requisição deve ser um objeto JSON com as seguintes propriedades:

  ```json
  {
    "idRequest": "string",
    "accountNumber": "string",
    "value": number,
    "typeMovement": "string"
  }
  ```
 Parâmetro | Tipo | Descrição
---|---|---
idRequest| string | UUID da requisição, para teste usar o site para gerar o [UUID](https://www.uuidtools.com/v4)
accountNumber | string | Pode ser o UUID do idcontacorrente ou apenas o número da conta gravados no sqlite
value | number | Valor da movimentação
typeMovement | string | Tipo de movimentação, aceitando apenas valores "D" ou "C" (Debito ou Credito)

 Resposta da requisição:
  ```json
  {
    "description": "string",
    "result": number
  }
  ```

### Obter saldo da conta

Retorna o saldo da conta com base no número da conta.

- **URL**
  `/api/accountbalance/{accountNumber}`
  
Endereço | Método | Descrição
---|---|---
api/accountbalance/{accountNumber}| GET | AccountBalance

- **Parâmetros de URL**

| Parâmetro       | Tipo   | Descrição                    |
|-----------------|--------|------------------------------|
| `accountNumber` | string | Número da conta, Pode ser o UUID do idcontacorrente ou apenas o número da conta gravados no sqlite |


 Resposta da requisição - Status 200:
  ```json
  {
   "accountNumber": number,
   "accountHolder": "string",
   "consultationDate": "string",
   "balanceValue": number
  }
  ```

 Resposta da requisição - Status 400 Bad Request:
  ```json
  {
    "description": "string",
    "result": number
  }
  ```
