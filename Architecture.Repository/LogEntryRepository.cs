using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.Interface;
using Architecture.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.Repository
{
    public class LogEntryRepository : ILogEntry
    {
        private readonly IRepository<LogEntry> _logEntry;

        public LogEntryRepository(IRepository<LogEntry> logEntry)
        {
            _logEntry = logEntry;
        }

        public IEnumerable<LogEntry> GetLogEntries()
        {
            return _logEntry.Table.AsEnumerable();
        }
        public IPagedList<LogEntry> GetLogEntryPaging(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<LogEntry>(_logEntry.Table, pageIndex, pageSize);
        }

        public LogEntry GetLogEntry(long Id)
        {
            if (Id == 0)
            {
                return null;
            }
            return _logEntry.GetById(Id);
        }

        public LogEntry AddLogEntry(LogEntry newUser)
        {
            _logEntry.Insert(newUser);
            return newUser;
        }

        public LogEntry UpdateLogEntry(LogEntry newUser)
        {
            _logEntry.Update(newUser);
            return newUser;
        }
    }
}
