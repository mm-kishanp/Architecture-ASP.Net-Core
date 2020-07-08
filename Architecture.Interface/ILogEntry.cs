using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.Services;
using System.Collections.Generic;

namespace Architecture.Interface
{
    public interface ILogEntry
    {
        IEnumerable<LogEntry> GetLogEntries();
        LogEntry GetLogEntry(long Id);
        IPagedList<LogEntry> GetLogEntryPaging(int pageIndex = 0, int pageSize = int.MaxValue);
        LogEntry AddLogEntry(LogEntry newLogEntry);
        LogEntry UpdateLogEntry(LogEntry newLogEntry);
    }
}
