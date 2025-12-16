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
            dgvTarefas.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleção de linha inteira
        }

        private void Sistema_Load(object sender, EventArgs e)
        {
            ListarTarefas();
        }

        // ----------------------------------------------------------------------
        // OPERAÇÕES DO BANCO DE DADOS (CRUD)
        // ----------------------------------------------------------------------

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
                isUpdate = true; // Marca como Edição
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

                        if (txtId.Text != "")
                            cmd.Parameters.AddWithValue("@id", txtId.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                if (isUpdate)
                {
                    MessageBox.Show("Tarefa editada com sucesso!"); // Mensagem para Edição
                }
                else
                {
                    MessageBox.Show("Tarefa cadastrada com sucesso!"); // Mensagem para Cadastro
                }

                LimparCampos();
                ListarTarefas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }

        private void btnRemoveBanda_Click(object sender, EventArgs e) // Método de Remoção (compatibilidade)
        {
            if (dgvTarefas.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Tem certeza que deseja apagar esta tarefa?", "Apagar", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                        MessageBox.Show("Apagado com sucesso!");
                        ListarTarefas();
                        LimparCampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao apagar: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para apagar.");
            }
        }

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
                        Control[] foundControls = Controls.Find("txtBusca", true);
                        if (foundControls.Length > 0 && foundControls[0] is TextBox)
                        {
                            busca = ((TextBox)foundControls[0]).Text;
                        }

                        cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvTarefas.DataSource = dt;

                        // Renomeia as colunas para exibição
                        dgvTarefas.Columns["id"].HeaderText = "Cód";
                        dgvTarefas.Columns["titulo"].HeaderText = "Tarefa";
                        dgvTarefas.Columns["data_tarefa"].HeaderText = "Data";
                        dgvTarefas.Columns["status"].HeaderText = "Situação";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar: " + ex.Message);
            }
        }

        private void CarregarDados()
        {
            // Transfere os dados da linha selecionada do Grid para os campos
            if (dgvTarefas.SelectedRows.Count > 0)
            {
                var linha = dgvTarefas.SelectedRows[0];
                txtId.Text = linha.Cells["id"].Value.ToString(); // Preenche o ID para o UPDATE
                txtTarefa.Text = linha.Cells["titulo"].Value.ToString();

                if (DateTime.TryParse(linha.Cells["data_tarefa"].Value.ToString(), out DateTime data))
                {
                    dtpData.Value = data;
                }

                cbStatus.Text = linha.Cells["status"].Value.ToString();
            }
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtTarefa.Text = "";
            cbStatus.SelectedIndex = -1;
            dtpData.Value = DateTime.Now;
        }

        // ----------------------------------------------------------------------
        // NAVEGAÇÃO E EVENTOS DE INTERFACE
        // ----------------------------------------------------------------------

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            if (Controls.Find("tabControlPrincipal", true).Length > 0)
                ((TabControl)Controls.Find("tabControlPrincipal", true)[0]).SelectedIndex = 0;
            else if (Controls.Find("tabItemCadastrar", true).Length > 0)
                ((TabControl)Controls.Find("tabItemCadastrar", true)[0]).SelectedIndex = 0;

            LimparCampos();
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            if (Controls.Find("tabControlPrincipal", true).Length > 0)
                ((TabControl)Controls.Find("tabControlPrincipal", true)[0]).SelectedIndex = 1;
            else if (Controls.Find("tabItemCadastrar", true).Length > 0)
                ((TabControl)Controls.Find("tabItemCadastrar", true)[0]).SelectedIndex = 1;

            ListarTarefas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        // ----------------------------------------------------------------------
        // FUNÇÕES DE BUSCAR E EDITAR/EXCLUIR
        // ----------------------------------------------------------------------

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            ListarTarefas();
        }

        private void dgvTarefas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvTarefas.Rows[e.RowIndex].Selected = true;
                CarregarDados(); // Carrega dados para habilitar botões
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTarefas.SelectedRows.Count > 0)
            {
                CarregarDados();

                if (Controls.Find("tabControlPrincipal", true).Length > 0)
                    ((TabControl)Controls.Find("tabControlPrincipal", true)[0]).SelectedIndex = 0;
                else if (Controls.Find("tabItemCadastrar", true).Length > 0)
                    ((TabControl)Controls.Find("tabItemCadastrar", true)[0]).SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para editar.", "Sistema de Agenda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ----------------------------------------------------------------------
        // MÉTODOS DE COMPATIBILIDADE (Obrigatorios para evitar erros de compilação)
        // ----------------------------------------------------------------------

        private void dgvBandas_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void dtpData_ValueChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        public void panel1_Paint(object sender, PaintEventArgs e) { }
        public void btnAlterar_Click(object sender, EventArgs e) { }
        public void btnConfirmaAlteracao_Click(object sender, EventArgs e) { }
        public void bntAddGenero_Click(object sender, EventArgs e) { }
        public void label4_Click(object sender, EventArgs e) { }
        private void dgvTarefas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}