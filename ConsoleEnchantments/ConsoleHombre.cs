namespace HomeworkHelpers.ConsoleEnchantments
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    /// <inheritdoc />
    public class ConsoleHombre : IConsoleHombre
    {
        private readonly IConsoleWriter writer;

        /// <summary>
        /// Creates a new instance using the provided writer
        /// </summary>
        /// <param name="writer">A console writer used internally</param>
        public ConsoleHombre(IConsoleWriter writer)
        {
            this.writer = writer;
        }

        /// <inheritdoc />
        public void PrintHeading(string text, ConsoleColor color = ConsoleColor.White)
        {
            int totalWidth = Console.WindowWidth;
            string format = string.Format(" {0} ", text);
            char paddingChar = ' ';
            var paddingColor = ConsoleColor.White;
            var backgorundColor = ConsoleColor.DarkGray;
            int freeWidth = totalWidth - format.Length;

            this.writer.Write(new string(paddingChar, totalWidth), paddingColor, backgorundColor);
            this.writer.Write(new string(paddingChar, freeWidth / 2), paddingColor, backgorundColor);
            this.writer.Write(format, color, backgorundColor);
            this.writer.Write(new string(paddingChar, freeWidth / 2), paddingColor, backgorundColor);
            this.writer.Write(new string(paddingChar, totalWidth), paddingColor, backgorundColor);
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <inheritdoc />
        public ConsoleMenu<T> CreateMenu<T>(IList<T> items)
        {
            return new ConsoleMenu<T>(this.writer, items);
        }

        /// <inheritdoc />
        public ConsoleMenu<T> CreateMenu<T>(IList<T> items, string prefix)
        {
            return new ConsoleMenu<T>(this.writer, items, prefix);
        }
    }
}
