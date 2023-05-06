using System;
using System.Collections.Specialized;

namespace CourseWork;

internal class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        Solution solution = new Solution();
        Console.CancelKeyPress += OnConsoleClosing;
        
        bool appContext = true;

        
        while (appContext)
        {
            Console.WriteLine(
                "Введіть код операції:\n1. Додати людину\n2. Видалити людину\n3. Вивести список особистого складу\n4. Вивести сортований список особистого складу\n5. Отримати детальну інформацію про військовослужбовця\n6. Пошук за батальйоном \n7. Редагувати дані про військовослужбовця\n8. Вийти");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    solution.AddPerson();
                    break;
                case "2":
                    Console.Clear();
                    solution.DeleteAt();
                    break;
                case "3":
                    Console.Clear();
                    solution.PrintAll();
                    break;
                case "4":
                    Console.Clear();
                    solution.SortBy();
                    break;
                case "5":
                    Console.Clear();
                    solution.GetDetailedInfo();
                    break;
                case "6":
                    
                    break;
                case "7":
                    Console.Clear();
                    solution.Refactsoldier();
                    break;
                case "8":
                    solution.SaveData();
                    appContext = false;
                    break;
                default:
                    Console.WriteLine("Такої команди не було створено");
                    break;
            }
        }
        void OnConsoleClosing(object sender, ConsoleCancelEventArgs e)
        {
            solution.SaveData();
        }
    }
}