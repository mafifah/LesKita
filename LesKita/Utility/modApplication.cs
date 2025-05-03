namespace LesKita;

public static class modApplication
{
    public static bool IsBlazorHybrid()
    {
        return OperatingSystem.IsAndroid() || OperatingSystem.IsIOS() || OperatingSystem.IsMacCatalyst();
    }

    public static string GenerateInitials(string name, int panjangKarakter = 2)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return string.Empty;
        }

        var words = name.Split(' ');
        string initials = "";

        foreach (var word in words)
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                initials += word[0];
            }
        }

        return initials.Length > 2 ? initials.ToUpper()[..2] : initials.ToUpper();
    }
}
