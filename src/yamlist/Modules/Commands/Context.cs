using System.Linq;

namespace yamlist.Modules.Commands
{
    public class Context
    {
        public Context(string[] args)
        {
            Command = args[0];
            Arguments = args.Skip(1).Take(args.Length - 1).ToArray();
        }

        public string Command { get; set; }
        public string[] Arguments { get; set; }

        public override string ToString()
        {
            return $"{nameof(Command)}: {Command}, {nameof(Arguments)}: {string.Join(",", Arguments)}";
        }
    }
}