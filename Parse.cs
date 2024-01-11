using HtmlAgilityPack;
using Scraping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping
{
    public class Parse
    {
        public static async Task GetSmartphonesData(int pageNumber)
        {

            var url = $"https://2droida.ru/collection/smartfony?page={pageNumber}";
            var httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(15) };
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var smartphonesHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("product-preview__content")).ToList();

            List<Smartphone> smartphones = new List<Smartphone>();
            using (var dbcon = new LocalDBContext())
            {
                foreach (var smartphoneHtml in smartphonesHtml)
                {
                    var smartphoneLink = "https://2droida.ru" + smartphoneHtml.Descendants("div")
         .FirstOrDefault(node => node.GetAttributeValue("class", "")
         .Equals("product-preview__title")).Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    var smartphonePageHtml = await httpClient.GetStringAsync(smartphoneLink);
                    var smartphonePageDocument = new HtmlDocument();
                    smartphonePageDocument.LoadHtml(smartphonePageHtml);

                    var smartphone = new Smartphone();

                    smartphone.Name = (smartphonePageDocument.DocumentNode.Descendants("h1")
        .FirstOrDefault(node => node.GetAttributeValue("class", "")
        .Equals("product__title heading")).InnerText).Trim();

                    var properties = smartphonePageDocument.DocumentNode.Descendants("div")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("property ")).ToList();


                    foreach (var property in properties)
                    {
                        var propertyName = property.Descendants("div")
                            .FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("property__name")).InnerText.Trim();

                        var propertyContent = property.Descendants("div")
                            .FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("property__content")).InnerText.Trim();

                        switch (propertyName)
                        {
                            case "Экран":
                                smartphone.Screen = propertyContent;
                                break;
                            case "Процессор":
                                smartphone.Processor = propertyContent;
                                break;
                            case "Основная камера":
                                smartphone.MainCamera = propertyContent;
                                break;
                            case "Оперативная память":
                                smartphone.Ram = propertyContent;
                                break;
                            case "Встроенная память":
                                smartphone.InternalMemory = propertyContent;
                                break;
                            case "Емкость аккумулятора":
                                smartphone.BatteryCapacity = propertyContent;
                                break;
                            case "Бренд":
                                Brand brand = new Brand();
                                brand = dbcon.Brands.FirstOrDefault(b => b.Name == propertyContent);
                                if (brand == null)
                                {
                                    brand = new Brand { Name = propertyContent };
                                    dbcon.Brands.Add(brand);
                                    dbcon.SaveChanges();
                                }
                                smartphone.BrandId = brand.IdBrand;
                                break;
                        }
                    }
                    try
                    {
                        dbcon.Smartphones.Add(smartphone);
                        dbcon.SaveChanges();
                        Console.WriteLine(smartphone.Name);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при сохранении смартфона: {ex.Message}");
                    }

                }
            }
            Console.WriteLine($"Страница {pageNumber} успешно сохранена в бд.");
        }
        public static async Task GetHeadphonesData(int pageNumber)
        {

            var url = $"https://2droida.ru/collection/naushniki-i-garnitury?page={pageNumber}";
            var httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(15) };
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var headphonesHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("product-preview__content")).ToList();

            List<Headphone> headphones = new List<Headphone>();
            using (var dbcon = new LocalDBContext())
            {
                foreach (var headphoneHtml in headphonesHtml)
                {
                    var headphoneLink = "https://2droida.ru" + headphoneHtml.Descendants("div")
         .FirstOrDefault(node => node.GetAttributeValue("class", "")
         .Equals("product-preview__title")).Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    var headphonePageHtml = await httpClient.GetStringAsync(headphoneLink);
                    var headphonePageDocument = new HtmlDocument();
                    headphonePageDocument.LoadHtml(headphonePageHtml);

                    var properties = headphonePageDocument.DocumentNode.Descendants("div")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("property ")).ToList();


                    var headphone = new Headphone();

                    headphone.Name = (headphonePageDocument.DocumentNode.Descendants("h1")
        .FirstOrDefault(node => node.GetAttributeValue("class", "")
        .Equals("product__title heading")).InnerText).Trim();

                    foreach (var property in properties)
                    {
                        var propertyName = property.Descendants("div")
                            .FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("property__name")).InnerText.Trim();

                        var propertyContent = property.Descendants("div")
                            .FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("property__content")).InnerText.Trim();

                        switch (propertyName)
                        {
                            case "Гарантия" or "Гарантийный срок":
                                headphone.Warranty = propertyContent;
                                break;
                            case "Модель":
                                headphone.Model = propertyContent;
                                break;
                            case "Версия Bluetooth":
                                headphone.BluetoothVersion = propertyContent;
                                break;
                            case "Цвет":
                                headphone.Color = propertyContent;
                                break;
                            case "Тип устройства":
                                headphone.DeviceType = propertyContent;
                                break;
                            case "Импеданс":
                                headphone.Impedance = propertyContent;
                                break;
                            case "Бренд":
                                Brand brand = new Brand();
                                brand = dbcon.Brands.FirstOrDefault(b => b.Name == propertyContent);
                                if (brand == null)
                                {
                                    brand = new Brand { Name = propertyContent };
                                    dbcon.Brands.Add(brand);
                                    dbcon.SaveChanges();
                                }
                                headphone.BrandId = brand.IdBrand;
                                break;
                        }
                    }
                    if (headphone.BrandId == Guid.Empty)
                    {
                        Brand brand = dbcon.Brands.FirstOrDefault(b => b.Name == "No brand");
                        if (brand == null)
                        {
                            brand = new Brand { Name = "No brand" };
                            dbcon.Brands.Add(brand);
                            dbcon.SaveChanges();
                        }
                        headphone.BrandId = brand.IdBrand;
                    }
                    try
                    {
                        dbcon.Headphones.Add(headphone);
                        dbcon.SaveChanges();
                        Console.WriteLine(headphone.Name);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при сохранении наушников: {ex.Message}");
                    }

                }
            }
        }
    }
}
