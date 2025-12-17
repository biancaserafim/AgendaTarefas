using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SistemaCadastro
{
    public class ConectaBanco
    {
        // dados de conexao
        private string connectionString = "server=localhost;uid=root;pwd=;database=sistema_agenda";

        // armazena avisos do sistema
        public string mensagem = "";

        // stored procedure
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
                        cmd.CommandType = CommandType.StoredProcedure; // define uso de procedure
                        using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                        {
                            adaptador.Fill(tabela);
                        }
                    }
                }
                catch (Exception ex) { mensagem = "Erro: " + ex.Message; }
            }
            return tabela;
        }

        // insere nova tarefa via procedure
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
                        cmd.Parameters.AddWithValue("p_descricao", descricao);
                        cmd.Parameters.AddWithValue("p_data", data);
                        cmd.Parameters.AddWithValue("p_concluida", concluida);
                        cmd.ExecuteNonQuery();
                    }
                    mensagem = "Salvo!";
                    return true;
                }
                catch (Exception ex) { mensagem = "Erro: " + ex.Message; return false; }
            }
        }

        // altera tarefa existente via procedure
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
                    mensagem = "Alterado!";
                    return true;
                }
                catch (Exception ex) { mensagem = "Erro: " + ex.Message; return false; }
            }
        }

        // remove tarefa via procedure
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
                    mensagem = "Excluido!";
                    return true;
                }
                catch (Exception ex) { mensagem = "Erro: " + ex.Message; return false; }
            }
        }
    }
}