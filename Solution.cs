using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
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
        _person.Name = Validation.VerifyString("ім'я");
        _person.Surname = Validation.VerifyString("прізвище");
        _person.Age = Validation.VerifyInt("вік");
        _person.Rank = Validation.VerifyRank("звання");
        _person.DateRank = Validation.VerifyString("дату отримання звання");
        _person.FormOfService = Validation.VerifyString("форму служби");
        _person.mNameSurname = Validation.VerifyString("ім'я та прізвище матері");
        _person.mAdress = Validation.VerifyString("адресу проживання матері");
        _person.fNameSurname = Validation.VerifyString("ім'я та прізвище батька");
        _person.fAdress = Validation.VerifyString("адресу проживання батька");
        _person.civilProfession = Validation.VerifyString("цивільну професію військовослужбовця");
        _person.Education = Validation.VerifyString("заклад освіти");
        _person.Position = Validation.VerifyString("позицію");
        _person.Unit = Validation.VerifyInt("номер роти");
        _person.Battalion = Validation.VerifyInt("номер батальйону");
        _person.Brigade = Validation.VerifyInt("номер бригади");
        _person.Period = Validation.VerifyString("період служби");
        _person.AboutSoldier = Validation.VerifyString("характеристику");
        bool IsInputCorrect = false;
        while (IsInputCorrect == false)
        {
            
            Console.WriteLine("Дані введені вірно?");
            Console.WriteLine("1.Так\n2.Ні\n");
            switch (Validation.VerifyInt())
            {
                case 1:
                    IsInputCorrect = true;
                    break;
                case 2:
                    IsInputCorrect = true;
                    AddPerson();
                    return;
                default:
                    Console.WriteLine("Такої відповіді немає");
                    return;
            }
        }
        Random rnd = new Random();
        _person.IdNum = rnd.Next(100000, 250000);
        _person.Id = "UA" + "-" + string.Join("", _person.Rank.Select(x => x.ToString().ToUpper()).ToArray()) + "-" +
                     _person.Surname[0] + _person.Name[0] + "-" + _person.IdNum;
        _storage.list.Add(_person);
        SaveData();
    }

    public void Refactsoldier()
    {
        if (MayPrintId())
        {
            Console.WriteLine();

            Console.WriteLine("Введіть останні цифри з  ID-номеру військовослужбовця");
            var p = PersonById(Validation.VerifyInt());
            if (p != null)
            {
                _storage.list.RemoveAt(_storage.list.IndexOf(p));
                bool InProcess = true;
                while (InProcess)
                {
                    Console.WriteLine("Введіть поле яке треба змінити");
                    Console.WriteLine("1.Ім'я \n2.Прізвище \n3.Вік \n4.Звання \n5.Дата отримання звання \n6.Форма служби \n7.Ім'я та" 
                                      + " прізвище матері \n8.Адреса проживання матері \n9.Ім'я та прізвище батька \n"+
                                      " 10.Адреса проживання батька \n11.Цивільна професія військовослужбовця\n"+
                                      "12.Заклад освіти \n13.Позиція \n14.Номер роти\n15.Номер батальйону\n"+
                                      "16.Номер бригади\n17.Період служби\n18.Характеристика\n19.Завершити редагування");
                    switch (Validation.VerifyInt())
                    {
                        case 1:
                            p.Name = Validation.VerifyString("ім'я");
                            break;
                        case 2:
                            p.Surname = Validation.VerifyString("прізвище");
                            break;
                        case 3:
                            p.Age = Validation.VerifyInt("вік");
                            break;
                        case 4:
                            p.Rank = Validation.VerifyRank("звання");
                            break;
                        case 5:
                            p.DateRank = Validation.VerifyString(" дату отримання звання");
                            break;
                        case 6:
                            p.FormOfService = Validation.VerifyString("форму служби");
                            break;
                        case 7:
                            p.mNameSurname = Validation.VerifyString("ім'я та прізвище матері");
                            break;
                        case 8:
                            p.mAdress = Validation.VerifyString("адресу проживання матері");
                            break;
                        case 9:
                            p.fNameSurname = Validation.VerifyString("ім'я та прізвище батька");
                            break;
                        case 10:
                            p.fAdress = Validation.VerifyString("адресу проживання батька");
                            break;
                        case 11:
                            p.civilProfession = Validation.VerifyString("цивільну професію військовослужбовця");
                            break;
                        case 12:
                            p.Education = Validation.VerifyString("заклад освіти");
                            break;
                        case 13:
                            p.Position = Validation.VerifyString("позицію");
                            break;
                        case 14:
                            p.Unit = Validation.VerifyInt("номер роти");
                            break;
                        case 15:
                            p.Battalion = Validation.VerifyInt("номер батальйону");
                            break;
                        case 16:
                            p.Brigade = Validation.VerifyInt("назву бригади");
                            break;
                        case 17:
                            p.Period = Validation.VerifyString("період служби");
                            break;
                        case 18:
                            p.AboutSoldier = Validation.VerifyString("характеристику");
                            break;
                        case 19:
                            InProcess = false;
                            break;
                        default:
                            Console.WriteLine("Невідома команда");
                            break;
                    }
                }
                _storage.list.Add(p);
                SaveData();
            }
        }
    }
    
    
    public void GetDetailedInfo()
    {
        if (MayPrintId())
        {
            Console.WriteLine();

            Console.WriteLine("Введіть останні цифри з  ID-номеру військовослужбовця");
            var p = PersonById(Validation.VerifyInt());
            if (p != null)
            {
                string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";
                Console.WriteLine(
                    $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nФорма служби: {p.FormOfService}\nТермін служби: {p.Period}\nДата отримання звання: {p.DateRank}\nID:{p.Id}\nПозиція: {p.Position}\nОсвіта: {p.Education}\n\nХарактеристика: {p.AboutSoldier}\n\nІм'я та прізвище батька - {p.fNameSurname}\nМісце проживання - {p.fAdress}\nІм'я та прізвище матері - {p.mNameSurname}\nМісце проживання - {p.mAdress}\nЦивільна професія - {p.civilProfession}\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade} бригадм");
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

    private bool MayPrintId()
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
                    $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\n\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade} бригади");
                Console.WriteLine("————————————————————————————————————");
                Console.WriteLine();
            }

            Console.WriteLine();
            SaveToFile(_storage.list);
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
            MayPrintId();
            var p = PersonById(Validation.VerifyInt());
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

    public void SortByBrigade()
    {
        var tempStorage = _storage.list;
        SortedPrint(tempStorage.OrderBy(x => x.Brigade).ThenBy(x=>x.Battalion).ThenBy(x=>x.Unit).ToList());
        SaveToFile(_storage.list);
    }

    public void SearchBy()
    {
        var tempStorage = _storage.list;
        Console.WriteLine(
            "Шукати:\n1.За бригадою 2.За бригадою та батальйоном 3.За бригадою, батальйоном та ротою \n4.Офіцерський склад\n5.Сержантський і старшинський склад\n6.Рsядовий склад");
        int choise = Validation.VerifyInt();
        switch (choise)
        {
            case 1:
                int t_brigade = Validation.VerifyInt("бригаду");
                SortedPrint(tempStorage.Where(x => x.Brigade == t_brigade).ToList());
                break;
            case 2:
                t_brigade = Validation.VerifyInt("бригаду");
                int t_Battalion = Validation.VerifyInt("батальйон");
                SortedPrint(tempStorage.Where(x => x.Brigade == t_brigade).Where(x => x.Battalion == t_Battalion).ToList()
                );
                break;
            case 3:
                t_brigade = Validation.VerifyInt("бригаду");
                t_Battalion = Validation.VerifyInt("бригаду");
                int t_Unit = Validation.VerifyInt("роту");
                SortedPrint(tempStorage.Where(x => x.Brigade == t_brigade).Where(x => x.Battalion == t_Battalion)
                        .Where(x => x.Unit == t_Unit).ToList());
                break;
            case 4:
                SortedPrint(tempStorage.Where(x => Array.IndexOf(Validation.GetRankArr(), x.Rank) > 10)
                    .ToList());
                break;
            case 5:
                SortedPrint(tempStorage.Where(x =>
                    Array.IndexOf(Validation.GetRankArr(), x.Rank) < 10 &&
                    Array.IndexOf(Validation.GetRankArr(), x.Rank) > 2).ToList());
                break;
            case 6:
                SortedPrint(tempStorage.Where(x => Array.IndexOf(Validation.GetRankArr(), x.Rank) < 3).ToList());
                break;
            default:
                Console.WriteLine("Немає такої команди");
                return;
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
                    $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\n\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade} бригади");
                Console.WriteLine("————————————————————————————————————");
                Console.WriteLine();
            }

            Console.WriteLine();
            SaveToFile(squad);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Ваш список військовослужбовців пустий");
            Console.WriteLine();
        }
    }

    private void SaveToFile(List<Person> persons)
    {
        bool IsInputCorrect = false;
        while (IsInputCorrect == false)
        {
            
            Console.WriteLine("Чи бажаєте зберігти список у файл?");
            Console.WriteLine("1.Так\n2.Ні\n");
            switch (Validation.VerifyInt())
            {
                case 1:
                    IsInputCorrect = true;
                    break;
                case 2:
                    IsInputCorrect = true;
                    return;
                default:
                    Console.WriteLine("Такої відповіді немає");
                    return;
            }
        }
        if (persons.Count == 0)
        {
            return;
        }
        Console.Clear();
        string document = "";
        bool? isFull = null;
        while (isFull == null)
        {
            Console.WriteLine("1. Коротка відомість\n2. Повна відомість");
            switch (Validation.VerifyInt())
            {
                case 1:
                    isFull = false;
                    break;
                case 2:
                    isFull = true;
                    break;
                default:
                    Console.WriteLine("Такої відповіді немає");
                    break;
            }
        }
        foreach (var p in persons)
        {
            string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";

            if(isFull == true)
            {
                document += "--------------------------------------------------------------------------------------"; 
               document += $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nФорма служби: {p.FormOfService}\n" +
                           $"Термін служби: {p.Period}\nДата отримання звання: {p.DateRank}\nID:{p.Id}\nПозиція: " +
                           $"{p.Position}\nОсвіта: {p.Education}\n\nХарактеристика: {p.AboutSoldier}\n\nІм'я та " +
                           $"прізвище батька - {p.fNameSurname}\nМісце проживання - {p.fAdress}\nІм'я та прізвище матері" +
                           $" - {p.mNameSurname}\nМісце проживання - {p.mAdress}\nЦивільна професія - {p.civilProfession}" +
                           $"\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade} бригади\n\n\n";
               document += "--------------------------------------------------------------------------------------"; 

            }
            else
            {
                document += $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\n\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade} бригади\n\n\n";
            }
        }
        string name = Validation.VerifyString("назву для файла");
        string folderName = "Reports";
        string fileName = $"{name}.txt";
        string path = Path.Combine(folderName, fileName);

        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(folderName);
        }
        File.WriteAllText(path, document);
    }
}