using System;
using System.Collections.Generic;

public static class EstadoValidator
{
    // Lista de siglas válidas de estados brasileiros
    private static readonly HashSet<string> _estadosValidos = new HashSet<string>
    {
        "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO",
        "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
        "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
    };

    public static bool IsValid(string uf)
    {
        // Verifica se tem 2 caracteres e é uma sigla válida
        return uf != null &&
               uf.Length == 2 &&
               _estadosValidos.Contains(uf.ToUpper());
    }

    public static string Normalizar(string uf)
    {
        // Converte para maiúsculas e remove espaços
        return uf?.Trim().ToUpper();
    }
}