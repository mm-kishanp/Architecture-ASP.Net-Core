using Architecture.DataBase.DatabaseFirst.Models;
using Architecture.Interface;
using Architecture.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.Repository
{
    public class UserRepository : IUser
    {
        private readonly IRepository<Users> _users;
        public UserRepository(IRepository<Users> users)
        {
            _users = users;
        }

        public IEnumerable<Users> GetUsers()
        {
            return _users.Get(includeProperties: "UserRole");
        }

        public IPagedList<Users> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<Users>(_users.Get(includeProperties: "UserRole").ToList(), pageIndex, pageSize);
        }

        public Users GetUsersById(long userId)
        {
            if (userId == 0)
            {
                return null;
            }
            return _users.GetById(userId);
        }

        public Users AddUser(Users newUser)
        {
            _users.Insert(newUser);
            return newUser;
        }

        public Users UpdateUser(Users newUser)
        {
            _users.Update(newUser);
            return newUser;
        }

        public bool ValidateLastChanged(string lastChanged, string userName)
        {
            return _users.Table.Any(x => x.EmailId == userName && (x.UpdatedDate <= Convert.ToDateTime(lastChanged) || x.UpdatedDate == null));
        }

        public Users GetUsersByEmail(string userName)
        {
            return _users.Table.FirstOrDefault(x => x.EmailId == userName);
        }
    }
}
