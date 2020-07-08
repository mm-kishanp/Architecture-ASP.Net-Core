using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.Interface;
using Architecture.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.Repository
{
    public class UserRoleRepository : IUserRole
    {
        private readonly IRepository<UserRole> _usersRole;

        public UserRoleRepository(IRepository<UserRole> usersRole)
        {
            _usersRole = usersRole;
        }

        public IEnumerable<UserRole> GetUserRoles()
        {
            return _usersRole.Table.AsEnumerable();
        }
        public IPagedList<UserRole> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<UserRole>(_usersRole.Table, pageIndex, pageSize);
        }

        public UserRole GetUserRoleById(long userId)
        {
            if (userId == 0)
            {
                return null;
            }
            return _usersRole.GetById(userId);
        }

        public UserRole AddUserRole(UserRole newUser)
        {
            _usersRole.Insert(newUser);
            return newUser;
        }

        public UserRole UpdateUserRole(UserRole newUser)
        {
            _usersRole.Update(newUser);
            return newUser;
        }
    }
}
