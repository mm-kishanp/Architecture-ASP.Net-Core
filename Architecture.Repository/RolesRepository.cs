using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.Interface;
using Architecture.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.Repository
{
    public class RolesRepository : IRole
    {
        private readonly IRepository<Role> _role;
        public RolesRepository(IRepository<Role> role)
        {
            _role= role;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _role.Table.AsEnumerable();
        }
        public IPagedList<Role> GetRolePaging(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<Role>(_role.Table, pageIndex, pageSize);
        }

        public Role GetRoleById(long userId)
        {
            if (userId == 0)
            {
                return null;
            }
            return _role.GetById(userId);
        }

        public Role AddRole(Role newUser)
        {
            _role.Insert(newUser);
            return newUser;
        }

        public Role UpdateRole(Role newUser)
        {
            _role.Update(newUser);
            return newUser;
        }
    }
}
