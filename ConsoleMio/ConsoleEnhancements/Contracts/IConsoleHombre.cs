namespace ConsoleMio.ConsoleEnhancements.Contracts
{
    using System;

    /// <summary>
    /// Provides helper methods used for prettier rendering
    /// </summary>
    public interface IConsoleHombre
    {
        /// <summary>
        /// Creates a heading text from the provided text and color
        /// </summary>
        /// <param name="text">A heading text</param>
        /// <param name="color">
        /// Color from the <see cref="ConsoleColor"/> enumeration, defaults to white color
        /// </param>
        void PrintHeading(string text, ConsoleColor color = ConsoleColor.White);

        /// <summary>
        /// Creates a <see cref="ConsoleMenu{T}"/> from a list of items
        /// </summary>
        /// <typeparam name="T">The type of the objects in the menu</typeparam>
        /// <param name="items">A list of object that will be selectable from the menu</param>
        /// <returns>Returns a menu that can be displayed</returns>
        IConsoleMenu<T> CreateMenu<T>(params T[] items);

        /// <summary>
        /// Creates a <see cref="ConsoleMenu{T}"/> from a list of items
        /// the items are displayed with the given <paramref name="prefix"/>
        /// </summary>
        /// <typeparam name="T">The type of the objects in the menu</typeparam>
        /// <param name="prefix">A template prefix that will be printed before each item</param>
        /// <param name="items">A list of object that will be selectable from the menu</param>
        /// <returns>Returns a menu that can be displayed</returns>
        IConsoleMenu<T> CreateMenu<T>(string prefix, params T[] items);

        /// <summary>
        /// Creates a <see cref="PromptMenu{T}"/> from a list of items
        /// </summary>
        /// <typeparam name="T">The type of the objects in the menu</typeparam>
        /// <param name="prompt">A text to be displayed above the menu</param>
        /// <param name="items">A list of object that will be selectable from the menu</param>
        /// <returns>Returns a menu prompt that can be displayed</returns>
        IConsoleMenu<T> CreatePromptMenu<T>(string prompt, params T[] items);

        /// <summary>
        /// Prompts the user for interaction in order to continue the program execution
        /// </summary>
        /// <param name="color">The color of the text that may appear</param>
        void PromptToContinue(ConsoleColor color);

        /// <summary>
        /// Reads generic input from the console and transforms it to the given type using
        /// the provided function
        /// </summary>
        /// <typeparam name="T">The type that console input should be converted to</typeparam>
        /// <param name="message">Prompt message before reading the input</param>
        /// <param name="splitPoints">
        /// Strings (like " ", "," "-") on which to split input to separate parameters
        /// </param>
        /// <param name="transform">
        /// A transform function that will be used to convert each entry in to the specific type
        /// </param>
        /// <param name="parametersCount">Exact number of parameters that must be read</param>
        /// <param name="messageColor">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <param name="inputColor">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <param name="errorColor">Color from the <see cref="ConsoleColor"/> enumeration</param>
        /// <returns>An array {T} values</returns>
        T[] ReadInput<T>(
            string message,
            string[] splitPoints,
            Func<string, T> transform,
            int parametersCount,
            ConsoleColor messageColor,
            ConsoleColor inputColor,
            ConsoleColor errorColor);

        /// <summary>
        /// Clears the rows from the console starting from the <paramref name="top"/> position
        /// Then returns to the start of the <paramref name="top"/> row
        /// </summary>
        /// <param name="top">The row from which to start from</param>
        /// <param name="count">How many rows to clear</param>
        void ClearRows(int top, int count);
    }
}
