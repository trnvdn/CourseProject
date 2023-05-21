using System;
using System.Collections.Specialized;

namespace SOTNYK;

internal class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        Solution solution = new Solution();

        bool appContext = true;

        
        while (appContext)
        {
            switch (Validation.VerifyInt(
                        "код операції:\n" +
                        "1. Додати людину\n" +
                        "2. Видалити людину\n" +
                        "3. Вивести список особистого складу\n" + 
                        "4. Вивести відсортований список\n" +
                        "5. Отримати детальну інформацію про військовослужбовця\n" +
                        "6. Пошук за критеріями\n" +
                        "7. Редагувати дані про військовослужбовця\n" +
                        "8. Створити та зберігти звіт\n" +
                        "9. Вийти\n\n"))
            {
                case 1:
                    Console.Clear();
                    solution.AddPerson();
                    break;
                case 2:
                    Console.Clear();
                    solution.DeleteAt();
                    break;
                case 3:
                    Console.Clear();
                    solution.PrintAll();
                    break;
                case 4:
                    Console.Clear();
                    solution.SortByBrigade();
                    break;
                case 5:
                    Console.Clear();
                    solution.GetDetailedInfo();
                    break;
                case 6:
                    Console.Clear();
                    solution.SearchBy();
                    break;
                case 7:
                    Console.Clear();
                    solution.Refactsoldier();
                    break;
                case 8:
                    Console.Clear();
                    solution.NewReport();
                    break;
                case 9:
                    solution.SaveData();
                    appContext = false;
                    break;
                default:
                    Console.WriteLine("Такої команди не було створено");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}