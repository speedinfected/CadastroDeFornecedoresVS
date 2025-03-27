using CadastroDeFornecedores;
using CadastroFornecedores;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly FornecedorService _fornecedorService;
        private readonly IServiceFactory _serviceFactory;

        public MainForm()

        {
            InitializeComponent();
            _serviceFactory = new ServiceFactory();
            _fornecedorService = new FornecedorService(_serviceFactory);
            dataGridViewFornecedores.SelectionChanged += dataGridViewFornecedores_SelectionChanged;
            CarregarFornecedores();
        }

        private void AtualizarEstadoBotoes()
        {
            bool temRegistros = dataGridViewFornecedores.Rows.Count > 0;
            bool linhaSelecionada = dataGridViewFornecedores.SelectedRows.Count > 0;

            btnEditar.Enabled = temRegistros && linhaSelecionada;
            btnExcluir.Enabled = temRegistros && linhaSelecionada;
        }


        private void CarregarFornecedores()
        {
            try
            {
                dataGridViewFornecedores.DataSource = _fornecedorService.ListarFornecedores();
                dataGridViewFornecedores.AutoResizeColumns();
                dataGridViewFornecedores.ReadOnly = true;
                AtualizarEstadoBotoes(); // Atualiza os botões após carregar
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar fornecedores: {ex.Message}\n\nVerifique:\n1. Se o MySQL está rodando\n2. Se o banco 'cadastro_fornecedores' existe\n3. Se a tabela 'fornecedores' foi criada",
                                "Erro de Conexão",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                AtualizarEstadoBotoes(); // Atualiza mesmo em caso de erro
            }
        }

        private void dataGridViewFornecedores_SelectionChanged(object sender, EventArgs e)
        {
            AtualizarEstadoBotoes();
        }


        private void btnEditar_Click(object sender, EventArgs e)

        {

            if (dataGridViewFornecedores.SelectedRows.Count == 0)

            {

                MessageBox.Show("Selecione um fornecedor para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }



            int id = Convert.ToInt32(dataGridViewFornecedores.SelectedRows[0].Cells["Id"].Value);

            var cadastroForm = new CadastroForm(_serviceFactory, id); // Usando o novo construtor

            if (cadastroForm.ShowDialog() == DialogResult.OK)

            {

                CarregarFornecedores();

            }

        }



        private void btnExcluir_Click(object sender, EventArgs e)

        {

            if (dataGridViewFornecedores.SelectedRows.Count == 0)

            {

                MessageBox.Show("Selecione um fornecedor para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }



            if (MessageBox.Show("Tem certeza que deseja excluir este fornecedor?", "Confirmação",

                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {

                int id = Convert.ToInt32(dataGridViewFornecedores.SelectedRows[0].Cells["Id"].Value);

                try

                {

                    _fornecedorService.ExcluirFornecedor(id);

                    CarregarFornecedores();

                    MessageBox.Show("Fornecedor excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                catch (Exception ex)

                {

                    MessageBox.Show($"Erro ao excluir fornecedor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }



        private void btnBuscarCNPJ_Click(object sender, EventArgs e)

        {

            try

            {

                var cnpjForm = new BuscarCNPJForm(_serviceFactory);

                if (cnpjForm.ShowDialog() == DialogResult.OK && cnpjForm.DadosFornecedor != null)

                {

                    // Usando o construtor que aceita dynamic

                    var cadastroForm = new CadastroForm(_serviceFactory, cnpjForm.DadosFornecedor);



                    if (cadastroForm.ShowDialog() == DialogResult.OK)

                    {

                        CarregarFornecedores();

                        MessageBox.Show("Fornecedor cadastrado com sucesso!", "Sucesso",

                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show($"Erro ao buscar CNPJ: {ex.Message}", "Erro",

                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }



        private void bntNovo_Click(object sender, EventArgs e)

        {

            var cadastroForm = new CadastroForm(_serviceFactory);

            if (cadastroForm.ShowDialog() == DialogResult.OK)

            {

                CarregarFornecedores();

            }

        }

    }

}
