using Architecture.Entities;
using Architecture.Interface;
using System.Collections.Generic;

namespace Architecture.BusinessLogic
{
    public interface ILogEntryBL
    {
        List<LogEntryEntity> GetLogEntryEntities(int pageIndex = 0, int pageSize = int.MaxValue);
        LogEntryEntity CreataLogEntry(LogEntryEntity user);
        LogEntryEntity UpdateLogEntry(LogEntryEntity user);
    }
}
