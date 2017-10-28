using System.Collections.Generic;
using StaticWise.Entities;

namespace StaticWise.Compiler.Utilities.Logger
{
    public interface ILogger
    {
        /// <summary>
        /// Add an error to the log entries
        /// </summary>
        /// <param name="message">The error message to add</param>
        void Error(string message);

        /// <summary>
        /// Get all entries in the log
        /// </summary>
        /// <returns>A list of all log entries</returns>
        List<LogEntry> GetEntries();

        /// <summary>
        /// Add an informational message to the log entries
        /// </summary>
        /// <param name="message">The informational message to add</param>
        void Info(string message);

        /// <summary>
        /// Add a warning to the log entries
        /// </summary>
        /// <param name="message">The warning message to add</param>
        void Warning(string message);
    }
}