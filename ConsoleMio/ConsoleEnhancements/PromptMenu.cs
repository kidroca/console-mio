namespace ConsoleMio.ConsoleEnhancements
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    /// <inheritdoc />
    public class PromptMenu<T> : ConsoleMenu<T>
    {
        private readonly string prompt;

        /// <summary>
        /// Initializes a new instance of the <see cref="PromptMenu{T}"/> class.
        /// </summary>
        /// <param name="writer">
        /// An <see cref="IConsoleWriter"/> to be used for menu rendering
        /// </param>
        /// <param name="prompt">The text prompt that will be displayed on top</param>
        public PromptMenu(IConsoleWriter writer, string prompt)
            : this(writer, prompt, new List<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PromptMenu{T}"/> class.
        /// </summary>
        /// <param name="writer">
        /// An <see cref="IConsoleWriter"/> to be used for menu rendering
        /// </param>
        /// <param name="prompt">The text prompt that will be displayed on top</param>
        /// <param name="items">Menu items to choose from</param>
        public PromptMenu(IConsoleWriter writer, string prompt, IList<T> items)
            : base(writer, items)
        {
            this.prompt = prompt;
        }

        /// <inheritdoc />
        public override T Show(ConsoleColor foreground, ConsoleColor background)
        {
            this.Writer.Write(this.prompt, foreground);

            int left = Console.CursorLeft + 1;
            this.Writer
                .WriteLine()
                .WriteLine();

            var selected = base.Show(foreground, background);
            Console.CursorTop -= 2;
            Console.CursorLeft = left;

            this.Writer.WriteLine(selected, background);

            return selected;
        }
    }
}