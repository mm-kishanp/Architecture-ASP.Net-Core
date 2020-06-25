using System;
using System.Collections.Generic;

namespace Architecture.DataBase.DatabaseFirst.Models
{
    public partial class ModuleAction
    {
        public ModuleAction()
        {
            RoleModuleAction = new HashSet<RoleModuleAction>();
        }

        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? UpdatedUtcdate { get; set; }
        public DateTime? CreatedUtcdate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long ModuleId { get; set; }
        public long ActionId { get; set; }

        public virtual Action Action { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<RoleModuleAction> RoleModuleAction { get; set; }
    }
}
