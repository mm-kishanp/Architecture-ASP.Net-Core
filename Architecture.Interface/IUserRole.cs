using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.Services;
using System.Collections.Generic;

namespace Architecture.Interface
{
    public interface IUserRole
    {
        IEnumerable<UserRole> GetUserRoles();
        UserRole GetUserRoleById(long Id);
        IPagedList<UserRole> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue);
        UserRole AddUserRole(UserRole userRole);
        UserRole UpdateUserRole(UserRole userRole);
        
    }
}
