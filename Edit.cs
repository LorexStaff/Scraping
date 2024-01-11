using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping
{
    public class Edit
    {
        public static void EditRecordByName(string name)
        {
            using (var dbcon = new LocalDBContext())
            {
                var smartphone = dbcon.Smartphones.FirstOrDefault(s => s.Name == name);
                var brand = dbcon.Brands.FirstOrDefault(b => b.Name == name);
                var headphone = dbcon.Headphones.FirstOrDefault(h => h.Name == name);

                if (smartphone != null)
                {
                    // Предоставляем пользователю возможность выбрать, какую характеристику он хочет отредактировать
                    Console.WriteLine("Выберите характеристику для редактирования:");
                    Console.WriteLine("1. Название смартфона");
                    Console.WriteLine("2. Экран");
                    Console.WriteLine("3. Процессор");
                    Console.WriteLine("4. Основная камера");
                    Console.WriteLine("5. Оперативная память");
                    Console.WriteLine("6. Встроенная память");
                    Console.WriteLine("7. Емкость аккумулятора");
                    Console.WriteLine("8. Бренд");

                    int choice = int.Parse(Console.ReadLine());
                    string newValue;

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Введите новое название смартфона:");
                            newValue = Console.ReadLine();
                            smartphone.Name = newValue;
                            break;
                        case 2:
                            Console.WriteLine("Введите новые параметры экрана:");
                            newValue = Console.ReadLine();
                            smartphone.Screen = newValue;
                            break;
                        case 3:
                            Console.WriteLine("Введите новые параметры процессора:");
                            newValue = Console.ReadLine();
                            smartphone.Processor = newValue;
                            break;
                        case 4:
                            Console.WriteLine("Введите новые параметры основной камеры:");
                            newValue = Console.ReadLine();
                            smartphone.MainCamera = newValue;
                            break;
                        case 5:
                            Console.WriteLine("Введите новые параметры оперативной памяти:");
                            newValue = Console.ReadLine();
                            smartphone.Ram = newValue;
                            break;
                        case 6:
                            Console.WriteLine("Введите новые параметры встроенной памяти:");
                            newValue = Console.ReadLine();
                            smartphone.InternalMemory = newValue;
                            break;
                        case 7:
                            Console.WriteLine("Введите новые параметры емкости аккумулятора:");
                            newValue = Console.ReadLine();
                            smartphone.BatteryCapacity = newValue;
                            break;
                        case 8:
                            Console.WriteLine("Введите новое название бренда:");
                            string brandName = Console.ReadLine();

                            // Ищем бренд в базе данных
                            brand = dbcon.Brands.FirstOrDefault(b => b.Name == brandName);

                            if (brand != null)
                            {
                                // Если бренд найден, присваиваем его IdBrand свойству BrandId смартфона
                                smartphone.BrandId = brand.IdBrand;
                            }
                            else
                            {
                                // Если бренд не найден, выводим сообщение об ошибке
                                Console.WriteLine("Такого бренда нет.");
                            }
                            break;
                        default:
                            Console.WriteLine("Некорректный выбор.");
                            break;
                    }

                    dbcon.SaveChanges();
                    Console.WriteLine("Смартфон успешно отредактирован.");
                }
                else if (headphone != null)
                {
                    // Предоставляем пользователю возможность выбрать, какую характеристику он хочет отредактировать
                    Console.WriteLine("Выберите характеристику для редактирования:");
                    Console.WriteLine("1. Название наушников");
                    Console.WriteLine("2. Модель");
                    Console.WriteLine("3. Гарантия");
                    Console.WriteLine("4. Версия Bluetooth");
                    Console.WriteLine("5. Цвет");
                    Console.WriteLine("6. Тип устройства");
                    Console.WriteLine("7. Импеданс");
                    Console.WriteLine("8. Бренд");
                    int choice = int.Parse(Console.ReadLine());
                    string newValue;
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Введите новое название наушников:");
                            newValue = Console.ReadLine();
                            headphone.Name = newValue;
                            break;
                        case 2:
                            Console.WriteLine("Введите новую модель:");
                            newValue = Console.ReadLine();
                            headphone.Model = newValue;
                            break;
                        case 3:
                            Console.WriteLine("Введите новую гарантию:");
                            newValue = Console.ReadLine();
                            headphone.Warranty = newValue;
                            break;
                        case 4:
                            Console.WriteLine("Введите новую версию Bluetooth:");
                            newValue = Console.ReadLine();
                            headphone.BluetoothVersion = newValue;
                            break;
                        case 5:
                            Console.WriteLine("Введите новый цвет:");
                            newValue = Console.ReadLine();
                            headphone.Color = newValue;
                            break;
                        case 6:
                            Console.WriteLine("Введите новый тип устройства:");
                            newValue = Console.ReadLine();
                            headphone.DeviceType = newValue;
                            break;
                        case 7:
                            Console.WriteLine("Введите новый импеданс:");
                            newValue = Console.ReadLine();
                            headphone.Impedance = newValue;
                            break;
                        case 8:
                            Console.WriteLine("Введите новое название бренда:");
                            string brandName = Console.ReadLine();

                            // Ищем бренд в базе данных
                            brand = dbcon.Brands.FirstOrDefault(b => b.Name == brandName);
                            if (brand != null)
                            {
                                headphone.BrandId = brand.IdBrand;
                            }
                            else
                            {
                                Console.WriteLine("Такого бренда нет.");
                            }
                            break;
                        default:
                            Console.WriteLine("Некорректный выбор.");
                            break;
                    }
                    dbcon.SaveChanges();
                    Console.WriteLine("Наушники успешно отредактированы.");
                }
                else if (brand != null)
                {
                    Console.WriteLine("Введите новое название бренда:");
                    var newName = Console.ReadLine();
                    brand.Name = newName;
                    dbcon.SaveChanges();
                    Console.WriteLine("Бренд успешно отредактирован.");
                }
                else
                {
                    Console.WriteLine("Смартфон, наушники или бренд с таким названием не найдены.");
                }
            }
        }
    }
}
