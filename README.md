# Sistema de gerenciamento de cadastro - Odontoprev

### Objetivo do projeto:
Este projeto é voltado para a Odontoprev com o intuito de reduzir os sinistros, permitindo o gerenciamento de informações de pacientes e dentistas. O sistema oferece uma visão clara dos usuários, facilitando a consulta de dados e a verificação de consultas. Além disso, em breve, contará com uma inteligência artificial que analisará informações para classificar se um usuário é suspeito de fraude, ajudando as seguradoras a tomar decisões mais informadas e seguras.

### Escopo:
A aplicação terá as seguintes funcionalidades:
- Cadastro de Pacientes
- Cadastro de Dentistas
- Gerenciamento de Consultas
- Visualização de informações de Pacientes e Dentistas

### Requisitos funcionais:
- O sistema deve permitir o cadastro de pacientes
- O sistema deve permitir o cadastro de dentistas
- O sistema deve perimitir a visualização e edição de informações de Pacientes e Dentistas
### Requisitos não funcionais:
- O sistema deve ser responsivo e funcionar em diferentes dispositivos
- O sistema deve ter as melhores práticas de segurança
- O sistema deve ser capaz de processar os dados em menos de 2 segundos para a maioria das solicitaçõe

Este projeto foi desenvolvido utilizando o conceito de **Clean Architecture** para que seja legível e permitir uma maior manutenibilidade e escalabilidade da aplicação.

## Camadas da Aplicação:
### Apresentação
- **Controllers**: Gerenciar as requisições e respostas, interagindo com o **service**
### Aplicação
- **Services**: Contém a lógica para realizar as operações
### Domínio
- **Models**: Representam as entidades
### Infraestrutura
- **Repositories**: Lógica de acesso aos dados utilizando o Entity Framework Core
- **Config**: Configurações de conexão com o banco de dados e mapeamento das entidades

### Tecnologias utilizadas
- **C#**: Linguagem de programação utilizada
- **Entity Framework Core**: Utilizado para manipulação do banco de dados
- **Oracle**: Banco de dados utilizado para a persistência dos dados
- **Swagger**: Ferramenta de documentação interativa da API 

## Como executar o projeto:

Essa aplicação utiliza o Swagger para a documentação e teste da API, ele é aberto automaticamente ao executar o programa.

### 1. Clone o repositório ou faça o download do projeto:
```bash
git clone https://github.com/celestemayumi/challenge-c-sharp.git
```
#### Caso tenha clonado o repositório:
### 2. Navegue até o diretório do projeto
```bash
cd challenge-c-sharp
```
### 3. Restaure as dependências:
```bash
dotnet restore
```
### 4. Execute a aplicação:
```bash
dotnet run
```

A aplicação irá abrir o navegador com o Swagger, caso não abra acesse:

https://localhost:7107/swagger


  

