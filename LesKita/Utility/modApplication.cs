using Polly;
using System.Runtime.CompilerServices;

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

    public static string GetThousandSeparator(double? nominal)
    {
        string result = Convert.ToDecimal((nominal ?? 0).ToString("N2"))
            .ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("en-US"))
            .Replace(",", ".");

        return result;
    }

    public static string GetFormattedDate(DateTimeOffset? value)
    {
        var dateValue = DateTimeOffset.Parse(value?.ToString() ?? DateTimeOffset.Now.ToString());
        var localDate = dateValue.ToLocalTime();
        var formattedDate = localDate.ToString("dd MMM yy - HH:mm");

        var timeSpan = DateTimeOffset.Now - dateValue;
        string agoText;

        if (timeSpan.TotalDays < 1)
        {
            agoText = "Hari ini";
        }
        else if (timeSpan.TotalDays < 2)
        {
            agoText = "Kemarin";
        }
        else if (timeSpan.TotalDays < 7)
        {
            agoText = $"{(int)timeSpan.TotalDays} hari yang lalu";
        }
        else if (timeSpan.TotalDays < 14)
        {
            agoText = "Seminggu yang lalu";
        }
        else if (timeSpan.TotalDays < 30)
        {
            int weeks = (int)(timeSpan.TotalDays / 7);
            agoText = $"{weeks} minggu yang lalu";
        }
        else if (timeSpan.TotalDays < 60)
        {
            agoText = "Sebulan yang lalu";
        }
        else if (timeSpan.TotalDays < 365)
        {
            int months = (int)(timeSpan.TotalDays / 30);
            agoText = $"{months} bulan yang lalu";
        }
        else if (timeSpan.TotalDays < 730)
        {
            agoText = "Setahun yang lalu";
        }
        else
        {
            int years = (int)(timeSpan.TotalDays / 365);
            agoText = $"{years} tahun yang lalu";
        }

        //var valueFieldName = $"{formattedDate} ({agoText})";
        var valueFieldName = $"{formattedDate}";
        return valueFieldName;
    }

    

}
