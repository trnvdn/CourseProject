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


    public void SaveData()
    {
        File.WriteAllText("storage.json", JsonConvert.SerializeObject(_storage, Formatting.Indented));
    }

    public void AddPerson()
    {
        Person? _person = new();
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
        while (true)
        {
            Console.WriteLine("Дані введені вірно?");
            switch (Validation.VerifyInt(":\n1.Так\n2.Ні\n"))
            {
                case 1:
                    _storage.list.Add(generateID(_person));
                    SaveData();
                    return;
                case 2:
                    AddPerson();
                    return;
                default:
                    Console.WriteLine("Такої відповіді немає");
                    break;
            }
        }
    }

    private Person generateID(Person person)
    {
        Random rnd = new Random();
        person.IdNum = rnd.Next(100000, 250000);
        person.Id = "UA" + "-" + string.Join("", person.Rank.Select(x => x.ToString().ToUpper()).ToArray()) + "-" +
                    person.Surname[0] + person.Name[0] + "-" + person.IdNum;
        return person;
    }
    
    public void Refactsoldier()
    {
        if (isListNotEmpty())
        {
            Console.WriteLine();

            var person = personById(Validation.VerifyInt("останні цифри з  ID-номеру військовослужбовця")).FirstOrDefault();
            if (person != null)
            {
                _storage.list.RemoveAt(_storage.list.IndexOf(person));
                bool InProcess = true;
                while (InProcess)
                {
                    switch (Validation.VerifyInt("поле яке треба змінити\n" + "1.Ім'я\n" +
                                                 "2.Прізвище\n"+
                                                 "3.Вік \n"+
                                                 "4.Звання\n" +
                                                 "5.Дата отримання звання\n" +
                                                 "6.Форма служби\n" +
                                                 "7.Ім'я та прізвище матері\n" +
                                                 "8.Адреса проживання матері\n" +
                                                 "9.Ім'я та прізвище батька\n" +
                                                 "10.Адреса проживання батька\n" +
                                                 "11.Цивільна професія військовослужбовця\n" +
                                                 "12.Заклад освіти\n" +
                                                 "13.Позиція\n" +
                                                 "14.Номер роти\n" +
                                                 "15.Номер батальйону\n" +
                                                 "16.Номер бригади\n" +
                                                 "17.Період служби\n" +
                                                 "18.Характеристика\n" +
                                                 "19.Завершити редагування"))
                    {
                        case 1:
                            person.Name = Validation.VerifyString("ім'я");
                            break;
                        case 2:
                            person.Surname = Validation.VerifyString("прізвище");
                            break;
                        case 3:
                            person.Age = Validation.VerifyInt("вік");
                            break;
                        case 4:
                            person.Rank = Validation.VerifyRank("звання");
                            break;
                        case 5:
                            person.DateRank = Validation.VerifyString(" дату отримання звання");
                            break;
                        case 6:
                            person.FormOfService = Validation.VerifyString("форму служби");
                            break;
                        case 7:
                            person.mNameSurname = Validation.VerifyString("ім'я та прізвище матері");
                            break;
                        case 8:
                            person.mAdress = Validation.VerifyString("адресу проживання матері");
                            break;
                        case 9:
                            person.fNameSurname = Validation.VerifyString("ім'я та прізвище батька");
                            break;
                        case 10:
                            person.fAdress = Validation.VerifyString("адресу проживання батька");
                            break;
                        case 11:
                            person.civilProfession = Validation.VerifyString("цивільну професію військовослужбовця");
                            break;
                        case 12:
                            person.Education = Validation.VerifyString("заклад освіти");
                            break;
                        case 13:
                            person.Position = Validation.VerifyString("позицію");
                            break;
                        case 14:
                            person.Unit = Validation.VerifyInt("номер роти");
                            break;
                        case 15:
                            person.Battalion = Validation.VerifyInt("номер батальйону");
                            break;
                        case 16:
                            person.Brigade = Validation.VerifyInt("назву бригади");
                            break;
                        case 17:
                            person.Period = Validation.VerifyString("період служби");
                            break;
                        case 18:
                            person.AboutSoldier = Validation.VerifyString("характеристику");
                            break;
                        case 19:
                            Console.Clear();
                            InProcess = false;
                            break;
                        default:
                            Console.WriteLine("Невідома команда");
                            break;
                    }
                }
                _storage.list.Add(generateID(person));
                SaveData();
            }
        }
    }

    public void NewReport()
    {
        var choise = Validation.VerifyInt("що треба зберігти\n"+
            "1.Увесь список військовослужбовців\n" +
            "2.Одного військовослужбовця\n" +
            "3.Пошук по критеріям\n" +
            "4.Відсортуваний список по бригадам");
        switch(choise)
        {
            case 1:
                PrintAll(true);
                break;
            case 2:
                GetDetailedInfo(true);
                break;
            case 3:
                SearchBy(true);
                break;
            case 4:
                SortByBrigade(true);
                break;
        };
    }
    
    
    public void GetDetailedInfo(bool save = false)
    {
        
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Шукати військовослужбовця за:\n1.За прізвищем\n2.За ID номером");

        var choise = Validation.VerifyInt();
        List<Person>? persons = choise switch
        {
            1 => personBySurname(Validation.VerifyString("прізвище військовослужбовця")),
            2 => isListNotEmpty() ? personById(Validation.VerifyInt("останні цифри з  ID-номеру військовослужбовця")) : null,
            _ => null,
        };
        if (persons != null)
        {
            foreach (var p in persons)
            {
                string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";
                Console.WriteLine(
                    $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nФорма служби: {p.FormOfService}\nТермін служби: {p.Period}\nДата отримання звання: {p.DateRank}\nID:{p.Id}\nПозиція: {p.Position}\nОсвіта: {p.Education}\n\nХарактеристика: {p.AboutSoldier}\n\nІм'я та прізвище батька - {p.fNameSurname}\nМісце проживання - {p.fAdress}\nІм'я та прізвище матері - {p.mNameSurname}\nМісце проживання - {p.mAdress}\nЦивільна професія - {p.civilProfession}\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade} бригадм");
            }

            if (save)
            {
                saveToFile(persons);
            }
        }
        else
        {
            Console.WriteLine("Такої команди не існує");
        }
    }

    private List<Person> personById(int lineId)
    {
        return _storage.list.Where(x => x.IdNum == lineId).ToList();;
    }

    private List<Person> personBySurname(string surname)
    {
        var person = _storage.list.Where(x => x.Surname == surname.ToUpper()).ToList();
        if (person == null)
        {
            Console.WriteLine("Невірний ID-номер");
            return null;
        }
        return person;
    }

    private bool isListNotEmpty()
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

    public void PrintAll(bool save = false)
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
            if (save)
            {
                saveToFile(_storage.list);
            }
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
            Console.WriteLine();
            isListNotEmpty();
            var p = personById(Validation.VerifyInt("останні цифри з ID-номеру військовослужбовця для його видалення з реєстру:"));
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

    public void SortByBrigade(bool save = false)
    {
        var tempStorage = _storage.list;
        sortedPrint(tempStorage.OrderBy(x => x.Brigade).ThenBy(x => x.Battalion).ThenBy(x => x.Unit).ToList(),save);
    }

    public void SearchBy(bool save = false)
    {
        var tempStorage = _storage.list;
        Console.WriteLine(
            "Шукати:\n1.За бригадою \n2.За бригадою та батальйоном \n3.За бригадою, батальйоном та ротою \n4.Офіцерський склад\n5.Сержантський і старшинський склад\n6.Рsядовий склад");
        int choise = Validation.VerifyInt();
        switch (choise)
        {
            case 1:
                int t_brigade = Validation.VerifyInt("бригаду");
                sortedPrint(tempStorage.Where(x => x.Brigade == t_brigade).ToList(),save);
                break;
            case 2:
                t_brigade = Validation.VerifyInt("бригаду");
                int t_Battalion = Validation.VerifyInt("батальйон");
                sortedPrint(tempStorage.Where(x => x.Brigade == t_brigade).Where(x => x.Battalion == t_Battalion)
                    .ToList(),save);
                break;
            case 3:
                t_brigade = Validation.VerifyInt("бригаду");
                t_Battalion = Validation.VerifyInt("батальйон");
                int t_Unit = Validation.VerifyInt("роту");
                sortedPrint(tempStorage.Where(x => x.Brigade == t_brigade).Where(x => x.Battalion == t_Battalion)
                    .Where(x => x.Unit == t_Unit).ToList(),save);
                break;
            case 4:
                sortedPrint(tempStorage.Where(x => Array.IndexOf(Validation.GetRankArr(), x.Rank) > 10)
                    .ToList(),save);
                break;
            case 5:
                sortedPrint(tempStorage.Where(x =>
                    Array.IndexOf(Validation.GetRankArr(), x.Rank) < 10 &&
                    Array.IndexOf(Validation.GetRankArr(), x.Rank) > 2).ToList(),save);
                break;
            case 6:
                sortedPrint(tempStorage.Where(x => Array.IndexOf(Validation.GetRankArr(), x.Rank) < 3).ToList(),save);
                break;
            default:
                Console.WriteLine("Немає такої команди");
                return;
        }
    }

    private void sortedPrint(List<Person> squad, bool save = false)
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
            if (save)
            {
                saveToFile(squad);
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Ваш список військовослужбовців пустий");
            Console.WriteLine();
        }
    }

    private void saveToFile(List<Person> persons = null, Person person = null)
    {
        if (persons != null && persons.Count == 0)
        {
            return;
        }
        if (person != null && persons == null)
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

        if (persons != null)
        {
            foreach (var p in persons)
            {
                string age = p.Age % 10 == 0 || p.Age % 10 >= 5 ? "років" : "роки";

                if(isFull == true)
                {
                    document += "--------------------------------------------------------------------------------------\n"; 
                    document += $"{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nФорма служби: {p.FormOfService}\n" +
                                $"Термін служби: {p.Period}\nДата отримання звання: {p.DateRank}\nID:{p.Id}\nПозиція: " +
                                $"{p.Position}\nОсвіта: {p.Education}\n\nХарактеристика: {p.AboutSoldier}\n\nІм'я та " +
                                $"прізвище батька - {p.fNameSurname}\nМісце проживання - {p.fAdress}\nІм'я та прізвище матері" +
                                $" - {p.mNameSurname}\nМісце проживання - {p.mAdress}\nЦивільна професія - {p.civilProfession}" +
                                $"\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade} бригади\n\n\n";
                    document += "--------------------------------------------------------------------------------------\n"; 

                }
                else
                {
                    document += $"\n{p.Rank} {p.Name} {p.Surname} - {p.Age} {age}.\nID:{p.Id}\nПозиція: {p.Position}\n\n{p.Unit} рота {p.Battalion} батальйону {p.Brigade} бригади\n\n\n";
                }
            }
        }
        else
        {
            string age = person.Age % 10 == 0 || person.Age % 10 >= 5 ? "років" : "роки";

            if(isFull == true)
            {
                document += "\n--------------------------------------------------------------------------------------/n"; 
                document += $"{person.Rank} {person.Name} {person.Surname} - {person.Age} {age}.\nФорма служби: {person.FormOfService}\n" +
                            $"Термін служби: {person.Period}\nДата отримання звання: {person.DateRank}\nID:{person.Id}\nПозиція: " +
                            $"{person.Position}\nОсвіта: {person.Education}\n\nХарактеристика: {person.AboutSoldier}\n\nІм'я та " +
                            $"прізвище батька - {person.fNameSurname}\nМісце проживання - {person.fAdress}\nІм'я та прізвище матері" +
                            $" - {person.mNameSurname}\nМісце проживання - {person.mAdress}\nЦивільна професія - {person.civilProfession}" +
                            $"\n{person.Unit} рота {person.Battalion} батальйону {person.Brigade} бригади\n\n\n";
                document += "\n--------------------------------------------------------------------------------------\n"; 

            }
            else
            {
                document += $"\n{person.Rank} {person.Name} {person.Surname} - {person.Age} {age}.\nID:{person.Id}\nПозиція: {person.Position}\n\n{person.Unit} рота {person.Battalion} батальйону {person.Brigade} бригади\n\n\n";
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