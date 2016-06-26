namespace ConsoleMio.ConsoleEnhancements
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    /// <inheritdoc />
    public class ConsoleMenu<T> : IConsoleMenu<T>
    {
        private readonly IList<T> items;

        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMenu{T}"/> class.
        /// Creates an empty menu
        /// </summary>
        /// <param name="writer">
        /// An <see cref="IConsoleWriter"/> to be used for menu rendering
        /// </param>
        public ConsoleMenu(IConsoleWriter writer)
        {
            this.Writer = writer;
            this.items = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMenu{T}"/> class.
        /// Creates a menu from a list of options
        /// </summary>
        /// <param name="writer">
        /// An <see cref="IConsoleWriter"/> to be used for menu rendering
        /// </param>
        /// <param name="items">Menu items to choose from</param>
        public ConsoleMenu(IConsoleWriter writer, IList<T> items)
        {
            this.Writer = writer;
            this.items = items;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMenu{T}"/> class.
        /// Creates a menu from a list of options displaying the options with the
        /// specified <paramref name="prefix"/>
        /// </summary>
        /// <param name="writer">
        /// An <see cref="IConsoleWriter"/> to be used for menu rendering
        /// </param>
        /// <param name="items">Menu items to choose from</param>
        /// <param name="prefix">Adds a prefix to the displayed menu items</param>
        public ConsoleMenu(IConsoleWriter writer, IList<T> items, string prefix)
            : this(writer, items)
        {
            this.prefix = prefix;
        }

        /// <summary>
        /// Gets the items of the menu
        /// </summary>
        /// <value>
        /// The items of the menu
        /// </value>
        public IList<T> MenuItems => this.items;

        /// <summary>
        /// Gets the <see cref="IConsoleWriter"/>
        /// </summary>
        /// <value>
        /// The <see cref="IConsoleWriter"/>
        /// </value>
        protected IConsoleWriter Writer { get; }

        /// <summary>
        /// O(1)
        /// <inheritdoc />
        /// </summary>
        /// <param name="item">An item of <typeparamref name="T"/></param>
        /// <returns>Returns self for chaining</returns>
        public IConsoleMenu<T> AddItem(T item)
        {
            this.items.Add(item);
            return this;
        }

        /// <summary>
        /// O(n)
        /// <inheritdoc />
        /// </summary>
        /// <param name="item">An item of <typeparamref name="T"/></param>
        /// <returns>Returns self for chaining</returns>
        public IConsoleMenu<T> RemoveItem(T item)
        {
            this.items.Remove(item);
            return this;
        }

        /// <inheritdoc />
        public virtual T Show(ConsoleColor foreground, ConsoleColor background)
        {
            Console.CursorVisible = false;

            int top = Console.CursorTop,
                bottom = top + this.items.Count - 1,
                highlighted = top;

            Action<ConsoleColor, ConsoleColor, int, int> highlightMethod = this.HighlightItem;
            if (!string.IsNullOrEmpty(this.prefix))
            {
                highlightMethod = this.HighlightItemWithPrefix;
            }

            ConsoleKeyInfo pressedKey;
            do
            {
                highlightMethod(foreground, background, top, highlighted);

                pressedKey = Console.ReadKey(true);
                highlighted = SetHighlightPosition(pressedKey, highlighted, bottom, top);
            }
            while (pressedKey.Key != ConsoleKey.Enter);

            this.HideMenu(top);
            Console.CursorVisible = true;

            return this.items[highlighted - top];
        }

        private static int SetHighlightPosition(
            ConsoleKeyInfo pressedKey, int highlighted, int bottom, int top)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.RightArrow:
                    if (highlighted + 1 <= bottom)
                    {
                        highlighted++;
                    }
                    else
                    {
                        highlighted = top;
                    }

                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.LeftArrow:
                    if (highlighted - 1 >= top)
                    {
                        highlighted--;
                    }
                    else
                    {
                        highlighted = bottom;
                    }

                    break;
            }

            return highlighted;
        }

        private void HideMenu(int top)
        {
            Console.CursorTop = top;
            int additionalLength = 0;
            if (!string.IsNullOrEmpty(this.prefix))
            {
                additionalLength += this.prefix.Length + this.prefix.Length.ToString().Length + 4;
            }

            foreach (var item in this.items)
            {
                // Overwrites whitespace with the length of the option + length of the prefix if any
                Console.WriteLine(
                    new string(' ', item.ToString().Length + additionalLength));
            }

            Console.CursorTop = top;
        }

        private void HighlightItem(
            ConsoleColor foreground, ConsoleColor background, int top, int highlighted)
        {
            Console.CursorTop = top;
            foreach (T item in this.items)
            {
                this.Writer.WriteLine(item.ToString(), foreground);
            }

            Console.CursorTop = highlighted;
            this.Writer.WriteLine(this.items[highlighted - top].ToString(), background, foreground);
        }

        private void HighlightItemWithPrefix(
            ConsoleColor foreground, ConsoleColor background, int top, int highlighted)
        {
            Console.CursorTop = top;

            for (int i = 0; i < this.items.Count; i++)
            {
                this.Writer.WriteLine(
                    $"{this.prefix} {i + 1}: {this.items[i]}",
                    foreground);
            }

            Console.CursorTop = highlighted;
            this.Writer.WriteLine(
                $"{this.prefix} {highlighted - top + 1}: {this.items[highlighted - top]}",
                background,
                foreground);
        }
    }
}
