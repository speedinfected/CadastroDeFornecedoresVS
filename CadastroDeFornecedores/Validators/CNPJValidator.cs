using System.Linq;

public static class CNPJValidator
{
    public static bool IsValid(string cnpj)
    {
        // Remove caracteres não numéricos
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        // Verifica tamanho ou se é sequência repetida
        if (cnpj.Length != 14 || IsSequenciaRepetida(cnpj))
            return false;

        // Calcula e compara os dígitos verificadores
        string cnpjSemDigitos = cnpj.Substring(0, 12);
        string digitosCalculados = CalcularDigitos(cnpjSemDigitos);
        string digitosOriginais = cnpj.Substring(12, 2);

        return digitosCalculados == digitosOriginais;
    }

    public static string Format(string cnpj)
    {
       
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
        
        if (cnpj.Length != 14)
            return cnpj;

        // Aplica a formatação XX.XXX.XXX/XXXX-XX
        return $"{cnpj.Substring(0, 2)}.{cnpj.Substring(2, 3)}.{cnpj.Substring(5, 3)}/{cnpj.Substring(8, 4)}-{cnpj.Substring(12)}";
    }

    private static bool IsSequenciaRepetida(string cnpj)
    {
        char primeiroDigito = cnpj[0];
        return cnpj.All(d => d == primeiroDigito);
    }

    private static string CalcularDigitos(string cnpjParcial)
    {
        int[] pesosPrimeiroDigito = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] pesosSegundoDigito = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        // Calcula primeiro dígito
        int soma = 0;
        for (int i = 0; i < 12; i++)
        {
            soma += int.Parse(cnpjParcial[i].ToString()) * pesosPrimeiroDigito[i];
        }
        int primeiroDigito = (soma % 11) < 2 ? 0 : 11 - (soma % 11);

        // Calcula segundo dígito
        soma = 0;
        for (int i = 0; i < 12; i++)
        {
            soma += int.Parse(cnpjParcial[i].ToString()) * pesosSegundoDigito[i];
        }
        soma += primeiroDigito * pesosSegundoDigito[12];
        int segundoDigito = (soma % 11) < 2 ? 0 : 11 - (soma % 11);

        return primeiroDigito.ToString() + segundoDigito.ToString();
    }
}