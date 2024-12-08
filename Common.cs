using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkoa2024
{
    public class Common
    {
        public static string InputDirectory => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Input"));

        public static IEnumerable<string> Input(string inputFile)
        {
            var fileName = Path.Combine(InputDirectory, inputFile);

            Console.WriteLine($"Read from ${fileName}");

            if (!File.Exists(fileName))
            {
                using var fs = File.Create(fileName);
            }

            int counter = 0;
            string? line;

            using var file = new StreamReader(fileName);
            while (file != null && (line = file.ReadLine()) != null)
            {
                yield return line;
                counter++;
            }

            Console.WriteLine("There were {0} lines.", counter);
        }

        public static IEnumerable<string> GetLines(string input)
        {
            using StringReader reader = new(input);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
