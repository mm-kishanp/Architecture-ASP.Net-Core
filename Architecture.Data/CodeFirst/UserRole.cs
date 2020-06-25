using Architecture.Data;

namespace Architecture.DataBase.CodeFirst
{
    public class UserRole : BaseEntities
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
