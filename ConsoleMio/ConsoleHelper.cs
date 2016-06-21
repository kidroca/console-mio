namespace ConsoleMio
{
    using System;
    using ConsoleEnhancements;

    /// <summary>
    /// A helper for Console Applications
    /// </summary>
    public abstract class ConsoleHelper
    {
        private ConsoleMio console;

        /// <summary>
        /// Gets the ConsoleMio single instance
        /// </summary>
        /// <value>
        /// Holds the ConsoleMio single instance
        /// </value>
        public ConsoleMio ConsoleMio => this.console ?? (this.console = new ConsoleMio());

        /// <summary>
        /// Restarts the given method, usually for Main.
        /// </summary>
        /// <param name="restartCallback">
        /// An <see cref="Action"/> that will be called to restart the program
        /// after the user is alerted that a restart is taking place
        /// </param>
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
