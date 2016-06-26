namespace ConsoleMio.ConsoleEnhancements
{
    using System;
    using Contracts;

    /// <summary>
    /// Console helper library for printing colored messages
    /// </summary>
    public class ConsoleMio : IConsoleWriter, IConsoleReader, IConsoleHombre
    {
        private readonly IConsoleWriter writer;
        private readonly IConsoleReader reader;
        private readonly IConsoleHombre hombre;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMio"/> class.
        /// Creates a new ConsoleMio helper with the default implementations of
        /// the <see cref="IConsoleWriter"/>, <see cref="IConsoleReader"/> and
        /// <see cref="IConsoleHombre"/> and invokes the <see cref="Setup"/> method
        /// </summary>
        public ConsoleMio()
        {
            this.writer = new ConsoleWriter();
            this.reader = new ConsoleReader();
            this.hombre = new ConsoleHombre(this.writer, this.reader);

            this.Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMio"/> class.
        /// Creates a new ConsoleMio helper using the provided writer and reader
        /// and invokes the <see cref="Setup"/> method
        /// </summary>
        /// <param name="writer">An <see cref="IConsoleWriter"/> implementation</param>
        /// <param name="reader">An <see cref="IConsoleReader"/> implementation</param>
        /// <param name="hombre">An <see cref="IConsoleHombre"/> implementation</param>
        /// <exception cref="ArgumentNullException">
        /// Throws an exception if some of the provided parameters is null
        /// </exception>
        public ConsoleMio(IConsoleWriter writer, IConsoleReader reader, IConsoleHombre hombre)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (hombre == null)
            {
                throw new ArgumentNullException(nameof(hombre));
            }

            this.reader = reader;
            this.writer = writer;
            this.hombre = hombre;
        }

        /// <summary>
        /// Sets up overall console appearance, ecnoding to unicode and console window width
        /// </summary>
        /// <param name="color">The default text color</param>
        /// <param name="background">The default background color</param>
        /// <param name="windowWidth">The window width</param>
        public void Setup(
            ConsoleColor color = ConsoleColor.Black,
            ConsoleColor background = ConsoleColor.White,
            int windowWidth = 120)
        {
            try
            {
                Console.BackgroundColor = background;
                Console.ForegroundColor = color;
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.SetWindowSize(windowWidth, Console.WindowHeight);
            }
            catch (Exception)
            {
                // Prevent Crashing if Console can't take any of the parameters
            }
            finally
            {
                Console.Clear();
            }
        }

        /// <inheritdoc />
        public IConsoleWriter Write(string text, ConsoleColor color)
        {
            return this.writer.Write(text, color);
        }

        /// <inheritdoc />
        public IConsoleWriter Write(string text, ConsoleColor color, ConsoleColor background)
        {
            return this.writer.Write(text, color, background);
        }

        /// <inheritdoc />
        public IConsoleWriter Write(object item, ConsoleColor color)
        {
            return this.writer.Write(item, color);
        }

        /// <inheritdoc />
        public IConsoleWriter Write(object item, ConsoleColor color, ConsoleColor background)
        {
            return this.writer.Write(item, color, background);
        }

        /// <inheritdoc />
        public IConsoleWriter WriteLine(string text, ConsoleColor color)
        {
            return this.writer.WriteLine(text, color);
        }

        /// <inheritdoc />
        public IConsoleWriter WriteLine(string text, ConsoleColor color, ConsoleColor background)
        {
            return this.writer.WriteLine(text, color, background);
        }

        /// <inheritdoc />
        public IConsoleWriter WriteLine(object item, ConsoleColor color)
        {
            return this.writer.WriteLine(item, color);
        }

        /// <inheritdoc />
        public IConsoleWriter WriteLine(object item, ConsoleColor color, ConsoleColor background)
        {
            return this.writer.WriteLine(item, color, background);
        }

        /// <inheritdoc />
        public IConsoleWriter WriteLine()
        {
            return this.writer.WriteLine();
        }

        /// <inheritdoc />
        public IConsoleWriter Format(string text, ConsoleColor color, params object[] args)
        {
            return this.writer.Format(text, color, args);
        }

        /// <inheritdoc />
        public IConsoleWriter FormatLine(string text, ConsoleColor color, params object[] args)
        {
            return this.writer.FormatLine(text, color, args);
        }

        /// <inheritdoc />
        public string ReadLine(ConsoleColor color)
        {
            return this.reader.ReadLine(color);
        }

        /// <inheritdoc />
        public string ReadLine(ConsoleColor color, ConsoleColor background)
        {
            return this.reader.ReadLine(color, background);
        }

        /// <inheritdoc />
        public void PrintHeading(string text, ConsoleColor color = ConsoleColor.White)
        {
            this.hombre.PrintHeading(text, color);
        }

        /// <inheritdoc />
        public IConsoleMenu<T> CreateMenu<T>(params T[] items)
        {
            return this.hombre.CreateMenu<T>(items);
        }

        /// <inheritdoc />
        public IConsoleMenu<T> CreateMenu<T>(string prefix, params T[] items)
        {
            return this.hombre.CreateMenu<T>(prefix, items);
        }

        /// <inheritdoc />
        public IConsoleMenu<T> CreatePromptMenu<T>(string prompt, params T[] items)
        {
            return this.hombre.CreatePromptMenu(prompt, items);
        }

        /// <inheritdoc />
        public void PromptToContinue(ConsoleColor color)
        {
            this.hombre.PromptToContinue(color);
        }

        public T[] ReadInput<T>(
            string message,
            string[] splitPoints,
            Func<string, T> transform,
            int parametersCount,
            ConsoleColor messageColor,
            ConsoleColor inputColor,
            ConsoleColor errorColor)
        {
            return this.hombre.ReadInput(message, splitPoints, transform, parametersCount, messageColor, inputColor, errorColor);
        }

        /// <inheritdoc />
        public void ClearRows(int top, int count)
        {
            this.hombre.ClearRows(top, count);
        }
    }
}
