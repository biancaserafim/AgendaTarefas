using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaCadastro
{
    public partial class Sistema : Form
    {
        string stringConexao = "server=localhost;uid=root;pwd=;database=sistema_agenda;SslMode=Preferred";

        public Sistema()
        {
            InitializeComponent();
            dgvTarefas.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //seleciona linha toda
        }

        // carrega dados ao iniciar
        private void Sistema_Load(object sender, EventArgs e)
        {
            ListarTarefas();
        }

        // salva ou edita tarefa
        private void BtnConfirmaCadastro_Click(object sender, EventArgs e)
        {
            if (txtTarefa.Text == "")
            {
                MessageBox.Show("Digite o nome da tarefa!");
                return;
            }

            string sql = "";
            bool isUpdate = false;

            if (txtId.Text == "")
                sql = "INSERT INTO tarefas (titulo, data_tarefa, status) VALUES (@titulo, @data, @status)";
            else
            {
                sql = "UPDATE tarefas SET titulo=@titulo, data_tarefa=@data, status=@status WHERE id=@id";
                isUpdate = true;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(stringConexao))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@titulo", txtTarefa.Text);
                        cmd.Parameters.AddWithValue("@data", dtpData.Value);
                        cmd.Parameters.AddWithValue("@status", cbStatus.Text);
                        if (txtId.Text != "") cmd.Parameters.AddWithValue("@id", txtId.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show(isUpdate ? "Editado!" : "Salvo!");
                LimparCampos();
                ListarTarefas();
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        // exclui tarefa selecionada
        private void btnRemoveBanda_Click(object sender, EventArgs e)
        {
            if (dgvTarefas.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Apagar?", "Confirma", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string id = dgvTarefas.SelectedRows[0].Cells["id"].Value.ToString();
                        using (MySqlConnection con = new MySqlConnection(stringConexao))
                        {
                            con.Open();
                            string sql = "DELETE FROM tarefas WHERE id = @id";
                            using (MySqlCommand cmd = new MySqlCommand(sql, con))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        ListarTarefas();
                        LimparCampos();
                    }
                    catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
                }
            }
        }

        // busca tarefas no banco
        private void ListarTarefas()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(stringConexao))
                {
                    con.Open();
                    string sql = "SELECT id, titulo, data_tarefa, status FROM tarefas WHERE titulo LIKE @busca ORDER BY data_tarefa";
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        string busca = "";
                        Control[] res = Controls.Find("txtBusca", true);
                        if (res.Length > 0) busca = ((TextBox)res[0]).Text;
                        cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvTarefas.DataSource = dt;
                    }
                }
            }
            catch { }
        }

        // preenche campos para edicao
        private void CarregarDados()
        {
            if (dgvTarefas.SelectedRows.Count > 0)
            {
                var linha = dgvTarefas.SelectedRows[0];
                txtId.Text = linha.Cells["id"].Value.ToString();
                txtTarefa.Text = linha.Cells["titulo"].Value.ToString();
                if (DateTime.TryParse(linha.Cells["data_tarefa"].Value.ToString(), out DateTime d)) dtpData.Value = d;
                cbStatus.Text = linha.Cells["status"].Value.ToString();
            }
        }

        // reseta o formulario
        private void LimparCampos()
        {
            txtId.Text = "";
            txtTarefa.Text = "";
            cbStatus.SelectedIndex = -1;
            dtpData.Value = DateTime.Now;
        }

        // vai para aba cadastro
        private void btnCadastra_Click(object sender, EventArgs e)
        {
            MudarAba(0);
            LimparCampos();
        }

        // vai para aba busca
        private void btnBusca_Click(object sender, EventArgs e)
        {
            MudarAba(1);
            ListarTarefas();
        }

        // troca indice do tabcontrol
        private void MudarAba(int index)
        {
            Control[] res = Controls.Find("tabControlPrincipal", true);
            if (res.Length > 0) ((TabControl)res[0]).SelectedIndex = index;
        }

        // prepara edicao e muda aba
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTarefas.SelectedRows.Count > 0)
            {
                CarregarDados();
                MudarAba(0);
            }
        }

        // pesquisa enquanto digita
        private void txtBusca_TextChanged(object sender, EventArgs e) => ListarTarefas();

        // carrega dados ao clicar na grade
        private void dgvTarefas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) CarregarDados();
        }

        // metodos vazios para o designer
        private void dgvBandas_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void dtpData_ValueChanged(object sender, EventArgs e) { }
        public void panel1_Paint(object sender, PaintEventArgs e) { }
        private void dgvTarefas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // encerra aplicacao
        private void button2_Click(object sender, EventArgs e) => Application.Exit();
    
    // métodos exigidos pelo Designer (não remover)
private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

    } 

}