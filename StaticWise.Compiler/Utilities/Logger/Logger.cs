using StaticWise.Entities;
using System.Collections.Generic;

namespace StaticWise.Compiler.Utilities.Logger
{
    public class Logger : ILogger
    {
        #region Properties

        private List<LogEntry> _logEntries;

        #endregion

        #region Constructors

        public Logger()
        {
            _logEntries = new List<LogEntry>();
        }

        #endregion

        #region Methods

        void ILogger.Error(string message)
        {
            if (!string.IsNullOrEmpty(message))
                _logEntries.Add(
                    new LogEntry
                    {
                        Message = message,
                        Status = LogStatus.Error
                    });
        }

        void ILogger.Warning(string message)
        {
            if (!string.IsNullOrEmpty(message))
                _logEntries.Add(
                new LogEntry
                {
                    Message = message,
                    Status = LogStatus.Warning
                });
        }

        void ILogger.Info(string message)
        {
            if (!string.IsNullOrEmpty(message))
                _logEntries.Add(
                new LogEntry
                {
                    Message = message,
                    Status = LogStatus.Info
                });
        }

        List<LogEntry> ILogger.GetEntries()
        {
            return _logEntries;
        }

        #endregion
    }
}