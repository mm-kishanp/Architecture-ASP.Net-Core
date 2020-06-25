using System;
using System.Collections.Generic;

namespace Architecture.DataBase.DatabaseFirst.Models
{
    public partial class UserRoleModuleAction
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
        public long UserRoleId { get; set; }
        public long RoleModuleAction { get; set; }

        public virtual RoleModuleAction RoleModuleActionNavigation { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
