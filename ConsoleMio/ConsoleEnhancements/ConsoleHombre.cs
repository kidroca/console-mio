namespace ConsoleMio.ConsoleEnhancements
{
    using System;
    using System.Linq;
    using Contracts;

    /// <inheritdoc />
    public class ConsoleHombre : IConsoleHombre
    {
        private readonly IConsoleWriter writer;
        private readonly IConsoleReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleHombre"/> class.
        /// Creates a new instance using the provided writer and reader
        /// </summary>
        /// <param name="writer">A console writer that is used internally</param>
        /// <param name="reader">a console reader that is used internally</param>
        public ConsoleHombre(IConsoleWriter writer, IConsoleReader reader)
        {
            this.writer = writer;
            this.reader = reader;
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

        /// <inheritdoc />
        public T[] ReadInput<T>(
            string message,
            string[] splitPoints,
            Func<string, T> transform,
            ConsoleColor messageColor,
            ConsoleColor inputColor,
            ConsoleColor errorColor)
        {
            T[] args = null;
            int initalTop = Console.CursorTop;
            int rows = 0;

            while (args == null)
            {
                this.ClearRows(initalTop, rows);
                this.writer.Write(message, messageColor);

                try
                {
                    args = this.reader.ReadLine(inputColor)
                        .Split(splitPoints, StringSplitOptions.RemoveEmptyEntries)
                        .Select(transform)
                        .ToArray();
                }
                catch (Exception e)
                {
                    this.writer.WriteLine(e.Message, errorColor);
                    this.PromptToContinue(errorColor);
                }

                rows = Console.CursorTop - initalTop;
            }

            return args;
        }

        /// <inheritdoc />
        public void ClearRows(int top, int count)
        {
            Console.SetCursorPosition(0, top);

            for (int i = 0; i < count; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, top);
        }
    }
}
