# Trabalho Vendinha 

Sistema de gerenciamento de clientes e dívidas desenvolvido em C# .NET com SQLite.

---

##  Descrição

Aplicação de console que permite cadastrar clientes, registrar dívidas e controlar pagamentos. Os dados são persistidos em um banco de dados SQLite, garantindo que as informações sejam mantidas entre as execuções do programa.

---

##  Funcionalidades

- **Cadastrar cliente** — registra nome, CPF, e-mail e data de nascimento
-  **Editar cliente** - permite que o usuario edite dados dos clientes
-  ** Deletar cliebte** permite que o usuario delete os dados dos clientes
- **Listar clientes** — exibe todos os clientes com suas dívidas
- **Adicionar dívida** — registra uma dívida em aberto para um cliente (apenas uma por vez)
- **Deletar dívida** -- permite que o usuario delete uma divida
- **Pagar dívida** — marca a dívida em aberto como paga e registra a data de pagamento
- **Buscar cliente por nome** — filtra clientes pelo nome, ordenados por quem deve mais
- **Listar por paginação** — exibe 10 clientes por vez, ordenados por maior dívida

---

##  Tecnologias

- C# / .NET 10
- Entity Framework Core 10
- SQLite
- DBeaver (criação do banco)

---

##  Como rodar o projeto

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [DBeaver](https://dbeaver.io/) ou qualquer cliente SQLite

### Configuração do banco de dados

1. Crie a pasta `C:\datas\sqlite\` no seu computador
2. Abra o DBeaver e crie um novo banco SQLite em `C:\datas\sqlite\vendinha.db`
3. Execute o seguinte SQL para criar as tabelas:

```sql
CREATE TABLE Clientes (
    id_cliente      INTEGER PRIMARY KEY AUTOINCREMENT,
    nome            VARCHAR(100) NOT NULL,
    cpf             VARCHAR(11) UNIQUE NOT NULL,
    data_nascimento DATETIME,
    email           VARCHAR(100)
);

CREATE TABLE Dividas (
    id_divida      INTEGER PRIMARY KEY AUTOINCREMENT,
    valor          DECIMAL,
    paga           BOOLEAN,
    data_criacao   DATETIME,
    data_pagamento DATETIME,
    id_cliente     INT,
    FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente)
);
```

---

##  Estrutura do projeto

```
TrabalhoVendinha/
├── Controllers/
│   └── ClienteController.cs   # Controla o menu e a interação com o usuário
├── Data/
│   └── AppDbContext.cs         # Configuração do Entity Framework e mapeamento das tabelas
├── Models/
│   ├── Cliente.cs              # Modelo de cliente com validações
│   └── Divida.cs               # Modelo de dívida
├── Service/
│   └── ClienteService.cs       # Regras de negócio e acesso ao banco
└── Program.cs                  # Ponto de entrada da aplicação
```

---

##  Validações

- Nome deve começar com letra maiúscula e conter nome e sobrenome
- CPF deve conter exatamente 11 dígitos numéricos e ser único
- E-mail deve ser válido
- Um cliente só pode ter uma dívida em aberto por vez

---

## Autores

Otavio Zampronio
Felipe Rodrigues de souza
