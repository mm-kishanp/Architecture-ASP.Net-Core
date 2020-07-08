using System;
using System.Collections.Generic;

namespace Architecture.DataBase.DataBaseFirst.Models
{
    public partial class LogEntry
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? UpdatedUtcdate { get; set; }
        public DateTime? CreatedUtcdate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string ActionDescriptor { get; set; }
        public string IpAddress { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }
        public string RequestPath { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
    }
}
