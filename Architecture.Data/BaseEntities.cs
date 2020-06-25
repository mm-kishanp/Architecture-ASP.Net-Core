using System;

namespace Architecture.Data
{
    public class BaseEntities
    {
        public BaseEntities()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.UpdatedDate = DateTime.UtcNow;
            this.UpdatedUTCDate = DateTime.UtcNow;
            this.CreatedUTCDate = DateTime.UtcNow;
        }

        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
      
        public DateTime? UpdatedDate { get; set; }
        public DateTime? UpdatedUTCDate { get; set; }
        public DateTime? CreatedUTCDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
