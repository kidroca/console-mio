namespace ConsoleMio.Tests
{
    using System;
    using ConsoleEnhancements;
    using static System.ConsoleColor;

    public class StartUp
    {
        private static readonly ConsoleMio ConsoleMio = new ConsoleMio();

        private static void Main()
        {
            TestHeading();
            TestPromptMenu();
            TestReadInput();
            TestClearRows();
        }

        private static void TestHeading()
        {
            ConsoleMio.PrintHeading("Even heading");

            ConsoleMio.PrintHeading("Odd heading");

            ConsoleMio.PrintHeading("Some really long heading text that will probably span multiple lines. Dobar si pesho. Some really long heading text that will probably span multiple lines.");
        }

        private static void TestPromptMenu()
        {
            var prompt = "Select an item:";
            string[] items = { "Item 1", "Item 2", "Item 3" };

            var menu = ConsoleMio.CreatePromptMenu(prompt, items);

            Console.WriteLine("Some text before menu");
            menu.Show(Red, Blue);
            Console.WriteLine("Some text after");
        }

        private static void TestClearRows()
        {
            ConsoleMio.ClearRows(0, 3);
            Console.WriteLine("clear test");
        }

        private static void TestReadInput()
        {
            var input = ConsoleMio.ReadInput(
                "Reading test input:",
                new[] { " " },
                double.Parse,
                2,
                Red,
                Blue,
                DarkCyan);

            foreach (var number in input)
            {
                ConsoleMio.WriteLine(number, Blue);
            }
        }
    }
}
