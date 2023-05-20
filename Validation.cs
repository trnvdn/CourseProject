using System;
using System.Linq;

namespace CourseWork;

public static class Validation
{
    static Validation()
    {
        _rankArr = new[] {
            "РЕКРУТ", "СОЛДАТ", "СТАРШИЙ СОЛДАТ", "МОЛОДШИЙ СЕРЖАНТ", "СЕРЖАНТ", "СТАРШИЙ СЕРЖАНТ", "ГОЛОВНИЙ СЕРЖАНТ",
            "ШТАБ-СЕРЖАНТ", "МАЙСТЕР СЕРЖАНТ", "СТАРШИЙ МАЙСТЕР СЕРЖАНТ", "ГОЛОВНИЙ МАЙСТЕР СЕРЖАНТ",
            "МОЛОДШИЙ ЛЕЙТИНАНТ", "ЛЕЙТИНАНТ", "СТАРШИЙ ЛЕЙТИНАНТ", "КАПІТАН", "МАЙОР", "ПІДПОЛКОВНИК", "ПОЛКОВНИК",
            "БРИГАДНИЙ ГЕНЕРАЛ", "ГЕНЕРАЛ МАЙОР", "ГЕНЕРАЛ ЛЕЙТИНАНТ", "ГЕНЕРАЛ"
        };
    }
    private static string[] _rankArr;

    public static string VerifyString(string? caption = null)
    {
        if (caption != null)
        {
            Console.WriteLine($"Введіть {caption}");
        }
        string? str = Console.ReadLine();
        while (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
        {
            Console.WriteLine("Поле не може бути пустим!\nСпробуйте ще раз:");
            str = Console.ReadLine();
        }
        return str.ToUpper();
    }
    public static string VerifyRank(string? caption = null)
    {
        if (caption != null)
        {
            Console.WriteLine($"Введіть звання");
        }
        string str = VerifyString();
        while (!_rankArr.Contains(str))
        {
            Console.WriteLine("Невірне звання!");
            str = VerifyString();
        }

        return str;
    }
    public static int VerifyInt(string? caption = null)
    {
        if (caption != null)
        {
            Console.WriteLine($"Введіть {caption}");
        }
        int num;
        while (!int.TryParse(Console.ReadLine(),out num))
        {
            Console.WriteLine("Помилка. Введіть число");
        }
        return num;
    }
    public static string[] GetRankArr() => _rankArr;
}