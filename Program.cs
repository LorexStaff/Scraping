using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Scraping.Models;
using Microsoft.Office.Interop.Excel;
using Microsoft.EntityFrameworkCore;


namespace Scraping
{
    class Program
    {
        static void Main()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Удалить по названию");
                Console.WriteLine("2. Редактировать запись");
                Console.WriteLine("3. Вывести все записи из таблицы");
                Console.WriteLine("4. Добавить записи в базу данных");
                Console.WriteLine("5. Обновить базу данных");
                Console.WriteLine("6. Удалить все данные");
                Console.WriteLine("7. Построить график");
                Console.WriteLine("8. Сформировать отчет");
                Console.WriteLine("9. Завершить");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите название смартфона, наушников или бренда для удаления:");
                        var nameToDelete = Console.ReadLine();
                        DeleteOneNote.DeleteByName(nameToDelete);
                        break;
                    case "2":
                        Console.WriteLine("Введите название смартфона, наушников или бренда для редактирования:");
                        var nameToEdit = Console.ReadLine();
                        Edit.EditRecordByName(nameToEdit);
                        break;
                    case "3":
                        Console.WriteLine("Введите название таблицы для вывода (Smartphones, Headphones или Brands):");
                        var tableToDisplay = Console.ReadLine();
                        Display.DisplayTableRecords(tableToDisplay);
                        break;
                    case "4":
                        for (int i = 1; i <= 5; i++)
                        {
                            try
                            {
                                Parse.GetSmartphonesData(i).Wait();
                            }
                            catch (AggregateException ex)
                            {
                                foreach (var innerEx in ex.InnerExceptions)
                                {
                                    Console.WriteLine(innerEx.Message);
                                }
                            }
                        }
                        for (int i = 1; i <= 4; i++)
                        {
                            try
                            {
                                Parse.GetHeadphonesData(i).Wait();
                            }
                            catch (AggregateException ex)
                            {
                                foreach (var innerEx in ex.InnerExceptions)
                                {
                                    Console.WriteLine(innerEx.Message);
                                }
                            }
                        }
                        return;
                    case "5":
                        DeleteAllNotes.DeleteAll();
                        for (int i = 1; i <= 5; i++)
                        {
                            try
                            {
                                Parse.GetSmartphonesData(i).Wait();
                            }
                            catch (AggregateException ex)
                            {
                                foreach (var innerEx in ex.InnerExceptions)
                                {
                                    Console.WriteLine(innerEx.Message);
                                }
                            }
                        }
                        for (int i = 1; i <= 4; i++)
                        {
                            try
                            {
                                Parse.GetHeadphonesData(i).Wait();
                            }
                            catch (AggregateException ex)
                            {
                                foreach (var innerEx in ex.InnerExceptions)
                                {
                                    Console.WriteLine(innerEx.Message);
                                }
                            }
                        }
                        return;
                    case "6":
                        DeleteAllNotes.DeleteAll();
                        break;
                    case "7":
                        Console.WriteLine("Выберите характеристику смартфона:");
                        string characteristic = Console.ReadLine();
                        if (characteristic != "Screen" && characteristic != "Processor" && characteristic != "Main Camera" && characteristic != "RAM" && characteristic != "Internal Memory" && characteristic != "Battery Capacity")
                        {
                            Console.WriteLine("Некорректный выбор характеристики.");
                            break;
                        }
                        using (var dbcon = new LocalDBContext())
                        {
                            var smartphones = dbcon.Smartphones.ToList();
                            var characteristicValues = new Dictionary<string, int>();

                            foreach (var smartphone in smartphones)
                            {
                                string value = Excel_grafik.GetCharacteristicValue(smartphone, characteristic);
                                if (value != null)
                                {
                                    if (characteristicValues.ContainsKey(value))
                                    {
                                        characteristicValues[value]++;
                                    }
                                    else
                                    {
                                        characteristicValues[value] = 1;
                                    }
                                }
                            }

                            Excel_grafik.CreatePieChart(characteristicValues, characteristic);
                        }
                        break;
                    case "8":
                        using (var dbcon = new LocalDBContext())
                        {
                            var smartphones = dbcon.Smartphones.Include(s => s.Brand).ToList();
                            var brandCounts = new Dictionary<string, int>();

                            foreach (var smartphone in smartphones)
                            {
                                if (brandCounts.ContainsKey(smartphone.Brand.Name))
                                {
                                    brandCounts[smartphone.Brand.Name]++;
                                }
                                else
                                {
                                    brandCounts[smartphone.Brand.Name] = 1;
                                }
                            }

                            Excel_grafik.CreatePieChart(brandCounts, "Бренды");
                        }
                        Console.WriteLine("Введите название смартфона, по которому хотите составить отчет.");
                        string name = Console.ReadLine();
                        using (var dbcon = new LocalDBContext())
                        {
                            var smartphone = dbcon.Smartphones.FirstOrDefault(s => s.Name == name);
                            if (smartphone == null)
                            {
                                Console.WriteLine("Такого смартфона нет. Пожалуйста, попробуйте еще раз.");
                                break;
                            }
                            else
                            {
                                var brand = dbcon.Brands.FirstOrDefault(b => b.IdBrand == smartphone.BrandId);
                                Word_otchoyt.CreateReport(smartphone, brand);
                            }
                        }
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.");
                        break;
                }
            }
        }
    }
    
}