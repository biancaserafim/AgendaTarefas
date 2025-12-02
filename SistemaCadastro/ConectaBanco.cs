using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SistemaCadastro
{
    public class ConectaBanco
    {
        public const string SERVER = "localhost";
        public const int PORT = 3306;
        public const string DATABASE = "sistema_cadastro_aula";
        public const string USER = "root";
        public const string PASSWORD = "compServer";

        MySqlConnection conexao = new MySqlConnection(
            $"server={SERVER};" +
            $"port={PORT};" +
            $"database={DATABASE};" +
            $"user id={USER};" +
            $"password={PASSWORD};");

        public string mensagem;

        public bool insereBanda(Banda novaBanda)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd =
                    new MySqlCommand("sp_insereBanda", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", novaBanda.nome);
                cmd.Parameters.AddWithValue("integrantes", novaBanda.integrantes);
                cmd.Parameters.AddWithValue("ranking", novaBanda.ranking);
                cmd.Parameters.AddWithValue("genero", novaBanda.genero);
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }

        }// fim do insereBanda
    }
}
