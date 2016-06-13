namespace ConsoleMio.ConsoleEnhancements.Contracts
{
    using System;

    /// <summary>
    /// A helper that wrties colored text over the console
    /// </summary>
    public interface IConsoleWriter
    {
        /// <summary>
        /// Writes the text in the given color
        /// </summary>
        /// <param name="text">A text string</param>
        /// <param name="color">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <returns>Returns self for chaining</returns>
        IConsoleWriter Write(string text, ConsoleColor color);

        /// <summary>
        /// Writes the text in the given color and with the given background color
        /// </summary>
        /// <param name="text">A text string</param>
        /// <param name="color">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <param name="background">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <returns>Returns self for chaining</returns>
        IConsoleWriter Write(string text, ConsoleColor color, ConsoleColor background);

        /// <summary>
        /// Writes a line of text in the given color and ends the current line
        /// </summary>
        /// <param name="text">A line of text</param>
        /// <param name="color">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <returns>Returns self for chaining</returns>
        IConsoleWriter WriteLine(string text, ConsoleColor color);

        /// <summary>
        /// Writes a line of text in the given colors and ends the current line
        /// </summary>
        /// <param name="text">A line of text</param>
        /// <param name="color">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <param name="background">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <returns>Returns self for chaining</returns>
        IConsoleWriter WriteLine(string text, ConsoleColor color, ConsoleColor background);

        /// <summary>
        /// Writes a formated string in the given color similary to String.Format method
        /// </summary>
        /// <param name="text">String containing placehoders <example>"{0} {1}"</example></param>
        /// <param name="color">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <param name="args">Arguments that will be inserted in the placeholders</param>
        /// <returns></returns>
        IConsoleWriter Format(string text, ConsoleColor color, params object[] args);

        /// <summary>
        /// Writes a formated line in the given color similary to String.Format method
        /// </summary>
        /// <param name="text">String containing placehoders <example>"{0} {1}"</example></param>
        /// <param name="color">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <param name="args">Arguments that will be inserted in the placeholders</param>
        /// <returns></returns>
        IConsoleWriter FormatLine(string text, ConsoleColor color, params object[] args);
    }
}