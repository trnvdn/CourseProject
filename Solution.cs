using System.Security.Cryptography;
using Newtonsoft.Json;
namespace CourseWork;

public class Solution
{
    private readonly Storage? _storage = File.Exists("storage.json") ? JsonConvert.DeserializeObject<Storage>(File.ReadAllText("storage.json")) : new Storage();
        private readonly Person? _person = new Person();

        private string[] RankArr = new string[]
        {
            "РЕКРУТ", "СОЛДАТ", "СТАРШИЙ СОЛДАТ", "МОЛОДШИЙ СЕРЖАНТ", "СЕРЖАНТ", "СТАРШИЙ СЕРЖАНТ", "ГОЛОВНИЙ СЕРЖАНТ",
            "ШТАБ-СЕРЖАНТ", "МАЙСТЕР СЕРЖАНТ", "СТАРШИЙ МАЙСТЕР СЕРЖАНТ", "ГОЛОВНИЙ МАЙСТЕР СЕРЖАНТ",
            "МОЛОДШИЙ ЛЕЙТИНАНТ", "ЛЕЙТИНАНТ", "СТАРШИЙ ЛЕЙТИНАНТ", "КАПІТАН", "МАЙОР", "ПІДПОЛКОВНИК", "ПОЛКОВНИК",
            "БРИГАДНИЙ ГЕНЕРАЛ", "ГЕНЕРАЛ МАЙОР", "ГЕНЕРАЛ ЛЕЙТИНАНТ", "ГЕНЕРАЛ"
        };
        private void SaveData()
        {
            File.WriteAllText("storage.json", JsonConvert.SerializeObject(_storage, Formatting.Indented));
        }
        public void AddPerson()
        {
            Console.WriteLine("Введіть ім'я:");
            _person.Name = StringValidation();
            
            Console.WriteLine("Введіть прізвише:");
            _person.Surname = StringValidation();
            Console.WriteLine("Введіть вік:");
            _person.Age = IntValidation();
            Console.WriteLine("Введіть звання:");
            _person.Rank = VerifyRank();
            Console.WriteLine("Введіть дату отримання звання:");
            _person.DateRank = StringValidation();
            Console.WriteLine("Введіть форму служби");
            _person.FormOfService = StringValidation();

            Console.WriteLine("Введіть ім'я та прізвище матері:");
            _person.mNameSurname = StringValidation();
            Console.WriteLine("Введіть адресу проживання матері:");
            _person.mAdress = StringValidation();
            Console.WriteLine("Введіть ім'я та прізвище батька:");
            _person.fNameSurname = StringValidation();
            Console.WriteLine("Введіть адресу проживання батька:");
            _person.fAdress = StringValidation();
            Console.WriteLine("Введіть цивільну професію військовослужбовця:");
            _person.civilProfession = StringValidation();
            Console.WriteLine("Введіть заклад освіти:");
            _person.Education = StringValidation();
            Console.WriteLine("Введіть позицію:");
            _person.Position = StringValidation();
            Console.WriteLine("Введіть номер роти");
            _person.Unit = IntValidation();
            Console.WriteLine("Введіть номер батальйону:");
            _person.Battalion = IntValidation();
            Console.WriteLine("Введіть назву бригади");
            _person.Brigade = StringValidation();
            Console.WriteLine("Введіть період служби");
            _person.Period = StringValidation();
            Console.WriteLine("Вкажіть характеристику");
            _person.AboutSoldier = Console.ReadLine();

                Random rnd = new Random();
            _person.Id = "UA" + "-" + string.Join("",_person.Rank.Select(x=>x.ToString().ToUpper()).ToArray())+ "-" + _person.Surname[0] + _person.Name[0] + "-" + rnd.Next(100000,250000);
            _storage.list.Add(_person);
            SaveData();
        }

        public void GetDetailedInfo()
        {
            if (PrintID())
            {
                Console.WriteLine();
                
                Console.WriteLine("Введіть ID-номер військовослужбовця");
                string lineId = StringValidation();
                var p = PersonById(lineId);
                if (p != null)
                {
                    string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";
                    Console.WriteLine($"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nФорма служби: {p.FormOfService}\nТермін служби: {p.Period}\nДата отримання звання: {p.DateRank}\nID:{p.Id}\nПозиція: {p.Position}\nОсвіта: {p.Education}\n\nХарактеристика: {p.AboutSoldier}\n\nІм'я та прізвище батька - {p.fNameSurname}\nМісце проживання - {p.fAdress}\nІм'я та прізвище матері - {p.mNameSurname}\nМісце проживання - {p.mAdress}\nЦивільна професія - {p.civilProfession}\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade}");
                }
            }
        }

