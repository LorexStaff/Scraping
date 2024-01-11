using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Scraping.Models;

namespace Scraping
{
    public class Excel_grafik
    {
        public static string GetCharacteristicValue(Smartphone smartphone, string characteristic)
        {
            switch (characteristic)
            {
                case "Screen":
                    return smartphone.Screen;
                case "Processor":
                    return smartphone.Processor;
                case "Main Camera":
                    return smartphone.MainCamera;
                case "RAM":
                    return smartphone.Ram;
                case "Internal Memory":
                    return smartphone.InternalMemory;
                case "Battery Capacity":
                    return smartphone.BatteryCapacity;
                default:
                    return null;
            }
        }

        public static void CreatePieChart(Dictionary<string, int> data, string characteristic)
        {
            var excelApp = new Application();
            Workbook workbook = excelApp.Workbooks.Add();
            Worksheet worksheet = workbook.ActiveSheet;

            int row = 2;
            worksheet.Cells[1, 1].Value = characteristic;
            worksheet.Cells[1, 2].Value = "Количество смартфонов";
            foreach (var item in data)
            {
                worksheet.Cells[row, 1].Value = item.Key;
                worksheet.Cells[row, 2].Value = item.Value;
                row++;
            }

            Microsoft.Office.Interop.Excel.Range chartRange = worksheet.Range["A1:B" + (row - 1)];
            ChartObjects chartObjects = (ChartObjects)worksheet.ChartObjects(Type.Missing);
            ChartObject chartObject = chartObjects.Add(160, 10, 300, 300);
            Chart chart = chartObject.Chart;
            chart.SetSourceData(chartRange);
            chart.ChartType = XlChartType.xlPie;

            chart.HasTitle = true;
            chart.ChartTitle.Text = $"Распределение смартфонов по {characteristic}";

            if (characteristic != "Бренды")
            {
                ChartObjects chartObjects2 = (ChartObjects)worksheet.ChartObjects(Type.Missing);
                ChartObject chartObject2 = chartObjects2.Add(470, 10, 600, 500);
                Chart chart2 = chartObject2.Chart;
                chart2.SetSourceData(chartRange);

                // Задаем тип графика - столбчатый
                chart2.ChartType = XlChartType.xlColumnClustered;

                chart2.HasTitle = true;
                chart2.ChartTitle.Text = $"Распределение смартфонов по {characteristic}";
            }
           
            string filePath = $@"C:\Users\Кристина\source\repos\Scraping\Excel_result\{characteristic}.xlsx";
            workbook.SaveAs(filePath);
            workbook.Close();
            excelApp.Quit();
            Console.WriteLine("График построен.");
        }
    }
}
