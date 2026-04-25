using System;
using System.Collections.Generic;
using System.IO;

namespace HeroEngine.Core.Data
{
    public static class TXTManager
    {
        public static void Write<T>(string path, T obj)
        {
            File.WriteAllText(path, obj?.ToString() ?? string.Empty);
        }

        public static List<T> Read<T>(string path, Func<string, T> parser)
        {
            List<T> list = new List<T>();

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path no valid");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string cleanLine = line.Trim();

                    try
                    {
                        T obj = parser(cleanLine);
                        list.Add(obj);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Error parsing line: {cleanLine}");
                    }
                }
            }

            return list;
        }
        public static void Apppend<T>(string filePath, T obj)
        {
            string content = obj?.ToString() ?? string.Empty;

            string directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                File.AppendAllText(filePath, Environment.NewLine + content);
            }
            else
            {
                File.AppendAllText(filePath, content);
            }
        }
    }
}
