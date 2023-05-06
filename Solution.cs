using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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
        _person.Brigade = Validation.VerifyString("назву бригади");
        _person.Period = Validation.VerifyString("період служби");
        _person.AboutSoldier = Validation.VerifyString("характеристику");
        Console.WriteLine("Дані введені вірно?");
        if(Console.ReadLine().ToLower() == "ні")
        {
            AddPerson();
            return;
        }
        Random rnd = new Random();
        _person.IdNum = rnd.Next(100000, 250000);
        _person.Id = "UA" + "-" + string.Join("", _person.Rank.Select(x => x.ToString().ToUpper()).ToArray()) + "-" +
                     _person.Surname[0] + _person.Name[0] + "-" + _person.IdNum;
        _storage.list.Add(_person);
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
                    Console.WriteLine("1.Ім'я \n2.Прізвище \n3.Вік \n4.звання \n5.дату отримання звання \n6.форму служби \n7.ім'я та" 
                                      + " прізвище матері \n8.адресу проживання матері \n9.ім'я та прізвище батька \n"+
                                      " 10.адресу проживання батька \n11.цивільну професію військовослужбовця \n"+
                                      "12.заклад освіти \n13. позиція \n14. номер роти \n15.номер батальйону \n"+
                                      "16.назву бригади \n17.період служби \n18.характеристика\n");
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
                            p.Brigade = Validation.VerifyString("назву бригади");
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
            MayPrintId();
            var p = PersonById(Validation.VerifyInt());
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
        int choise = Validation.VerifyInt();
        switch (choise)
        {
            case 1:
                SortedPrint(tempStorage.OrderBy(x => x.Battalion).ToList());
                break;
            case 2:
                SortedPrint(tempStorage.Where(x => Array.IndexOf(Validation.GetRankArr(), x.Rank) > 10)
                    .ToList());
                break;
            case 3:
                SortedPrint(tempStorage.Where(x =>
                    Array.IndexOf(Validation.GetRankArr(), x.Rank) < 10 &&
                    Array.IndexOf(Validation.GetRankArr(), x.Rank) > 2).ToList());
                break;
            case 4:
                SortedPrint(tempStorage.Where(x => Array.IndexOf(Validation.GetRankArr(), x.Rank) < 3).ToList());
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