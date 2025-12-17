# 📋 Agenda de Tarefas

Projeto desenvolvido em **C# (Windows Forms)** com integração a **banco de dados SQL**, com o objetivo de gerenciar tarefas de forma simples, permitindo cadastrar, visualizar, editar e excluir registros.

---

## 🎯 Objetivo do Projeto

O sistema tem como finalidade organizar tarefas, armazenando-as em um banco de dados e exibindo-as em uma interface gráfica.  
O projeto foi desenvolvido com foco didático, aplicando conceitos de **CRUD**, **conexão com banco de dados** e **manipulação de dados em DataGridView**.

---

## 🛠️ Tecnologias Utilizadas

- C#
- Windows Forms
- SQL Server / MySQL (dependendo da configuração do banco)
- ADO.NET
- DataGridView

---

## 📂 Estrutura do Projeto

### Program.cs
Arquivo inicial da aplicação. Responsável por iniciar o sistema e abrir o formulário principal. Não contém regras de negócio.

---

### Formulário Principal
Responsável pela interface gráfica do sistema.  
Contém:
- Campos de entrada de dados (tarefa, data, status)
- Botões para inserir, editar e excluir tarefas
- DataGridView para exibição dos dados

Toda a lógica do sistema está concentrada no formulário, incluindo a conexão com o banco e a execução dos comandos SQL.

---

### Conexão com o Banco de Dados
A conexão com o banco é feita diretamente no formulário utilizando `SqlConnection` e uma **connection string**.  
A conexão é aberta apenas no momento da execução do comando e fechada logo após, evitando desperdício de recursos.

---

### Banco de Dados
O banco de dados possui uma tabela responsável por armazenar as tarefas.  
Cada registro contém um **ID único**, que é utilizado para identificar corretamente as tarefas durante operações de edição e exclusão.

---

### Caixa de ID (oculta)
O sistema utiliza uma caixa de texto invisível para armazenar o ID da tarefa selecionada no DataGridView.  
Esse ID não é exibido ao usuário, pois é um dado técnico usado apenas internamente para garantir que operações de UPDATE e DELETE sejam feitas no registro correto.

---

### DataGridView
Utilizado para exibir as tarefas cadastradas no banco de dados.  
Sempre que uma tarefa é inserida, editada ou excluída, os dados são buscados novamente no banco e o DataGridView é atualizado.

---

## 🔄 Funcionalidades

- Cadastrar tarefas
- Listar tarefas
- Editar tarefas
- Excluir tarefas
- Visualizar dados em tempo real no DataGridView

---

## 🔁 Fluxo de Funcionamento

1. O usuário preenche os campos do formulário
2. Clica em um botão de ação (Salvar, Editar ou Excluir)
3. O sistema executa o comando SQL correspondente
4. O banco de dados retorna o resultado
5. O DataGridView é atualizado com os dados mais recentes

---

## 🖼️ Telas do Sistema

### 📋 Listagem de Tarefas
Tela responsável por exibir todas as tarefas cadastradas no banco de dados.  
Permite buscar, selecionar uma tarefa e realizar ações como editar ou remover.

![Tela de Listagem](imagens/tela_listagem.png)

---

### ➕ Cadastro de Tarefas
Tela utilizada para cadastrar novas tarefas no sistema.  
O usuário informa o título da tarefa, a data e o status, e salva no banco de dados.

![Tela de Cadastro](imagens/tela_cadastro.png)



## 📌 Observações

Este projeto foi desenvolvido com fins acadêmicos, priorizando clareza e simplicidade na implementação, sem separação em camadas como DAO ou Repository, por se tratar de um sistema introdutório.

---

