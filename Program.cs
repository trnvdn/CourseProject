using Newtonsoft.Json;

namespace CourseWork;

internal class Program
{
    static void Main(string[] args)
    {
        //Fixing problems with output&input "і ї"
        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        
        
        Solution solution = new Solution();
        bool appContext = true;
        Console.WriteLine("Welcome!");
        while (appContext)
        {
            Console.WriteLine("Введіть код операції:\n1. Додати людину\n2. Видалити людину\n3. Вивести список особистого складу\n4. Вийти");
            switch (Console.ReadLine())
            {
                case "1":
                    solution.AddPerson();
                    break;
                case "2":
                    solution.DeleteAt();
                    break;
                case "3":
                    solution.PrintAll();
                    break;
                case "4":
                    solution.SaveData();
                    appContext = false;
                    break;
                default:
                    Console.WriteLine("Такої команди не було створено");
                    break;
            }
        }
    }

    public class Solution
    {
        private readonly Storage? _storage = File.Exists("storage.json") ? JsonConvert.DeserializeObject<Storage>(File.ReadAllText("storage.json")) : new Storage();
        private readonly Person? _person = new Person();

        public void SaveData()
        {
            File.WriteAllText("storage.json", JsonConvert.SerializeObject(_storage, Formatting.Indented));
        }
        public void AddPerson()
        {
            Console.Clear();
            Console.WriteLine("Введіть ім'я:");
            _person.Name = StringValidation();
            
            Console.WriteLine("Введіть прізвише:");
            _person.Surname = StringValidation();
            Console.WriteLine("Введіть вік:");
            int num;
            //Age validation
            while (!int.TryParse(Console.ReadLine(),out num))
            {
                Console.WriteLine("Помилка!Введіть ціле число!");
            }

            _person.Age = num;
            Console.WriteLine("Введіть позицію:");
            _person.Position = StringValidation();
            Console.WriteLine("Введіть заклад освіти:");
            _person.Education = StringValidation();
            Console.WriteLine("Введіть звання:");
            _person.Rank = StringValidation();
            Random rnd = new Random();
            _person.Id = "UA" + "-" + string.Join("",_person.Rank.Select(x=>x.ToString().ToUpper()).ToArray())+ "-" + _person.Surname[0] + _person.Name[0] + "-" + rnd.Next(100000,250000);
            _storage.list.Add(_person);
            SaveData();

        }
        public void PrintAll()
        {
            Console.Clear();
            if (_storage.list.Count != 0)
            {
                Console.WriteLine();
                
                foreach (var p in _storage.list)
                {
                    string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";
                    Console.WriteLine();
                    Console.WriteLine("————————————————————————————————————");
                    Console.WriteLine($"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\nОсвіта: {p.Education}");
                    Console.WriteLine("————————————————————————————————————");
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Зараз немає наявної інформації!");
            }
        }
        public void DeleteAt()
        {
            if (_storage.list.Count != 0)
            {
                Console.Clear();
                Console.WriteLine("Введіть ID-номер військовослужбовця для його видалення з реєстру:");
                foreach (var p in _storage.list)
                {
                    Console.WriteLine($"{p.Name} {p.Surname} - {p.Id}");
                }
                string lineId = StringValidation();
                bool success = false;
                foreach (var p in _storage.list)
                {
                    if (p.Id == lineId)
                    {
                        int position = Array.IndexOf(_storage.list.ToArray(), p);
                        _storage.list.RemoveAt(position);
                        success = true;
                        SaveData();
                        PrintAll();
                        break;
                    }
                }
                if (!success)
                {
                    Console.WriteLine("Помилка.За цим номером не фіксуєтся військовослужбовців");
                }
            }
            else
            {
                Console.WriteLine("Зараз немає наявної інформації!");
            }
        }

        string StringValidation()
        {
            string? str = Console.ReadLine();
            while (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                Console.WriteLine("Поле не може бути пустим!\nСпробуйте ще раз:");
                str = Console.ReadLine();
            }

            return str;
        }
        private class Person
        {
            public string Id = "";
            public string Name = "";
            public string Surname = "";
            public int Age;
            public string Position = "";
            public string Education = "";
            public string Rank = "";
        }
        private class Storage
        {
            public List<Person> list = new List<Person>();
        }
    }
}