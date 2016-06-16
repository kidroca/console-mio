namespace ConsoleMio.Tests
{
    using ConsoleEnhancements;
    using ConsoleEnhancements.Contracts;

    public class StartUp
    {
        private static readonly IConsoleHombre Hombre = new ConsoleHombre(new ConsoleWriter());

        private static void Main()
        {
            TestHeading();
        }

        private static void TestHeading()
        {
            Hombre.PrintHeading("Even heading");

            Hombre.PrintHeading("Odd heading");

            Hombre.PrintHeading("Some really long heading text that will probably span multiple lines. Dobar si pesho. Some really long heading text that will probably span multiple lines.");
        }
    }
}
