using System;
using yamlist.Modules.Commands.Options;
using yamlist.Modules.Commands.Routing;

namespace yamlist.Commands
{
    [Binds(typeof(FormatArguments))]
    public class FormatCommand
    {
        public FormatCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(FormatArguments args)
        {
            Console.WriteLine("To install DOTNET Core, please see: https://dotnet.microsoft.com/download");
            return 0;
        }
    }
}