using System.Security.Cryptography.X509Certificates;

namespace CourseWork;

internal class Program
{
    static void Main(string[] args)
    {
        
        //Fixing problems with output&input "і ї"
        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        Solution solution = new Solution();
        Console.CancelKeyPress += new ConsoleCancelEventHandler(OnConsoleClosing);

        bool appContext = true;

        
        while (appContext)
        {
            Console.WriteLine(
                "Введіть код операції:\n1. Додати людину\n2. Видалити людину\n3. Вивести список особистого складу\n4. Вивести сортований список особистого складу\n5. Отримати детальну інформацію про військовослужбовця\n6. Вийти");
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