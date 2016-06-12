namespace HomeworkHelpers.ConsoleEnchantments
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    /// <summary>
    /// Console helper library for printig colored messages
    /// </summary> 
    public class ConsoleMio : IConsoleWriter, IConsoleReader
    {
        public ConsoleMio()
        {

        }

        public ConsoleMio(IConsoleWriter writer, IConsoleReader reader)
        {

        }

        /// <summary>
        /// Sets up overall console apperanace, ecnoding to unicode and window width
        /// </summary>
        /// <param name="color"></param>
        /// <param name="background"></param>
        /// <param name="windowWidth"></param>
        public void Setup(
            ConsoleColor color = ConsoleColor.Black
            , ConsoleColor background = ConsoleColor.White
            , int windowWidth = 120)
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

        /// <summary>
        /// Restarts the given method, usually for Main.
        /// </summary>
        /// <param name="restartCallback"></param>
        public void Restart(Action restartCallback)
        {
            Console.WriteLine();
            this.PrintHeading("PRESS ANY KEY TO RESTART or Ctrl + C to exit");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;

            restartCallback();
        }

        public IConsoleWriter Write(string text, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write(text);

            Console.ForegroundColor = previousColor;

            return this;
        }

        public IConsoleWriter Write(string text, ConsoleColor color, params object[] args)
        {
            return this.Write(string.Format(text, args), color);
        }

        public IConsoleWriter Write(string text, ConsoleColor color, ConsoleColor background)
        {
            var previousBackground = Console.BackgroundColor;
            Console.BackgroundColor = background;

            this.Write(text, color);

            Console.BackgroundColor = previousBackground;

            return this;
        }

        public IConsoleWriter WriteLine(string text, ConsoleColor color)
        {
            this.Write(text, color);
            Console.WriteLine();

            return this;
        }

        public IConsoleWriter WriteLine(string text, ConsoleColor color, ConsoleColor background)
        {
            this.Write(text, color, background);
            Console.WriteLine();

            return this;
        }

        public IConsoleWriter WriteLine(string text, ConsoleColor color, params object[] args)
        {
            return this.WriteLine(string.Format(text, args), color);
        }

        public string ReadLine(ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            string input = Console.ReadLine();

            Console.ForegroundColor = previousColor;

            return input;
        }

        public string ReadLine(ConsoleColor color, ConsoleColor background)
        {
            var previousBackground = Console.BackgroundColor;
            Console.BackgroundColor = background;

            this.ReadLine(color);

            Console.BackgroundColor = previousBackground;

            var previousColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            string input = Console.ReadLine();

            Console.ForegroundColor = previousColor;

            return input;
        }

        public void PrintHeading(string text, ConsoleColor color = ConsoleColor.White)
        {
            int totalWidth = Console.WindowWidth;
            string format = string.Format(" {0} ", text);
            char paddingChar = ' ';
            var paddingColor = ConsoleColor.White;
            var backgorundColor = ConsoleColor.DarkGray;
            int freeWidth = totalWidth - format.Length;

            this.Write(new string(paddingChar, totalWidth), paddingColor, backgorundColor);
            this.Write(new string(paddingChar, freeWidth / 2), paddingColor, backgorundColor);
            this.Write(format, color, backgorundColor);
            this.Write(new string(paddingChar, freeWidth / 2), paddingColor, backgorundColor);
            this.Write(new string(paddingChar, totalWidth), paddingColor, backgorundColor);
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Creates a <see cref="ConsoleMenu{T}"/> from a list of items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns>Returns a menu that can be displayed</returns>
        public ConsoleMenu<T> CreateMenu<T>(IList<T> items)
        {
            return new ConsoleMenu<T>(this, items);
        }

        /// <summary>
        /// Creates a <see cref="ConsoleMenu{T}"/> from a list of items
        /// the items are displayed with the given <paramref name="prefix"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="prefix">A template prefix that will be printed before each item</param>
        /// <returns>Returns a menu that can be displayed</returns>
        public ConsoleMenu<T> CreateMenu<T>(IList<T> items, string prefix)
        {
            return new ConsoleMenu<T>(this, items, prefix);
        }
    }
}
