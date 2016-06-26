namespace ConsoleMio.ConsoleEnhancements.Contracts
{
    using System;

    /// <summary>
    /// A helper that reads input from the console in color
    /// </summary>
    public interface IConsoleReader
    {
        /// <summary>
        /// Reads the next line of text in the given color
        /// </summary>
        /// <param name="color">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <returns>The entered line of text</returns>
        string ReadLine(ConsoleColor color);

        /// <summary>
        /// Reads the next line of text in the given color and background color
        /// </summary>
        /// <param name="color">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <param name="background">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <returns>The entered line of text</returns>
        string ReadLine(ConsoleColor color, ConsoleColor background);
    }
}