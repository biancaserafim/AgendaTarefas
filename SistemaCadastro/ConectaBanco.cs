using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SistemaCadastro
{
    public class ConectaBanco
    {
        // Configuração da conexão com o banco 'sistema_agenda'
        private string connectionString = "server=localhost;uid=root;pwd=;database=sistema_agenda";

        // Variável para retornar mensagens de erro ou sucesso
        public string mensagem = "";

     
        // 1. LISTAR (Chama a procedure sp_ListaTarefas)
       
        public DataTable ListarTarefas()
        {
            DataTable tabela = new DataTable();

            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_ListaTarefas", conexao))
                    {
                        // Define que é uma Stored Procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                        {
                            adaptador.Fill(tabela);
                        }
                    }
                }
                catch (Exception ex)
                {
                    mensagem = "Erro ao listar: " + ex.Message;
                }
            }
            return tabela;
        }

        // 2. CADASTRAR (Chama a procedure sp_InsereTarefa)
      
        public bool CadastrarTarefa(string descricao, DateTime data, int concluida)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_InsereTarefa", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Passa os parâmetros para o banco
                        cmd.Parameters.AddWithValue("p_descricao", descricao);
                        cmd.Parameters.AddWithValue("p_data", data);
                        cmd.Parameters.AddWithValue("p_concluida", concluida);

                        cmd.ExecuteNonQuery();
                    }
                    mensagem = "Cadastro realizado com sucesso!";
                    return true;
                }
                catch (Exception ex)
                {
                    mensagem = "Erro ao cadastrar: " + ex.Message;
                    return false;
                }
            }
        }

       
        // 3. ALTERAR (Chama a procedure sp_AlteraTarefa)
       
        public bool AlterarTarefa(int id, string descricao, DateTime data, int concluida)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_AlteraTarefa", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_id", id);
                        cmd.Parameters.AddWithValue("p_descricao", descricao);
                        cmd.Parameters.AddWithValue("p_data", data);
                        cmd.Parameters.AddWithValue("p_concluida", concluida);

                        cmd.ExecuteNonQuery();
                    }
                    mensagem = "Alteração realizada com sucesso!";
                    return true;
                }
                catch (Exception ex)
                {
                    mensagem = "Erro ao alterar: " + ex.Message;
                    return false;
                }
            }
        }

        // 4. EXCLUIR (Chama a procedure sp_RemoveTarefa)
       
        public bool ExcluirTarefa(int id)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_RemoveTarefa", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_id", id);

                        cmd.ExecuteNonQuery();
                    }
                    mensagem = "Exclusão realizada com sucesso!";
                    return true;
                }
                catch (Exception ex)
                {
                    mensagem = "Erro ao excluir: " + ex.Message;
                    return false;
                }
            }
        }
    }
}