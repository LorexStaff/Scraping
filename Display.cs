using Scraping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping
{
    public static class Display
    {
        public static void DisplayTableRecords(string tableName)
        {
            using (var dbcon = new LocalDBContext())
            {
                if (tableName == "Smartphones")
                {
                    var smartphones = dbcon.Smartphones.ToList();
                    foreach (var smartphone in smartphones)
                    {
                        Console.WriteLine($"Name: {smartphone.Name}");
                        Console.WriteLine($"Screen: {smartphone.Screen}");
                        Console.WriteLine($"Processor: {smartphone.Processor}");
                        Console.WriteLine($"Main Camera: {smartphone.MainCamera}");
                        Console.WriteLine($"RAM: {smartphone.Ram}");
                        Console.WriteLine($"Internal Memory: {smartphone.InternalMemory}");
                        Console.WriteLine($"Battery Capacity: {smartphone.BatteryCapacity}");
                        Guid smartphoneBrandId = smartphone.BrandId;
                        Brand brand = dbcon.Brands.FirstOrDefault(b => b.IdBrand == smartphoneBrandId);
                        Console.WriteLine($"Brand: {brand.Name}");
                    }
                }
                else if (tableName == "Headphones")
                {
                    var headphones = dbcon.Headphones.ToList();
                    foreach (var headphone in headphones)
                    {
                        Console.WriteLine($"Name: {headphone.Name}");
                        Console.WriteLine($"Model: {headphone.Model}");
                        Console.WriteLine($"Warranty: {headphone.Warranty}");
                        Console.WriteLine($"Bluetooth Version: {headphone.BluetoothVersion}");
                        Console.WriteLine($"Color: {headphone.Color}");
                        Console.WriteLine($"Device Type: {headphone.DeviceType}");
                        Console.WriteLine($"Impedance: {headphone.Impedance}");
                        Guid headphoneBrandId = headphone.BrandId;
                        Brand brand = dbcon.Brands.FirstOrDefault(b => b.IdBrand == headphoneBrandId);
                        Console.WriteLine($"Brand: {brand.Name}");
                    }
                }
                else if (tableName == "Brands")
                {
                    var brands = dbcon.Brands.ToList();
                    foreach (var brand in brands)
                    {
                        Console.WriteLine($"Name: {brand.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Таблица с таким названием не найдена.");
                }
            }
        }
    }
}
