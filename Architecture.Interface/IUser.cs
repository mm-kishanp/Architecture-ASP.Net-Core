using Architecture.DataBase.DatabaseFirst.Models;
using Architecture.Services;
using System.Collections.Generic;

namespace Architecture.Interface
{
    public interface IUser 
    {
        IEnumerable<Users> GetUsers();
        Users GetUsersById(long newsId);
        Users GetUsersByEmail(string userName);
        IPagedList<Users> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue);
        Users AddUser(Users newUser);
        Users UpdateUser(Users newUser);
        bool ValidateLastChanged(string lastChanged, string userName);
    }
}
