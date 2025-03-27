using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace CadastroFornecedores.Services
{
    public class CNPJService : IApiService
    {
        private const int TimeoutSegundos = 30;
        private readonly HttpClient _httpClient;
        public CNPJService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(TimeoutSegundos)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public dynamic ConsultarCNPJ(string cnpj)
        {
            try
            {
                Logger.Log($"Iniciando consulta de CNPJ: {cnpj}", "INFO");

                cnpj = SanitizarCNPJ(cnpj);
                ValidarFormatoCNPJ(cnpj);

                string url = $"https://receitaws.com.br/v1/cnpj/{cnpj}";

                var response = _httpClient.GetStringAsync(url).GetAwaiter().GetResult();
                var result = JsonConvert.DeserializeObject<dynamic>(response);

                if (result.status == "ERROR")
                {
                    throw new Exception($"API retornou erro: {result.message}");
                }

                result = ProcessarResponsavel(result);
                Logger.Log($"Consulta de CNPJ {cnpj} concluída com sucesso", "INFO");

                return result;
            }
            catch (TaskCanceledException)
            {
                Logger.Log($"Timeout na consulta do CNPJ {cnpj} (limite: {TimeoutSegundos}s)", "ERRO");
                throw new Exception("A consulta demorou muito para responder. Tente novamente mais tarde.");
            }
            catch (HttpRequestException ex)
            {
                Logger.Log($"Falha na conexão com a API: {ex.Message}", "ERRO");
                throw new Exception("Serviço de consulta indisponível no momento. Por favor, preencha manualmente.");
            }
            catch (Exception ex)
            {
                Logger.Log($"Erro ao consultar CNPJ: {ex.Message}", "ERRO");
                throw new Exception("Erro ao consultar CNPJ. Verifique o número e tente novamente.");
            }
        }

        private string SanitizarCNPJ(string cnpj)
        {
            return cnpj.Trim().Replace(".", "").Replace("/", "").Replace("-", "");
        }

        private void ValidarFormatoCNPJ(string cnpj)
        {
            if (cnpj.Length != 14 || !long.TryParse(cnpj, out _))
                throw new ArgumentException("CNPJ deve conter exatamente 14 dígitos numéricos");
        }

        private dynamic ProcessarResponsavel(dynamic result)
        {
            if (result.qsa != null && result.qsa.Count > 0)
            {
                foreach (var qsa in result.qsa)
                {
                    if (qsa.qual.ToString().Contains("Administrador") ||
                        qsa.qual.ToString().Contains("Sócio") ||
                        qsa.qual.ToString().Contains("Responsável"))
                    {
                        result.responsavel = qsa.nome;
                        break;
                    }
                }

                if (result.responsavel == null)
                {
                    result.responsavel = result.qsa[0].nome;
                }
            }
            return result;
        }
    }
}