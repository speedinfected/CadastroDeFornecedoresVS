using CadastroFornecedores;
using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace CadastroDeFornecedores
{
    public partial class BuscarCNPJForm : Form
    {
        private readonly IApiService _apiService;
        public dynamic DadosFornecedor { get; private set; }

        public BuscarCNPJForm(IServiceFactory serviceFactory)
        {
            InitializeComponent();
            _apiService = serviceFactory.CreateApiService();
            Logger.Log($"Formulário de busca por CNPJ inicializado", "INFO");
        }

        private void btnBuscarCNPJ_Click(object sender, EventArgs e)
        {
            string cnpj = txtCNPJ.Text.Trim().Replace(".", "").Replace("/", "").Replace("-", "");

            Logger.Log($"Iniciando consulta para CNPJ: {cnpj}", "INFO");

            if (cnpj.Length != 14 || !cnpj.All(char.IsDigit))
            {
                string msg = "CNPJ inválido. Deve conter 14 dígitos.";
                Logger.Log($"{msg} (CNPJ digitado: {txtCNPJ.Text})", "AVISO");
                MessageBox.Show(msg);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnBuscarCNPJ.Enabled = false;

                Logger.Log("Consultando API de CNPJ...", "DEBUG");
                DadosFornecedor = _apiService.ConsultarCNPJ(cnpj);

                if (DadosFornecedor == null || DadosFornecedor.status == "ERROR")
                {
                    string erro = DadosFornecedor?.message?.ToString() ?? "CNPJ não encontrado";
                    Logger.Log($"API retornou erro: {erro}", "ERRO");
                    MessageBox.Show(erro);
                    return;
                }

                Logger.Log($"Dados encontrados para CNPJ: {cnpj} (Razão Social: {DadosFornecedor.nome})", "INFO");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (WebException ex)
            {
                string erro = $"Falha na conexão com a API: {ex.Status}";
                Logger.Log(erro, "ERRO");
                MessageBox.Show(erro);
            }
            catch (Exception ex)
            {
                Logger.Log($"Erro inesperado: {ex.Message}\nStack Trace: {ex.StackTrace}", "ERRO");
                MessageBox.Show("Ocorreu um erro inesperado ao consultar o CNPJ");
            }
            finally
            {
                Cursor = Cursors.Default;
                btnBuscarCNPJ.Enabled = true;
                Logger.Log("Consulta de CNPJ finalizada", "DEBUG");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Logger.Log("Busca por CNPJ cancelada pelo usuário", "INFO");
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}