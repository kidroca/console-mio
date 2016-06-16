namespace ConsoleMio.ConsoleEnhancements
{
    using System;
    using Contracts;

    /// <inheritdoc />
    public class ConsoleReader : IConsoleReader
    {
        /// <inheritdoc />
        public string ReadLine(ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            string input = Console.ReadLine();

            Console.ForegroundColor = previousColor;

            return input;
        }

        /// <inheritdoc />
        public string ReadLine(ConsoleColor color, ConsoleColor background)
        {
            var previousBackground = Console.BackgroundColor;
            Console.BackgroundColor = background;

            string input = this.ReadLine(color);

            Console.BackgroundColor = previousBackground;

            return input;
        }
    }
}
