using StaticWise.Compiler;
using StaticWise.Entities;
using StaticWise.Compiler.Utilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using StaticWise.Common.Files;
using StaticWise.Common.Deserialize;
using StaticWise.Common.Queries;
using System.Diagnostics;
using StaticWise.Common.Urls;

namespace StaticWise
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ILogger log = new Logger();
            string pathToConfig = args.ElementAtOrDefault(0);

            if (!string.IsNullOrEmpty(pathToConfig))
            {
                IFileManager fileManager = new FileManager();
                IDeserializeManager deserializeManager = new DeserializeManager(fileManager);
                IQueryManager queryManager = new QueryManager(deserializeManager, fileManager);
                IUrlManager urlManager = new UrlManager();

                Config config = deserializeManager.DeserializeConfig(pathToConfig);

                try
                {
                    Compile compile = new Compile(
                        config,
                        log,
                        fileManager,
                        queryManager,
                        urlManager);
                    compile.Build();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }
            }
            else
                log.Error("No parameter for a configuration file path was found");

            sw.Stop();

            List<LogEntry> logEntries = log.GetEntries();
            if (logEntries.Any())
            {
                foreach (LogEntry logEntry in logEntries)
                {
                    Console.WriteLine(string.Format("{0}: {1}",
                        logEntry.Status.ToString(), logEntry.Message));
                }
            }

            Console.WriteLine("Time elapsed: {0}", sw.Elapsed);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
        }
    }
}