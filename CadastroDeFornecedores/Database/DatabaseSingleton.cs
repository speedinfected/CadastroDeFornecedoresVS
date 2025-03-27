using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

public sealed class DatabaseSingleton : IDisposable
{
    private static DatabaseSingleton _instance;
    private static readonly object _lock = new object();
    private readonly string _connectionString = "Server=localhost;Database=cadastro_fornecedores;Uid=root;Pwd=;";

    public MySqlConnection CreateConnection()
    {
        try
        {
            Logger.Log("Tentando estabelecer conexão com o banco de dados...", "INFO");
            var connection = new MySqlConnection(_connectionString);
            connection.Open();

            // Teste de conexão funcional
            using (var testCmd = new MySqlCommand("SELECT 1", connection))
            {
                testCmd.ExecuteScalar();
            }

            Logger.Log("Conexão com o banco estabelecida com sucesso", "INFO");
            return connection;
        }
        catch (MySqlException ex)
        {
            Logger.Log($"Falha na conexão com o banco: {ex.Message}", "ERRO");

            string errorMessage;
            switch (ex.Number)
            {
                case 0:
                    errorMessage = "Não foi possível conectar ao banco de dados. Verifique:\n\n" +
                                 "1. O servidor MySQL está rodando?\n" +
                                 "2. As credenciais estão corretas?\n" +
                                 "3. O banco 'cadastro_fornecedores' existe?";
                    break;
                case 1042:
                    errorMessage = "Servidor MySQL não encontrado. Verifique a conexão de rede.";
                    break;
                case 1045:
                    errorMessage = "Acesso negado. Usuário ou senha incorretos.";
                    break;
                default:
                    errorMessage = $"Erro de banco de dados: {ex.Message}";
                    break;
            }

            ShowDatabaseError(errorMessage);
            throw new ApplicationException("Erro de conexão com o banco", ex);
        }
        catch (Exception ex)
        {
            Logger.Log($"Erro inesperado ao conectar ao banco: {ex.Message}", "ERRO");
            ShowDatabaseError("Erro inesperado ao acessar o banco de dados.");
            throw;
        }
    }

    private void ShowDatabaseError(string message)
    {
        if (Application.OpenForms.Count > 0)
        {
            var form = Application.OpenForms[0];
            if (form.InvokeRequired)
            {
                form.BeginInvoke((Action)(() => MessageBox.Show(message, "Erro de Banco de Dados",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
            else
            {
                MessageBox.Show(message, "Erro de Banco de Dados",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static DatabaseSingleton Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseSingleton();
                }
                return _instance;
            }
        }
    }

    public void Dispose()
    {
        // Implementação de limpeza se tiver necessidade
    }
}