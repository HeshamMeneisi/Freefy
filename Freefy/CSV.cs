using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSV
{
    class CSVDecoder
    {
        /// <summary>
        /// Decodes single-line CSV line by line on-demand
        /// </summary>
        /// <typeparam name="T">Type with a T(Dictionary) constructor</typeparam>
        /// <param name="path">CSV file path</param>
        /// <param name="commentStart">A comment start string for ignored column(s)</param>
        /// <returns></returns>
        public static IEnumerable<T> DecodeSLConst<T>(string path, string commentStart = null)
        {
            string line;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            using (var sr = new StreamReader(File.OpenRead(path)))
            {
                line = sr.ReadLine();
                string[] columns = line.Split(',').ToArray();
                while (!sr.EndOfStream)
                {
                    try
                    {
                        line = sr.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length == 0)
                            continue;
                        
                        dict = new Dictionary<string, string>();
                        for (int i = 0; i < columns.Length; i++)
                            if (commentStart == null || dict[columns[i]].StartsWith(commentStart))
                                dict[columns[i]] = values[i];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Invalid CSV file. " + ex.Message);
                    }
                    yield return (T)Activator.CreateInstance(typeof(T), dict);
                }
            }
        }
    }

    class CSVEncoder
    {
        /// <summary>
        /// Encodes the public properties of the given objects to a CSV file asynchornously
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="data">The IEnumerable data</param>
        /// <param name="path">CSV file path</param>
        public static async Task EncodeToFileAsync<T>(IEnumerable<T> data, string path)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            using (var f = File.Create(path))
            using (var sw = new StreamWriter(f))
            {
                StringBuilder sb = new StringBuilder();
                foreach (PropertyInfo property in properties)
                {
                    sb.Append(property.Name);
                    sb.Append(',');
                }
                await sw.WriteLineAsync(sb.ToString().TrimEnd(','));
                float i = 0;
                foreach (var obj in data)
                {
                    sb = new StringBuilder();
                    foreach (PropertyInfo property in properties)
                    {
                        sb.Append("\"");
                        sb.Append(property.GetValue(obj, null).ToString().Replace("\"", "\"\""));
                        sb.Append("\",");
                    }
                    await sw.WriteLineAsync(sb.ToString().TrimEnd(','));
                    i++;
                    if (i % 100 == 0)
                        Debug.WriteLine(i / data.Count());
                }
            }
        }
    }
}

