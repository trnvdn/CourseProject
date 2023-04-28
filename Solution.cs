using Newtonsoft.Json;
namespace CourseWork;

public class Solution
{
    private readonly Storage? _storage = File.Exists("storage.json")
        ? JsonConvert.DeserializeObject<Storage>(File.ReadAllText("storage.json"))
        : new Storage();

    private readonly Person? _person = new();

    public void SaveData()
    {
        File.WriteAllText("storage.json", JsonConvert.SerializeObject(_storage, Formatting.Indented));
    }

    public void AddPerson()
    {
        Console.WriteLine("Введіть ім'я:");
        _person.Name = ValidationHandler.StringValidation();

        Console.WriteLine("Введіть прізвише:");
        _person.Surname = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть вік:");
        _person.Age = ValidationHandler.IntValidation();
        Console.WriteLine("Введіть звання:");
        _person.Rank = ValidationHandler.VerifyRank();
        Console.WriteLine("Введіть дату отримання звання:");
        _person.DateRank = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть форму служби");
        _person.FormOfService = ValidationHandler.StringValidation();

        Console.WriteLine("Введіть ім'я та прізвище матері:");
        _person.mNameSurname = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть адресу проживання матері:");
        _person.mAdress = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть ім'я та прізвище батька:");
        _person.fNameSurname = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть адресу проживання батька:");
        _person.fAdress = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть цивільну професію військовослужбовця:");
        _person.civilProfession = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть заклад освіти:");
        _person.Education = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть позицію:");
        _person.Position = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть номер роти");
        _person.Unit = ValidationHandler.IntValidation();
        Console.WriteLine("Введіть номер батальйону:");
        _person.Battalion = ValidationHandler.IntValidation();
        Console.WriteLine("Введіть назву бригади");
        _person.Brigade = ValidationHandler.StringValidation();
        Console.WriteLine("Введіть період служби");
        _person.Period = ValidationHandler.StringValidation();
        Console.WriteLine("Вкажіть характеристику");
        _person.AboutSoldier = ValidationHandler.StringValidation();
        Random rnd = new Random();
        _person.IdNum = rnd.Next(100000, 250000);
        _person.Id = "UA" + "-" + string.Join("", _person.Rank.Select(x => x.ToString().ToUpper()).ToArray()) + "-" +
                     _person.Surname[0] + _person.Name[0] + "-" + _person.IdNum;
        _storage.list.Add(_person);
    }

    public void GetDetailedInfo()
    {
        if (PrintId())
        {
            Console.WriteLine();

            Console.WriteLine("Введіть останні цифри з  ID-номеру військовослужбовця");
            var p = PersonById(ValidationHandler.IntValidation());
            if (p != null)
            {
                string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";
                Console.WriteLine(
                    $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nФорма служби: {p.FormOfService}\nТермін служби: {p.Period}\nДата отримання звання: {p.DateRank}\nID:{p.Id}\nПозиція: {p.Position}\nОсвіта: {p.Education}\n\nХарактеристика: {p.AboutSoldier}\n\nІм'я та прізвище батька - {p.fNameSurname}\nМісце проживання - {p.fAdress}\nІм'я та прізвище матері - {p.mNameSurname}\nМісце проживання - {p.mAdress}\nЦивільна професія - {p.civilProfession}\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade}");
            }
        }
    }

    private Person PersonById(int lineId)
    {
        var person = _storage.list.Where(x => x.IdNum == lineId).ToList();
        if (person.Count > 0)
        {
            return person[0];
        }

        Console.WriteLine("Невірний ID-номер");
        return null;
    }

    private bool PrintId()
    {
        if (_storage.list.Count != 0)
        {
            foreach (var p in _storage.list)
            {
                Console.WriteLine($"{p.Name} {p.Surname} - {p.Id}");
            }

            return true;
        }

        Console.WriteLine("Інформація про військовослужбовців в реєстрі відсутня");
        return false;

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
                Console.WriteLine(
                    $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\n\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade}");
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
            Console.WriteLine("Введіть останні цифри з ID-номеру військовослужбовця для його видалення з реєстру:");
            PrintId();
            var p = PersonById(ValidationHandler.IntValidation());
            if (p != null)
            {
                int position = Array.IndexOf(_storage.list.ToArray(), p);
                _storage.list.RemoveAt(position);
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
        Console.WriteLine(
            "Сортувати:\n1.За батальйоном\n2.Офіцерський склад\n3.Сержантський і старшинський склад\n4.Рядовий склад");
        int choise = ValidationHandler.IntValidation();
        switch (choise)
        {
            case 1:
                SortedPrint(tempStorage.OrderBy(x => x.Battalion).ToList());
                break;
            case 2:
                SortedPrint(tempStorage.Where(x => Array.IndexOf(ValidationHandler.GetRankArr(), x.Rank) > 10)
                    .ToList());
                break;
            case 3:
                SortedPrint(tempStorage.Where(x =>
                    Array.IndexOf(ValidationHandler.GetRankArr(), x.Rank) < 10 &&
                    Array.IndexOf(ValidationHandler.GetRankArr(), x.Rank) > 2).ToList());
                break;
            case 4:
                SortedPrint(tempStorage.Where(x => Array.IndexOf(ValidationHandler.GetRankArr(), x.Rank) < 3).ToList());
                break;
        }
    }

    private void SortedPrint(List<Person> squad)
    {
        if (squad.Count != 0)
        {
            Console.WriteLine();

            foreach (var p in squad)
            {
                string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";
                Console.WriteLine();
                Console.WriteLine("————————————————————————————————————");
                Console.WriteLine(
                    $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\n\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade}");
                Console.WriteLine("————————————————————————————————————");
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Ваш список військовослужбовців пустий");
            Console.WriteLine();
        }
    }
}