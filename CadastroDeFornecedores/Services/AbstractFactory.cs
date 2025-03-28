using CadastroFornecedores.Services;
using MySql.Data.MySqlClient;
using System;



namespace CadastroFornecedores
{
    public interface IDataService
    {
        void ExecuteCommand(string query);
        object GetData(string query);
    }



    public interface IApiService
    {
        dynamic ConsultarCNPJ(string cnpj);
    }



    public interface IServiceFactory
    {
        IDataService CreateDataService();
        IApiService CreateApiService();
    }


    public class ServiceFactory : IServiceFactory
    {
        public IDataService CreateDataService()
        {
            return new MySqlDataService();
        }

        public IApiService CreateApiService()

        {
            return new CNPJService();
        }
    }



    public class MySqlDataService : IDataService
    {
        public void ExecuteCommand(string query)
        {
            using (var connection = DatabaseSingleton.Instance.CreateConnection())
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public object GetData(string query)
        {
            using (var connection = DatabaseSingleton.Instance.CreateConnection())
            using (var cmd = new MySqlCommand(query, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var dataTable = new System.Data.DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }
    }

    public class ReceitaWSService : IApiService
    {
        public dynamic ConsultarCNPJ(string cnpj)
        {
            using (var client = new System.Net.WebClient())
            {
                try
                {
                    string url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

                    var response = client.DownloadString(url);

                    return Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                }

                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erro ao consultar CNPJ: {ex.Message}");
                    return null;
                }
            }
        }
    }
}