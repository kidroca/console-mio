namespace HomeworkHelpers.ConsoleEnchantments
{
    using System;
    using Contracts;

    /// <inheritdoc />
    public class ConsoleWriter : IConsoleWriter
    {
        /// <inheritdoc />
        public IConsoleWriter Write(string text, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write(text);

            Console.ForegroundColor = previousColor;

            return this;
        }

        /// <inheritdoc />
        public IConsoleWriter Write(string text, ConsoleColor color, ConsoleColor background)
        {
            var previousBackground = Console.BackgroundColor;
            Console.BackgroundColor = background;

            this.Write(text, color);

            Console.BackgroundColor = previousBackground;

            return this;
        }


        /// <inheritdoc />
        public IConsoleWriter WriteLine(string text, ConsoleColor color)
        {
            this.Write(text, color);
            Console.WriteLine();

            return this;
        }

        /// <inheritdoc />
        public IConsoleWriter WriteLine(string text, ConsoleColor color, ConsoleColor background)
        {
            this.Write(text, color, background);
            Console.WriteLine();

            return this;
        }

        /// <inheritdoc />
        public IConsoleWriter Format(string text, ConsoleColor color, params object[] args)
        {
            return this.Write(string.Format(text, args), color);
        }

        /// <inheritdoc />
        public IConsoleWriter FormatLine(string text, ConsoleColor color, params object[] args)
        {
            return this.WriteLine(string.Format(text, args), color);
        }
    }
}
