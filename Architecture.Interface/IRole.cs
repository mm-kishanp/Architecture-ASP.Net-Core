using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.Services;
using System.Collections.Generic;

namespace Architecture.Interface
{
    public interface IRole
    {
        IEnumerable<Role> GetRoles();
        Role GetRoleById(long Id);
        IPagedList<Role> GetRolePaging(int pageIndex = 0, int pageSize = int.MaxValue);
        Role AddRole(Role userRole);
        Role UpdateRole(Role userRole);
    }
}
