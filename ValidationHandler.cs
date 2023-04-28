using System.Reflection.Metadata.Ecma335;

namespace CourseWork;

public static class ValidationHandler
{
    public static string StringValidation()
    {
        string? str = Console.ReadLine();
        while (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
        {
            Console.WriteLine("Поле не може бути пустим!\nСпробуйте ще раз:");
            str = Console.ReadLine();
        }
        return str.ToUpper();
    }
    public static string VerifyRank()
    {
        string str = StringValidation();
        while (!RankArr.Contains(str))
        {
            Console.WriteLine("Невірне звання!");
            str = StringValidation();
        }

        return str;
    }
    public static int IntValidation()
    {
        int num;
        while (!int.TryParse(Console.ReadLine(),out num))
        {
            Console.WriteLine("Помилка. Введіть число");
        }
        return num;
    }
    private static string[] RankArr = 
    {
        "РЕКРУТ", "СОЛДАТ", "СТАРШИЙ СОЛДАТ", "МОЛОДШИЙ СЕРЖАНТ", "СЕРЖАНТ", "СТАРШИЙ СЕРЖАНТ", "ГОЛОВНИЙ СЕРЖАНТ",
        "ШТАБ-СЕРЖАНТ", "МАЙСТЕР СЕРЖАНТ", "СТАРШИЙ МАЙСТЕР СЕРЖАНТ", "ГОЛОВНИЙ МАЙСТЕР СЕРЖАНТ",
        "МОЛОДШИЙ ЛЕЙТИНАНТ", "ЛЕЙТИНАНТ", "СТАРШИЙ ЛЕЙТИНАНТ", "КАПІТАН", "МАЙОР", "ПІДПОЛКОВНИК", "ПОЛКОВНИК",
        "БРИГАДНИЙ ГЕНЕРАЛ", "ГЕНЕРАЛ МАЙОР", "ГЕНЕРАЛ ЛЕЙТИНАНТ", "ГЕНЕРАЛ"
    };

    public static string[] GetRankArr() => RankArr;
}