namespace HomeworkHelpers.ConsoleEnchantments.Contracts
{
    using System;

    /// <summary>
    /// A Simple select up and down from a list of options Console Menu
    /// </summary>
    /// <typeparam name="T">The type of the objects in the menu</typeparam>
    public interface IConsoleMenu<T>
    {
        /// <summary>
        /// Adds an item to the menu
        /// </summary>
        /// <param name="item">The item to be added</param>
        /// <returns>Returns self for chaining</returns>
        ConsoleMenu<T> AddItem(T item);

        /// <summary>
        /// Removes an item from the menu
        /// </summary>
        /// <param name="item">The item that must be removed</param>
        /// <returns>Returns self for chaining</returns>
        ConsoleMenu<T> RemoveItem(T item);

        /// <summary>
        /// Displays the menu, the menu stays on until an option is selected 
        /// with the 'Enter' key
        /// </summary>
        /// <returns>The coresponding selected item</returns>
        T Show(ConsoleColor foreground, ConsoleColor background);
    }
}