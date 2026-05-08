using System.Globalization;
using System.Text;
using ScoutLog.Application.DTOs.ScoutReports;
using ScoutLog.Application.Interfaces.Services;

namespace ScoutLog.Application.Services;

public class RuleBasedReportAnalysisService : IReportAnalysisService
{
    public ReportAnalysisDto Analyze(string reportText)
    {
        if (string.IsNullOrWhiteSpace(reportText))
        {
            throw new ArgumentException("Report text is required.", nameof(reportText));
        }

        var normalizedText = Normalize(reportText);
        var strengths = new List<string>();
        var weaknesses = new List<string>();
        var tags = new List<string>();
        var advice = new List<string>();
        var score = 5;

        if (ContainsAny(normalizedText, "hizli", "sprint", "cabuk"))
        {
            AddUnique(strengths, "Hiz ve sprint aksiyonlari");
            AddUnique(tags, "hizli oyuncu");
            score += 1;
        }

        if (ContainsAny(normalizedText, "pas", "asist", "oyun gorusu"))
        {
            AddUnique(strengths, "Pas kalitesi ve oyun gorusu");
            AddUnique(tags, "teknik oyuncu");
            score += 1;
        }

        if (ContainsAny(normalizedText, "sut", "bitiricilik", "gol"))
        {
            AddUnique(strengths, "Hucum ve bitiricilik tehdidi");
            AddUnique(tags, "hucum katkisi");
            score += 1;
        }

        if (ContainsAny(normalizedText, "savunma donusu", "pozisyon hatasi", "markaj"))
        {
            AddUnique(weaknesses, "Savunma farkindaligi");
            AddUnique(tags, "gelisim gerekli");
            AddUnique(advice, "Savunma pozisyon alma ve savunma donusu calismalari onerilir");
            score -= 1;
        }

        if (ContainsAny(normalizedText, "karar verme", "top kaybi", "riskli tercih"))
        {
            AddUnique(weaknesses, "Karar verme");
            AddUnique(tags, "gelisim gerekli");
            AddUnique(advice, "Baski altinda karar verme ve risksiz pas tercihleri calisilmalidir");
            score -= 1;
        }

        if (ContainsAny(normalizedText, "kondisyon", "yoruldu", "fiziksel dusus"))
        {
            AddUnique(weaknesses, "Fiziksel dayaniklilik");
            AddUnique(tags, "fiziksel gelisim");
            AddUnique(advice, "Fiziksel dayaniklilik ve mac sonu tempo koruma calismalari onerilir");
            score -= 1;
        }

        if (strengths.Count == 0)
        {
            strengths.Add("Dengeli performans profili");
        }

        if (weaknesses.Count == 0)
        {
            weaknesses.Add("Belirgin kritik gelisim alani tespit edilmedi");
        }

        if (tags.Count == 0)
        {
            tags.Add("takip edilmeli");
        }

        if (advice.Count == 0)
        {
            advice.Add("Mevcut performans duzenli mac ve antrenman gozlemleriyle takip edilmelidir");
        }

        var suggestedScore = Math.Clamp(score, 1, 10);
        var summary = BuildSummary(suggestedScore, strengths, weaknesses);

        return new ReportAnalysisDto(
            summary,
            strengths,
            weaknesses,
            suggestedScore,
            tags,
            string.Join("; ", advice));
    }

    private static string BuildSummary(
        int suggestedScore,
        IReadOnlyCollection<string> strengths,
        IReadOnlyCollection<string> weaknesses)
    {
        if (suggestedScore >= 8)
        {
            return $"Oyuncu guclu yonleriyle one cikiyor: {string.Join(", ", strengths)}.";
        }

        if (suggestedScore >= 6)
        {
            return $"Oyuncu olumlu sinyaller veriyor ancak {string.Join(", ", weaknesses)} alanlarinda gelisim gerekiyor.";
        }

        return $"Oyuncunun takip edilmesi onerilir; oncelikli gelisim alanlari: {string.Join(", ", weaknesses)}.";
    }

    private static bool ContainsAny(string text, params string[] keywords)
    {
        return keywords.Any(text.Contains);
    }

    private static void AddUnique(ICollection<string> values, string value)
    {
        if (!values.Contains(value))
        {
            values.Add(value);
        }
    }

    private static string Normalize(string value)
    {
        var lower = value.ToLowerInvariant();
        var normalized = lower.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);

        foreach (var character in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(character);
            }
        }

        return builder
            .ToString()
            .Normalize(NormalizationForm.FormC)
            .Replace('ı', 'i')
            .Replace('ğ', 'g')
            .Replace('ü', 'u')
            .Replace('ş', 's')
            .Replace('ö', 'o')
            .Replace('ç', 'c');
    }
}
