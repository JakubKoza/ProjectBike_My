using System;
using System.Globalization;

namespace ProjectBike.Console.UINew.Helpers
{
    public static class ConsoleHelpers
    {
        public static void Pause()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            System.Console.ReadKey(true);
        }
        public static int ReadIndex(string prompt, int count)
        {
            System.Console.Write(prompt);
            var text = System.Console.ReadLine();

            if (!int.TryParse(text, out int number))
                return -1;

            if (number < 1 || number > count)
                return -1;

            return number - 1;
        }
        public static string ReadString(string prompt)
        {
            System.Console.Write(prompt);
            return System.Console.ReadLine() ?? string.Empty;
        }
        public static int ReadInt(string prompt)
        {
            System.Console.Write(prompt);
            var input = System.Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            return 0;
        }
        public static double ReadDouble(string prompt)
        {
            System.Console.Write(prompt);
            var input = System.Console.ReadLine();
            if (input != null)
            {
                input = input.Replace(',', '.');
            }

            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }

            return 0.0;
        }
    }
}