namespace HomeworkHelpers
{
    using System;
    using ConsoleEnchantments;

    /// <summary>
    /// A helper for Console Applications
    /// </summary>
    public abstract class ConsoleHelper
    {
        private ConsoleMio console;

        /// <summary>
        /// Holds the ConsoleMio single instance
        /// </summary>
        public ConsoleMio ConsoleMio => this.console ?? (this.console = new ConsoleMio());

        /// <summary>
        /// Restarts the given method, usually for Main.
        /// </summary>
        /// <param name="restartCallback"></param>
        public void Restart(Action restartCallback)
        {
            Console.WriteLine();
            this.console.PrintHeading("PRESS ANY KEY TO RESTART or Ctrl + C to exit");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;

            restartCallback();
        }
    }
}
