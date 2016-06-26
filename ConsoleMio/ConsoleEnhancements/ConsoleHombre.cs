namespace ConsoleMio.ConsoleEnhancements
{
    using System;
    using Contracts;

    /// <inheritdoc />
    public class ConsoleHombre : IConsoleHombre
    {
        private readonly IConsoleWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleHombre"/> class.
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
            const char paddingChar = ' ';
            string heading = $" {text} ";

            int totalWidth = Console.WindowWidth;
            int freeWidth = totalWidth - heading.Length;
            if (freeWidth < 0)
            {
                freeWidth = 0;
            }

            string emptyLine = new string(paddingChar, totalWidth);
            string sideSpace = new string(paddingChar, freeWidth / 2);

            var paddingColor = ConsoleColor.White;
            var backgroundColor = ConsoleColor.DarkGray;

            bool isEvenLine = heading.Length % 2 == 0;

            this.writer
                .Write(emptyLine, paddingColor, backgroundColor)
                .Write(sideSpace, paddingColor, backgroundColor)
                .Write(heading, color, backgroundColor)
                .Write(
                    isEvenLine
                        ? sideSpace
                        : sideSpace + paddingChar,
                    paddingColor,
                    backgroundColor)
                .Write(emptyLine, paddingColor, backgroundColor)
                .WriteLine()
                .WriteLine();
        }

        /// <inheritdoc />
        public IConsoleMenu<T> CreateMenu<T>(params T[] items)
        {
            return new ConsoleMenu<T>(this.writer, items);
        }

        /// <inheritdoc />
        public IConsoleMenu<T> CreateMenu<T>(string prefix, params T[] items)
        {
            return new ConsoleMenu<T>(this.writer, items, prefix);
        }

        /// <inheritdoc />
        public IConsoleMenu<T> CreatePromptMenu<T>(string promp, params T[] items)
        {
            return new PromptMenu<T>(this.writer, promp, items);
        }

        /// <inheritdoc />
        public void PromptToContinue(ConsoleColor color)
        {
            this.writer
                .WriteLine("Press a key to proceed", color)
                .WriteLine();
            Console.ReadKey(true);
        }
    }
}
