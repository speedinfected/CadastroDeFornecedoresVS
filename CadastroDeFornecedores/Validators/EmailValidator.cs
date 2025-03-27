using System.Text.RegularExpressions;

public static class EmailValidator
{
    private static readonly Regex _regex = new Regex(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled
    );

    public static bool IsValid(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && _regex.IsMatch(email);
    }
}