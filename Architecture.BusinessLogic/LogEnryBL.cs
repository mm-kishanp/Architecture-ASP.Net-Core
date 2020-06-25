using Architecture.DataBase.DatabaseFirst.Models;
using Architecture.Entities;
using Architecture.Interface;
using Architecture.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.BusinessLogic
{
    public class LogEnryBL : ILogEntryBL
    {
        private readonly ILogEntry _logEntry;

        public LogEnryBL(ILogEntry logEntry)
        {
            _logEntry = logEntry;
        }

        public List<LogEntryEntity> GetLogEntryEntities(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var logEntry = _logEntry.GetLogEntryPaging(pageIndex, pageSize);
            return logEntry.Select(x => PrepareModelToEntity(x)).ToList(); 
        }

        public LogEntryEntity GetUsersEntityById(long userId)
        {
            var LogEntryEntity = _logEntry.GetLogEntry(userId);
            return PrepareModelToEntity(LogEntryEntity);
        }
        public LogEntryEntity CreataLogEntry(LogEntryEntity logEntryEntity)
        {
            LogEntry newLogEntry = PrepareEntityToModel(logEntryEntity);
            newLogEntry.CreatedDate = DateTime.Now;
            newLogEntry.CreatedUtcdate = DateTime.UtcNow;
            _logEntry.AddLogEntry(newLogEntry);

            logEntryEntity.Id = newLogEntry.Id;
            return logEntryEntity;
        }

        public LogEntryEntity UpdateLogEntry(LogEntryEntity logEntryEntity)
        {
            logEntryEntity.UpdatedDate = DateTime.Now;
            logEntryEntity.UpdatedUtcdate = DateTime.UtcNow;
            var newLogEntry = PrepareEntityToModel(logEntryEntity);
            _logEntry.UpdateLogEntry(newLogEntry);

            logEntryEntity.Id = newLogEntry.Id;
            return logEntryEntity;
        }



        #region Private Methods
        private LogEntryEntity PrepareModelToEntity(LogEntry logEntry)
        {
            return new LogEntryEntity()
            {
                CreatedBy = logEntry.CreatedBy,
                CreatedDate = logEntry.CreatedDate,
                CreatedUtcdate = logEntry.CreatedUtcdate,
                Id = logEntry.Id,
                IsActive = logEntry.IsActive,
                IsApproved = logEntry.IsApproved,
                UpdatedBy = logEntry.UpdatedBy,
                UpdatedDate = logEntry.UpdatedDate,
                UpdatedUtcdate = logEntry.UpdatedUtcdate,

                ActionDescriptor = logEntry.ActionDescriptor,
                IpAddress = logEntry.IpAddress,
                Message = logEntry.Message,
                RequestId = logEntry.RequestId,
                RequestPath = logEntry.RequestPath,
                Source = logEntry.Source,
                StackTrace = logEntry.StackTrace,
                TimeStamp = logEntry.TimeStamp,
                Type = logEntry.Type,
                User = logEntry.User,
            };
        }


        private LogEntry PrepareEntityToModel(LogEntryEntity logEntryEntity)
        {
            return new LogEntry()
            {
                CreatedBy = logEntryEntity.CreatedBy,
                CreatedDate = logEntryEntity.CreatedDate,
                CreatedUtcdate = logEntryEntity.CreatedUtcdate,
                Id = logEntryEntity.Id,
                IsActive = logEntryEntity.IsActive,
                IsApproved = logEntryEntity.IsApproved,
                UpdatedBy = logEntryEntity.UpdatedBy,
                UpdatedDate = logEntryEntity.UpdatedDate,
                UpdatedUtcdate = logEntryEntity.UpdatedUtcdate,

                ActionDescriptor = logEntryEntity.ActionDescriptor,
                IpAddress = logEntryEntity.IpAddress,
                Message = logEntryEntity.Message,
                RequestId = logEntryEntity.RequestId,
                RequestPath = logEntryEntity.RequestPath,
                Source = logEntryEntity.Source,
                StackTrace = logEntryEntity.StackTrace,
                TimeStamp = logEntryEntity.TimeStamp,
                Type = logEntryEntity.Type,
                User = logEntryEntity.User,
            };
        }

        #endregion
    }
}
