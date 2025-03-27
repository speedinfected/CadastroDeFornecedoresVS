using CadastroFornecedores;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;




namespace WindowsFormsApp1

{
    public partial class CadastroForm : Form
    {
        private readonly FornecedorService _fornecedorService;
        private readonly IServiceFactory _serviceFactory;
        private int? _idFornecedor;
        private dynamic _dadosApi;
        public CadastroForm(IServiceFactory serviceFactory)
        {
            InitializeComponent();
            _serviceFactory = serviceFactory;
            _fornecedorService = new FornecedorService(_serviceFactory);
            _dadosApi = null;
            ConfigureToolTips();
            ConfigurarEventosTextChanged();
        }

        public CadastroForm(IServiceFactory serviceFactory, int idFornecedor) : this(serviceFactory)
        {
            _idFornecedor = idFornecedor;
            CarregarDadosFornecedor();
        }

        public CadastroForm(IServiceFactory serviceFactory, dynamic dadosApi) : this(serviceFactory)
        {
            _dadosApi = dadosApi;
            PreencherCamposApi();
        }
        private void ConfigureToolTips()
        {
            toolTip1.SetToolTip(txtResponsavel, "Informe o nome do responsável legal pela empresa");
            try
            {
                if (_dadosApi?.qsa != null)
                {
                    var qsaList = new List<string>();
                    foreach (var qsa in _dadosApi.qsa)
                    {
                        if (qsa?.nome != null && qsa?.qual != null)
                        {
                            qsaList.Add($"{qsa.nome} ({qsa.qual})");
                        }
                    }
                    if (qsaList.Count > 0)
                    {
                        toolTip1.SetToolTip(txtResponsavel, "Quadro Societário:\n" + string.Join("\n", qsaList));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao configurar tooltip: {ex.Message}");
            }
        }


        private void CarregarDadosFornecedor()
        {
            if (!_idFornecedor.HasValue) return;
            try
            {
                var fornecedor = _fornecedorService.GetFornecedorPorId(_idFornecedor.Value);
                if (fornecedor != null)
                {
                    txtRazaoSocial.Text = fornecedor["RazaoSocial"].ToString();
                    txtCNPJ.Text = fornecedor["CNPJ"].ToString();
                    txtLogradouro.Text = fornecedor["Logradouro"].ToString();
                    txtNumero.Text = fornecedor["Numero"].ToString();
                    txtBairro.Text = fornecedor["Bairro"].ToString();
                    txtCidade.Text = fornecedor["Cidade"].ToString();
                    txtEstado.Text = fornecedor["Estado"].ToString();
                    txtCEP.Text = fornecedor["CEP"].ToString();
                    txtTelefone.Text = fornecedor["Telefone"].ToString();
                    txtEmail.Text = fornecedor["Email"].ToString();
                    txtResponsavel.Text = fornecedor["NomeResponsavel"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados do fornecedor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PreencherCamposApi()
        {
            if (_dadosApi == null) return;
            try
            {
                txtRazaoSocial.Text = _dadosApi.nome?.ToString() ?? "";
                txtCNPJ.Text = _dadosApi.cnpj?.ToString() ?? "";
                txtLogradouro.Text = _dadosApi.logradouro?.ToString() ?? "";
                txtNumero.Text = _dadosApi.numero?.ToString() ?? "";
                txtBairro.Text = _dadosApi.bairro?.ToString() ?? "";
                txtCidade.Text = _dadosApi.municipio?.ToString() ?? "";
                txtEstado.Text = _dadosApi.uf?.ToString() ?? "";
                txtCEP.Text = _dadosApi.cep?.ToString() ?? "";
                txtTelefone.Text = _dadosApi.telefone?.ToString() ?? "";
                txtEmail.Text = _dadosApi.email?.ToString() ?? "";

                if (_dadosApi.responsavel != null)
                {
                    txtResponsavel.Text = _dadosApi.responsavel.ToString();
                }
                else if (_dadosApi.qsa != null)
                {
                    foreach (var qsa in _dadosApi.qsa)
                    {
                        if (qsa != null && qsa.qual != null &&
                           (qsa.qual.ToString().Contains("Administrador") ||
                            qsa.qual.ToString().Contains("Sócio")))
                        {
                            txtResponsavel.Text = qsa.nome?.ToString() ?? "Responsável Legal";
                            break;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(txtResponsavel.Text))
                {
                    txtResponsavel.Text = "Responsável Legal";
                    txtResponsavel.BackColor = Color.LightYellow;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao preencher dados da API: {ex.Message}", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try
            {
                Cursor = Cursors.WaitCursor;
                btnSalvar.Enabled = false;

                if (_idFornecedor.HasValue)
                {
                    _fornecedorService.AtualizarFornecedor(
                        _idFornecedor.Value,
                        txtRazaoSocial.Text,
                        txtLogradouro.Text,
                        txtNumero.Text,
                        txtBairro.Text,
                        txtCidade.Text,
                        txtEstado.Text,
                        txtCEP.Text,
                        txtTelefone.Text,
                        txtEmail.Text,
                        txtResponsavel.Text);
                }

                else
                {
                    _fornecedorService.CadastrarFornecedor(
                        txtRazaoSocial.Text,
                        txtCNPJ.Text,
                        txtLogradouro.Text,
                        txtNumero.Text,
                        txtBairro.Text,
                        txtCidade.Text,
                        txtEstado.Text,
                        txtCEP.Text,
                        txtTelefone.Text,
                        txtEmail.Text,
                        txtResponsavel.Text);
                }

                DialogResult = DialogResult.OK;
                Close();
            }

            catch (ApplicationException ex) 
            {
                MessageBox.Show(ex.Message, "CNPJ Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCNPJ.BackColor = Color.LightPink;
                txtCNPJ.Focus();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar fornecedor: {ex.Message}\n\nDetalhes: {ex.InnerException?.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnSalvar.Enabled = true;
            }
        }

        private void ConfigurarEventosTextChanged()
        {
            txtRazaoSocial.TextChanged += CampoTexto_TextChanged;
            txtCNPJ.TextChanged += CampoTexto_TextChanged;
            txtLogradouro.TextChanged += CampoTexto_TextChanged;
            txtNumero.TextChanged += CampoTexto_TextChanged;
            txtBairro.TextChanged += CampoTexto_TextChanged;
            txtCidade.TextChanged += CampoTexto_TextChanged;
            txtEstado.TextChanged += CampoTexto_TextChanged;
            txtCEP.TextChanged += CampoTexto_TextChanged;
            txtTelefone.TextChanged += CampoTexto_TextChanged;
            txtEmail.TextChanged += CampoTexto_TextChanged;
            txtResponsavel.TextChanged += CampoTexto_TextChanged;
        }

        private void CampoTexto_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.BackColor = SystemColors.Window;
            }
        }


        private bool ValidarCampos()
        {
            bool camposValidos = true;

            // Lista de campos obrigatórios e suas mensagens de erro
         var camposObrigatorios = new Dictionary<TextBox, string>();

         if (txtRazaoSocial != null) camposObrigatorios.Add(txtRazaoSocial, "Informe a Razão Social.");
         if (txtCNPJ != null) camposObrigatorios.Add(txtCNPJ, "Informe o CNPJ.");
         if (txtLogradouro != null) camposObrigatorios.Add(txtLogradouro, "Informe o Logradouro.");
         if (txtNumero != null) camposObrigatorios.Add(txtNumero, "Informe o Número.");
         if (txtBairro != null) camposObrigatorios.Add(txtBairro, "Informe o Bairro.");
         if (txtCidade != null) camposObrigatorios.Add(txtCidade, "Informe a Cidade.");
         if (txtEstado != null) camposObrigatorios.Add(txtEstado, "Informe o Estado (UF).");
         if (txtCEP != null) camposObrigatorios.Add(txtCEP, "Informe o CEP.");
         if (txtResponsavel != null) camposObrigatorios.Add(txtResponsavel, "Informe o Nome do Responsável.");


            // Valida campos obrigatórios
            foreach (var campo in camposObrigatorios)
            {
                if (string.IsNullOrWhiteSpace(campo.Key.Text))
                {
                    campo.Key.BackColor = Color.LightPink;
                    {
                        MessageBox.Show(campo.Value, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        campo.Key.Focus();
                        camposValidos = false;
                    }
                }
            }

            if (!camposValidos) return false;

            // Validações específicas
            if (!_idFornecedor.HasValue && !CNPJValidator.IsValid(txtCNPJ.Text))
            {
                txtCNPJ.BackColor = Color.LightPink;
                MessageBox.Show("CNPJ inválido. Deve conter 14 dígitos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCNPJ.Focus();
                return false;
            }

            if (!EstadoValidator.IsValid(txtEstado.Text))
            {
                txtEstado.BackColor = Color.LightPink;
                MessageBox.Show("UF inválida. Use a sigla correta (ex: SP, RJ).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEstado.Focus();
                return false;
            }

            if (!CEPValidator.IsValid(txtCEP.Text))
            {
                txtCEP.BackColor = Color.LightPink;
                MessageBox.Show("CEP inválido. Deve conter 8 dígitos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCEP.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !EmailValidator.IsValid(txtEmail.Text))
            {
                txtEmail.BackColor = Color.LightPink;
                MessageBox.Show("E-mail inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            //ultima 
            if (!_idFornecedor.HasValue)
            {
                string cnpjLimpo = new string(txtCNPJ.Text.Where(char.IsDigit).ToArray());
                if (!CNPJValidator.IsValid(cnpjLimpo))
                {
                    txtCNPJ.BackColor = Color.LightPink;
                    MessageBox.Show("CNPJ inválido. Verifique os dígitos.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCNPJ.Focus();
                    return false;
                }
                // Formata automaticamente se válido
                txtCNPJ.Text = FormatCNPJ(txtCNPJ.Text);
            }
            return true;
        }
        // APAGAR AQUI SE NECESSÁRIO
        private string FormatCNPJ(string cnpj)
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
            if (cnpj.Length != 14) return cnpj;

            return $"{cnpj.Substring(0, 2)}.{cnpj.Substring(2, 3)}.{cnpj.Substring(5, 3)}/{cnpj.Substring(8, 4)}-{cnpj.Substring(12)}";
        }
        //================================================================================================
        public static class CNPJValidator
        {
            public static bool IsValid(string cnpj)
            {
                cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
                return cnpj.Length == 14;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CadastroForm_Load(object sender, EventArgs e)
        {
            try                
            {
                if (_dadosApi == null)
                {
                    if (string.IsNullOrWhiteSpace(txtCNPJ.Text))
                    {
                        txtCNPJ.Text = "";
                        txtCNPJ.ReadOnly = false;
                    }
                    txtResponsavel.BackColor = SystemColors.Window;
                }
                else
                {
                    if (txtResponsavel.Text == "Responsável Legal")
                    {
                        txtResponsavel.BackColor = Color.LightYellow;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro no Form_Load: {ex.Message}");
            }
        }



        public void PreencherCamposManualmente(string razaoSocial, string cnpj, string logradouro,
            string numero, string bairro, string cidade, string estado, string cep,
            string telefone, string email, string responsavel)
        {            
            txtRazaoSocial.Text = razaoSocial;
            txtCNPJ.Text = cnpj;
            txtLogradouro.Text = logradouro;
            txtNumero.Text = numero;
            txtBairro.Text = bairro;
            txtCidade.Text = cidade;
            txtEstado.Text = estado;
            txtCEP.Text = cep;
            txtTelefone.Text = telefone;
            txtEmail.Text = email;
            txtResponsavel.Text = responsavel;
        }
    }
}