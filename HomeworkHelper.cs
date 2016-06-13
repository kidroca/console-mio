namespace ConsoleMio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides collection of methods useful for homeworks or tests
    /// </summary>
    public class HomeworkHelper : ConsoleHelper
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Reads a sequence from the console and tries to convert it to the given template type
        /// </summary>
        /// <typeparam name="T">Works with all convertible types</typeparam>
        /// <param name="template">
        /// A template parameter whose value is irrelevant, 
        /// it only feed in the tpye of the output collection
        /// </param>
        /// <param name="splitChars">
        /// The desired split characters - set to null to use the default 
        /// single space separato
        /// r</param>
        /// <returns></returns>
        public ICollection<T> ReadCollection<T>(T template, char[] splitChars) where T : IComparable
        {
            if (splitChars == null)
            {
                splitChars = new[] { ' ' };
            }

            List<T> collection = new List<T>();

            Console.Write(
                "Enter a sequence of numbers, to end the sequence enter a blank line: ");

            string inputLine;
            while ((inputLine = this.ConsoleMio.ReadLine(ConsoleColor.DarkCyan)) != string.Empty)
            {
                T[] current = inputLine
                    .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => (T)Convert.ChangeType(n, typeof(T)))
                    .ToArray();

                collection.AddRange(current);
            }

            return collection;
        }

        /// <summary>
        /// Generates a random numeric collection of {T}
        /// </summary>
        /// <typeparam name="T">The type of the generated collection</typeparam>
        /// <param name="template">Used as a sample for the collection</param>
        /// <param name="size">The length of the generated collection</param>
        /// <param name="min">Lower boundry for the generated collection</param>
        /// <param name="max">Upper boundry for the generated collection</param>
        /// <returns></returns>
        public ICollection<T> GenerateRandomNumericCollection<T>(
            T template, long size, int min = -10000, int max = 10000)
            where T : struct, IComparable, IConvertible, IFormattable
        {
            if (max < min)
            {
                max ^= min;
                min ^= max;
                max ^= min;
            }

            int range = max - min;

            var result = new T[size];
            for (int i = 0; i < size; i++)
            {
                double randomNumber = (Random.NextDouble() * range) + min;
                T generated = (T)Convert.ChangeType(randomNumber, typeof(T));
                result[i] = generated;
            }

            return result;
        }

        /// <summary>
        /// A more convenient but slightly more restrictive (Doesn't generate strings) 
        /// collection generator
        /// </summary>
        /// <typeparam name="T">
        /// Should work for all numeric types
        /// </typeparam>
        /// <param name="template">
        /// A template parameter whose value is irrelevant, 
        /// it only feed in the tpye of the output collection
        /// </param>
        /// <param name="size"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>A random generated collection of the given type</returns>
        public ICollection<T> GetCollectionFromUserChoice<T>(
            T template, int size = 1000, int min = -10000, int max = 10000)
            where T : struct, IComparable, IConvertible, IFormattable
        {
            ConsoleKey userChoice;
            do
            {
                this.ConsoleMio.Write(
                "Before you lies the choice submit boring data by hand or use the random generator!\n" +
                "What say you (Y = Boring, N = Give Me All The Things!)\n",
                ConsoleColor.DarkCyan);

                userChoice = Console.ReadKey(true).Key;
            }
            while (userChoice != ConsoleKey.Y && userChoice != ConsoleKey.N);

            ICollection<T> collection;

            if (userChoice == ConsoleKey.Y)
            {
                collection = this.ReadCollection(template, new[] { ' ', ',' });
            }
            else
            {
                collection = this.GenerateRandomNumericCollection(template, size, min, max);
            }

            return collection;
        }
    }
}
