namespace HomeworkHelpers
{
    using ConsoleEnchantments;

    public abstract class ConsoleHelper
    {
        private ConsoleMio console;

        public ConsoleMio ConsoleMio => this.console ?? (this.console = new ConsoleMio());
    }
}
