using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CadastroFornecedores
{
    public class FornecedorService
    {
        private readonly IServiceFactory _serviceFactory;

        public FornecedorService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public DataTable ListarFornecedores()
        {
            Logger.Log("Iniciando listagem de fornecedores...");
            try
            {
                using (var connection = DatabaseSingleton.Instance.CreateConnection())
                {
                    string query = "SELECT * FROM fornecedores ORDER BY RazaoSocial";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            Logger.Log($"Listagem concluída. {dataTable.Rows.Count} fornecedores encontrados.");
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"ERRO ao listar fornecedores: {ex.Message}", "ERRO");
                throw;
            }
        }

        public void CadastrarFornecedor(
            string razaoSocial, string cnpj, string logradouro, string numero,
            string bairro, string cidade, string estado, string cep,
            string telefone, string email, string nomeResponsavel)
        {
            Logger.Log($"Iniciando cadastro do fornecedor: {razaoSocial} (CNPJ: {cnpj})");
            try
            {
                using (var connection = DatabaseSingleton.Instance.CreateConnection())
                {
                    string query = @"INSERT INTO fornecedores 
                                (RazaoSocial, CNPJ, Logradouro, Numero, Bairro, Cidade, Estado, CEP, Telefone, Email, NomeResponsavel)
                                VALUES
                                (@RazaoSocial, @CNPJ, @Logradouro, @Numero, @Bairro, @Cidade, @Estado, @CEP, @Telefone, @Email, @NomeResponsavel)";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@RazaoSocial", razaoSocial);
                        cmd.Parameters.AddWithValue("@CNPJ", cnpj);
                        cmd.Parameters.AddWithValue("@Logradouro", logradouro);
                        cmd.Parameters.AddWithValue("@Numero", numero);
                        cmd.Parameters.AddWithValue("@Bairro", bairro);
                        cmd.Parameters.AddWithValue("@Cidade", cidade);
                        cmd.Parameters.AddWithValue("@Estado", estado);
                        cmd.Parameters.AddWithValue("@CEP", cep);
                        cmd.Parameters.AddWithValue("@Telefone", telefone);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@NomeResponsavel", nomeResponsavel);

                        cmd.ExecuteNonQuery();
                        Logger.Log($"Fornecedor {razaoSocial} cadastrado com sucesso!");
                    }
                }
            }

            catch (MySqlException ex) when (ex.Number == 1062) // Código de erro para duplicata
            {
                Logger.Log($"CNPJ duplicado: {cnpj}", "ERRO");
                throw new ApplicationException("Não é possível cadastrar este CNPJ pois já está cadastrado no sistema.");
            }

            catch (Exception ex)
            {
                Logger.Log($"ERRO ao cadastrar fornecedor {razaoSocial}: {ex.Message}", "ERRO");
                throw;
            }
        }

        public void AtualizarFornecedor(
            int id, string razaoSocial, string logradouro, string numero,
            string bairro, string cidade, string estado, string cep,
            string telefone, string email, string nomeResponsavel)
        {
            Logger.Log($"Iniciando atualização do fornecedor ID: {id}...");
            try
            {
                using (var connection = DatabaseSingleton.Instance.CreateConnection())
                {
                    string query = @"UPDATE fornecedores SET
                                RazaoSocial = @RazaoSocial,
                                Logradouro = @Logradouro,
                                Numero = @Numero,
                                Bairro = @Bairro,
                                Cidade = @Cidade,
                                Estado = @Estado,
                                CEP = @CEP,
                                Telefone = @Telefone,
                                Email = @Email,
                                NomeResponsavel = @NomeResponsavel
                                WHERE Id = @Id";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@RazaoSocial", razaoSocial);
                        cmd.Parameters.AddWithValue("@Logradouro", logradouro);
                        cmd.Parameters.AddWithValue("@Numero", numero);
                        cmd.Parameters.AddWithValue("@Bairro", bairro);
                        cmd.Parameters.AddWithValue("@Cidade", cidade);
                        cmd.Parameters.AddWithValue("@Estado", estado);
                        cmd.Parameters.AddWithValue("@CEP", cep);
                        cmd.Parameters.AddWithValue("@Telefone", telefone);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@NomeResponsavel", nomeResponsavel);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        Logger.Log($"Fornecedor ID: {id} atualizado. Linhas afetadas: {rowsAffected}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"ERRO ao atualizar fornecedor ID: {id}: {ex.Message}", "ERRO");
                throw;
            }
        }

        public void ExcluirFornecedor(int id)
        {
            Logger.Log($"Iniciando exclusão do fornecedor ID: {id}...");
            try
            {
                using (var connection = DatabaseSingleton.Instance.CreateConnection())
                {
                    string query = "DELETE FROM fornecedores WHERE Id = @Id";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Logger.Log($"Fornecedor ID: {id} excluído. Linhas afetadas: {rowsAffected}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"ERRO ao excluir fornecedor ID: {id}: {ex.Message}", "ERRO");
                throw;
            }
        }

        public DataRow GetFornecedorPorId(int id)
        {
            Logger.Log($"Buscando fornecedor por ID: {id}...");
            try
            {
                using (var connection = DatabaseSingleton.Instance.CreateConnection())
                {
                    string query = "SELECT * FROM fornecedores WHERE Id = @Id";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            Logger.Log($"Fornecedor ID: {id} {(dataTable.Rows.Count > 0 ? "encontrado" : "não encontrado")}");
                            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"ERRO ao buscar fornecedor ID: {id}: {ex.Message}", "ERRO");
                throw;
            }
        }
    }
}