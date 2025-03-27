using System.Linq;
using System;

public static class CEPValidator
{
    public static bool IsValid(string cep)
    {
        // Remove caracteres não numéricos
        cep = new string(cep.Where(char.IsDigit).ToArray());

        // Verifica se tem 8 dígitos
        return cep.Length == 8;
    }

    public static string Format(string cep)
    {
        // Formata como 00000-000
        return Convert.ToUInt64(cep).ToString(@"00000\-000");
    }
}