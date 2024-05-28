using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Entities;

namespace TaskManagement
{
    internal static class Utilities
    {
        private const string directory = @"D:\.NET exercises\TaskManagement";
        private const string productsSaveFileName = "products2.txt";
        public static void SaveToFile(List<ISaveable> saveables)
        {
            StringBuilder sb = new StringBuilder();
            string path = Path.Combine(directory, productsSaveFileName);

            foreach (var item in saveables)
            {
                sb.Append(item.ConvertToStringForSaving());
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved items successfully");
            Console.ResetColor();
        }
    }

}
