using Microsoft.Office.Interop.Word;
using Scraping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Scraping
{
    public class Word_otchoyt
    {
        public static void CreateReport(Smartphone smartphone, Brand brand)
        {
            Application wordApp = new Application();
            object file = @"C:\Users\Кристина\source\repos\Scraping\Word\СМАРТФОН ШАБЛОН.docx";
            Microsoft.Office.Interop.Word.Document wDoc = wordApp.Documents.Add(ref file, false, WdNewDocumentType.wdNewBlankDocument, true);

            for (int i = 1; i <=3; i++)
            {
                Replace("{СМАРТФОН}", smartphone.Name);
                Replace("{БРЕНД}", brand.Name);
            }
            Replace("{ПРОЦЕССОР}", smartphone.Processor);
            Replace("{ОПЕРАТИВНАЯ ПАМЯТЬ}", smartphone.Ram);
            Replace("{ЭКРАН}", smartphone.Screen);
            Replace("{ОСНОВНАЯ КАМЕРА}", smartphone.MainCamera);
            Replace("{ВСТРОЕННАЯ ПАМЯТЬ}", smartphone.InternalMemory);
            Replace("{ЕМКОСТЬ АККУМУЛЯТОРА}", smartphone.BatteryCapacity);

            // Переходим к концу документа
            var range = wDoc.Content;
            range.Collapse(WdCollapseDirection.wdCollapseEnd);

            // Вставляем диаграмму
            range.InlineShapes.AddOLEObject(ClassType: "Excel.Chart.8", FileName: $@"C:\Users\Кристина\source\repos\Scraping\Excel_result\Бренды.xlsx", LinkToFile: false, DisplayAsIcon: false);

            try
            {
                wDoc.SaveAs2(@"C:\Users\Кристина\source\repos\Scraping\Word\SMARTPHONE.docx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            wordApp.Quit(WdSaveOptions.wdPromptToSaveChanges);
            Console.WriteLine("Отчет сформирован.");
            void Replace(string find, string replace)
            {
                Microsoft.Office.Interop.Word.Range range = wDoc.StoryRanges[WdStoryType.wdMainTextStory];
                range.Find.ClearFormatting();
                range.Find.Execute(FindText: find, ReplaceWith: replace);
            }
            //вставить создание круговой диаграммы с брендами
        }
    }
}