        private Person PersonById(string lineId)
        {
            var person = _storage.list.Where(x => x.Id == lineId.ToUpper()).ToList();
            if (person.Count > 0)
            {
                return person[0];
            }
            else
            {
                Console.WriteLine("Невірний ID-номер");
                return null;
            }
        }
        private bool PrintID()
        {
            if (_storage.list.Count != 0)
            {
                foreach (var p in _storage.list)
                {
                    Console.WriteLine($"{p.Name} {p.Surname} - {p.Id}");
                }

                return true;
            }
            else
            {
                Console.WriteLine("Інформація про військовослужбовців в реєстрі відсутня");
                return false;
            }
        }

        public void PrintAll()
        {
            if (_storage.list.Count != 0)
            {
                Console.WriteLine();
                
                foreach (var p in _storage.list)
                {
                    string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";
                    Console.WriteLine();
                    Console.WriteLine("————————————————————————————————————");
                    Console.WriteLine($"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\n\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade}");
                    Console.WriteLine("————————————————————————————————————");
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Зараз немає наявної інформації!");
                Console.WriteLine();
            }
        }
        public void DeleteAt()
        {
            if (_storage.list.Count != 0)
            {
                Console.WriteLine("Введіть ID-номер військовослужбовця для його видалення з реєстру:");
                PrintID();
                string lineId = StringValidation();
                var p = PersonById(lineId);
                if (p != null)
                {
                    int position = Array.IndexOf(_storage.list.ToArray(), p);
                    _storage.list.RemoveAt(position);
                    SaveData();
                    PrintAll();
                }
            }
            else
            {
                Console.WriteLine("Зараз немає наявної інформації!");
            }
        }

        public void SortBy()
        {
            var tempStorage = _storage.list;
            Console.WriteLine("Сортувати:\n1.За батальйоном\n2.Офіцерський склад\n3.Сержантський і старшинський склад\n4.Рядовий склад");
            int choise = IntValidation();
            switch (choise)
            {
                case 1:
                    SortedPrint(tempStorage.OrderBy(x => x.Battalion).ToList());
                    break;
                case 2:
                    SortedPrint(tempStorage.Where(x => Array.IndexOf(RankArr, x.Rank) > 10).ToList());
                    break;
                case 3:
                    SortedPrint(tempStorage.Where(x => Array.IndexOf(RankArr, x.Rank) < 10 && Array.IndexOf(RankArr, x.Rank) > 2).ToList());
                    break;
                case 4:
                    SortedPrint(tempStorage.Where(x => Array.IndexOf(RankArr, x.Rank) < 3).ToList());
                    break;
            }
        }

        void SortedPrint(List<Person> squad)
        {
            if (squad.Count != 0)
            {
                Console.WriteLine();
                
                foreach (var p in squad)
                {
                    string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";
                    Console.WriteLine();
                    Console.WriteLine("————————————————————————————————————");
                    Console.WriteLine($"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\n\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade}");
                    Console.WriteLine("————————————————————————————————————");
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Зараз немає наявної інформації!");
                Console.WriteLine();
            }
        }
        private string VerifyRank()
        {
            string str = StringValidation();
            while (!RankArr.Contains(str))
            {
                Console.WriteLine("Невірне звання!");
                str = StringValidation();
            }

            return str;
        }
        private string StringValidation()
        {
            string? str = Console.ReadLine();
            while (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                Console.WriteLine("Поле не може бути пустим!\nСпробуйте ще раз:");
                str = Console.ReadLine();
            }
            return str.ToUpper();
        }

        private int IntValidation()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(),out num))
            {
                Console.WriteLine("Помилка. Введіть число");
            }
            return num;
        }
        private class Person
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
            public string fNameSurname { get; set; }
            public string mNameSurname { get; set; }
            public string fAdress { get; set; }
            public string mAdress { get; set; }
            public string civilProfession { get; set; }
            public string Education { get; set; }
            public string Rank { get; set; }
            public string DateRank { get; set; }
            public string Position { get; set; }
            public int Unit { get; set; }
            public int Battalion { get; set; }
            public string Brigade { get; set; }
            public string Period { get; set; }
            public string FormOfService { get; set; }
            public string AboutSoldier { get; set; }

        }
        private class Storage
        {
            public List<Person> list = new List<Person>();
        }
}