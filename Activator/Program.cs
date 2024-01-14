using Activator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Activator
{
    class Program
    {
        public static void Main()
        {
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string folderPath = Path.Combine(projectPath, "Folder");

            Dictionary<string, List<object>> objectLists = new Dictionary<string, List<object>>();

            try
            {
                using (var reader = new StreamReader(Path.Combine(folderPath, "Okecsv.csv")))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        try
                        {
                            ProcessCsvLine(line, objectLists);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing CSV line: {ex.Message}");
                        }
                    }
                }

                PrintObjectLists(objectLists);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }
        }


        private static void ProcessCsvLine(string line, Dictionary<string, List<object>> objectLists)
        {
            var values = line.Split(',');
            string className = values[0].Trim();
            int counter = 1;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string fullClassName = "Activator.Models." + className;
            Type? type = assembly.GetType(fullClassName);

            if (type != null)
            {
                object obj = System.Activator.CreateInstance(type);

                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (counter == values.Length) continue;

                    Type propType = prop.PropertyType;
                    object propValue = Convert.ChangeType(values[counter], propType);
                    prop.SetValue(obj, propValue);

                    counter++;
                    if (!objectLists.ContainsKey(className))
                    {
                        objectLists[className] = new List<object>();
                    }

                    objectLists[className].Add(obj);
                }
            }
        }

        private static void PrintObjectLists(Dictionary<string, List<object>> objectLists)
        {
            foreach (var kvp in objectLists)
            {
                Console.WriteLine($"Class --> {kvp.Key}:");
                Console.WriteLine(kvp.Value.FirstOrDefault()?.ToString());
                Console.WriteLine();
            }
        }
    }
}
