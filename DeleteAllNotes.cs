using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping
{
    public class DeleteAllNotes
    {
        public static void DeleteAll()
        {
            // Подключение к базе данных
            using (var context = new LocalDBContext())
            {
                // Удаление всех записей из первой таблицы
                var table1Records = context.Smartphones.ToList();
                context.Smartphones.RemoveRange(table1Records);

                // Удаление всех записей из второй таблицы
                var table2Records = context.Brands.ToList();
                context.Brands.RemoveRange(table2Records);

                // Удаление всех записей из третей таблицы
                var table3Records = context.Headphones.ToList();
                context.Headphones.RemoveRange(table3Records);

                // Сохранение изменений в базе данных
                context.SaveChanges();
            }

            Console.WriteLine("Все записи из таблиц были удалены.");
        }
    }
}
