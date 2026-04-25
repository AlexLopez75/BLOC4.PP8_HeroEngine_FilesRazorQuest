using System.Text.Json;
using HeroEngine.Core.Models;
using System.IO;

namespace HeroEngine.Web.Data
{
    public class JsonManages
    {
        public static List<T> Read<T>(string path)
        {
            if (!File.Exists(path)) return new List<T>();

            string json = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(json)) return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static void Write<T>(string path, List<T> data)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Path isn't valid.", nameof(path));

            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(path, json);
        }

        public static void Append<T>(string path, T obj)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Path isn't valid.", nameof(path));

            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var data = Read<T>(path);

            if (!data.Contains(obj))
            {
                data.Add(obj);
            }

            Write(path, data);
        }

        public static void Append<T>(string path, List<T> objs)
        {

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path no válido", nameof(path));
            }

            if (objs == null || objs.Count == 0)
            {
                throw new ArgumentException("The list of objects to append cannot be null or empty.", nameof(objs));
            }

            var records = Read<T>(path);
            records.AddRange(objs);
            Write(path, records);
        }
    }
}
