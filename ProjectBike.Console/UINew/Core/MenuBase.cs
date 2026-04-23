using System;
using System.Collections.Generic;
using ProjectBike.Console.UINew.Helpers;

namespace ProjectBike.Console.UINew.Core;

public abstract class MenuBase
{
    protected abstract string Title { get; }
    protected abstract Dictionary<char, MenuOption> Options { get; }

    public void Run()
    {
        while (true)
        {
            System.Console.Clear();
            System.Console.WriteLine($"=== {Title} ===\n");

            foreach (var kv in Options)
                System.Console.WriteLine($"{kv.Key}) {kv.Value.Description}");

            System.Console.Write("\nWybierz opcję: ");
            var key = System.Console.ReadKey();
            System.Console.WriteLine();

            if (!Options.ContainsKey(key.KeyChar))
            {
                System.Console.WriteLine("Nieznana opcja.");
                ConsoleHelpers.Pause();
                continue;
            }

            var option = Options[key.KeyChar];
            if (option.Action == null)
                return;

            try
            {
                option.Action();
            }
            
            catch (ExitMenuException)
            {
                return;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Błąd: {ex.Message}");
                ConsoleHelpers.Pause();
            }
        }
    }
    public class ExitMenuException : Exception { }
}