# 📒 Agenda de Tarefas

Sistema de agenda desenvolvido em **C# (Windows Forms)**, com integração a **banco de dados MySQL**, permitindo o gerenciamento de tarefas por meio das operações básicas de um CRUD.

O projeto foi desenvolvido com fins **acadêmicos**, com foco no entendimento prático de conexão com banco de dados, uso de procedures e manipulação de dados em interface gráfica.

---

## 🛠️ Tecnologias Utilizadas

- **C#**
- **Windows Forms**
- **MySQL**
- **SQL (Stored Procedures)**

### Ferramentas
- Visual Studio  
- MySQL Workbench  
- Aiven

---

## ⚙️ Funcionalidades do Sistema

- Cadastrar tarefas  
- Listar tarefas cadastradas  
- Buscar tarefas pelo título  
- Editar tarefas existentes  
- Remover tarefas  
- Definir status da tarefa (Pendente / Concluída)

---

## 📋 Tela de Listagem de Tarefas

Tela responsável por exibir todas as tarefas cadastradas no banco de dados.  
Utiliza um **DataGridView** para apresentação dos dados, permitindo selecionar uma tarefa e executar ações de **edição** ou **remoção**, além da funcionalidade de **busca**.

![Tela de Listagem](https://raw.githubusercontent.com/biancaserafim/AgendaTarefas/main/imagens/tela_listagem.png)

---

## ➕ Tela de Cadastro de Tarefas

Tela utilizada para o cadastro de novas tarefas no sistema.  
O usuário informa:
- Descrição da tarefa  
- Data  
- Status  

Após o preenchimento, os dados são salvos diretamente no banco de dados.

![Tela de Cadastro](https://raw.githubusercontent.com/biancaserafim/AgendaTarefas/main/imagens/tela_cadastro.png)

---

## 🗄️ Banco de Dados

O sistema utiliza **MySQL** como banco de dados, com acesso realizado por meio da biblioteca `MySql.Data`.

As operações de listagem, inserção, atualização e remoção são realizadas utilizando **Stored Procedures**, garantindo maior organização e segurança nas consultas SQL.

---

## 🧠 Estrutura do Projeto

- **Forms (Windows Forms)**  
  Responsáveis pela interface gráfica e interação com o usuário.

- **Classe de Conexão**  
  Centraliza a conexão com o banco de dados e executa as procedures.

- **DataGridView**  
  Utilizado para exibir e selecionar registros do banco de dados.

---

## 📌 Observações Importantes

Este projeto **não utiliza separação em camadas** como DAO ou Repository.  
Essa decisão foi intencional, pois o objetivo é **didático**, focado no aprendizado inicial de:

- CRUD  
- Conexão com banco de dados  
- Uso de Stored Procedures  
- Integração entre interface gráfica e banco  

---
