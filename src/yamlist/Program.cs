﻿using System;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Parsers;
using yamlist.Modules.IO;
using yamlist.Modules.IO.Console;
using Version = yamlist.Modules.Version;

namespace yamlist
{
    internal class Program
    {
        private static readonly Router _router = new Router();

        private static int Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "--help" || args[0] == "/?" || args[0] == "?")
            {
                PrintUsage();
                return 0;
            }

            var result = 0;

            try
            {
                var dispatcher = _router.Route(args);
                if (dispatcher == null)
                {
                    PrintUsage();
                    return 1;
                }

                result = dispatcher.Execute();
            }
            catch (Exception err)
            {
                using (new Colour(ConsoleColor.Red, ConsoleColor.Black))
                {
                    Console.WriteLine(err.ToString());
                    Console.WriteLine();
                    Console.WriteLine(err.InnerException?.Message);
                    Console.WriteLine("Exiting with code -1");
                    result = -1;
                }

                Console.WriteLine("\r\n");
            }

            return result;
        }


        private static void PrintUsage()
        {
            Console.WriteLine($"yi v{Version.GetVersion()} by realorko \r\n");
            CommandParser.InfoAll();
        }
    }
}