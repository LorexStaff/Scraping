using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping
{
    public static class DeleteOneNote
    {
        public static void DeleteByName(string name)
        {
            using (var dbcon = new LocalDBContext())
            {
                var smartphone = dbcon.Smartphones.FirstOrDefault(s => s.Name == name);
                var headphone = dbcon.Headphones.FirstOrDefault(h => h.Name == name);
                var brand = dbcon.Brands.FirstOrDefault(b => b.Name == name);

                if (smartphone != null)
                {
                    dbcon.Smartphones.Remove(smartphone);
                    dbcon.SaveChanges();
                    Console.WriteLine("Смартфон успешно удален.");
                }
                else if (headphone != null)
                {
                    dbcon.Headphones.Remove(headphone);
                    dbcon.SaveChanges();
                    Console.WriteLine("Наушники успешно удалены.");
                }
                else if (brand != null)
                {
                    dbcon.Brands.Remove(brand);
                    dbcon.SaveChanges();
                    Console.WriteLine("Бренд успешно удален.");
                }
                else
                {
                    Console.WriteLine("Запись с таким названием не найдена.");
                }
            }
        }
    }
}
