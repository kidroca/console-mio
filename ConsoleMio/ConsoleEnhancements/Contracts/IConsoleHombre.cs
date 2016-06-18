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
        ConsoleMenu<T> CreateMenu<T>(params T[] items);

        /// <summary>
        /// Creates a <see cref="ConsoleMenu{T}"/> from a list of items
        /// the items are displayed with the given <paramref name="prefix"/>
        /// </summary>
        /// <typeparam name="T">The type of the objects in the menu</typeparam>
        /// <param name="prefix">A template prefix that will be printed before each item</param>
        /// <param name="items">A list of object that will be selectable from the menu</param>
        /// <returns>Returns a menu that can be displayed</returns>
        ConsoleMenu<T> CreateMenu<T>(string prefix, params T[] items);

        /// <summary>
        /// Prompts the user for interaction in order to continue the program execution
        /// </summary>
        /// <param name="color">The color of the text that may appear</param>
        void PromptToContinue(ConsoleColor color);
    }
}
