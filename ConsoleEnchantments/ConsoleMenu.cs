namespace HomeworkHelpers.ConsoleEnchantments
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    /// <inheritdoc />
    public class ConsoleMenu<T> : IConsoleMenu<T>
    {
        private readonly IConsoleWriter writer;

        private readonly IList<T> items;

        private readonly string prefix;

        /// <summary>
        /// Creates an empty menu
        /// </summary>
        /// <param name="writer">The writer used for rendering the menu</param>
        public ConsoleMenu(IConsoleWriter writer)
        {
            this.writer = writer;
            this.items = new List<T>();
        }

        /// <summary>
        /// Creates a menu from a list of options
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="items"></param>
        public ConsoleMenu(IConsoleWriter writer, IList<T> items)
        {
            this.writer = writer;
            this.items = items;
        }

        /// <summary>
        /// Creates a menu from a list of options displaying the options with the 
        /// specified <paramref name="prefix"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="items"></param>
        /// <param name="prefix"></param>
        public ConsoleMenu(IConsoleWriter writer, IList<T> items, string prefix)
            : this(writer, items)
        {
            this.prefix = prefix;
        }

        /// <summary>
        /// Gets the items of the menu
        /// </summary>
        public IList<T> MenuItems => this.items;

        /// <summary>
        /// O(1)
        /// <inheritdoc />
        /// </summary>
        public ConsoleMenu<T> AddItem(T item)
        {
            this.items.Add(item);
            return this;
        }

        /// <summary>
        /// O(n)
        /// <inheritdoc />
        /// </summary>
        public ConsoleMenu<T> RemoveItem(T item)
        {
            this.items.Remove(item);
            return this;
        }

        /// <inheritdoc />
        public T Show(ConsoleColor foreground, ConsoleColor background)
        {
            Console.CursorVisible = false;

            int top = Console.CursorTop,
                bottom = top + items.Count - 1,
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
                    new string(' ', (item.ToString().Length + additionalLength)));
            }

            Console.CursorTop = top;
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

        private void HighlightItem(
            ConsoleColor foreground, ConsoleColor background, int top, int highlighted)
        {
            Console.CursorTop = top;
            foreach (T item in this.items)
            {
                this.writer.WriteLine(item.ToString(), foreground);
            }

            Console.CursorTop = highlighted;
            this.writer.WriteLine(this.items[highlighted - top].ToString(), background, foreground);
        }

        private void HighlightItemWithPrefix(
            ConsoleColor foreground, ConsoleColor background, int top, int highlighted)
        {
            Console.CursorTop = top;

            for (int i = 0; i < this.items.Count; i++)
            {
                this.writer.WriteLine(
                    $"{this.prefix} {i + 1}: {this.items[i]}",
                    foreground);
            }

            Console.CursorTop = highlighted;
            this.writer.WriteLine(
                $"{this.prefix} {highlighted - top + 1}: {this.items[highlighted - top]}",
                background,
                foreground);
        }
    }
}
