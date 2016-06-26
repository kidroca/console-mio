namespace ConsoleMio.Tests
{
    using System;
    using ConsoleEnhancements;
    using ConsoleEnhancements.Contracts;
    using static System.ConsoleColor;

    public class StartUp
    {
        private static readonly IConsoleHombre Hombre = new ConsoleHombre(new ConsoleWriter());

        private static void Main()
        {
            TestHeading();
            TestPromptMenu();
            TestClearRows();
        }

        private static void TestHeading()
        {
            Hombre.PrintHeading("Even heading");

            Hombre.PrintHeading("Odd heading");

            Hombre.PrintHeading("Some really long heading text that will probably span multiple lines. Dobar si pesho. Some really long heading text that will probably span multiple lines.");
        }

        private static void TestPromptMenu()
        {
            var prompt = "Select an item:";
            string[] items = { "Item 1", "Item 2", "Item 3" };

            var menu = Hombre.CreatePromptMenu(prompt, items);

            Console.WriteLine("Some text before menu");
            menu.Show(Red, Blue);
            Console.WriteLine("Some text after");
        }

        private static void TestClearRows()
        {
            Hombre.ClearRows(0, 3);
            Console.WriteLine("clear test");
        }
    }
}
