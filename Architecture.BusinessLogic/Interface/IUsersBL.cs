using Architecture.Entities;
using Architecture.Interface;
using System.Collections.Generic;

namespace Architecture.BusinessLogic
{
    public interface IUsersBL
    {
        List<UsersEntity> GetUsersEntity(int pageIndex = 0, int pageSize = int.MaxValue);
        UsersEntity CreataUser(UsersEntity user);
        UsersEntity UpdateUser(UsersEntity user);
        UsersEntity GetUsersEntityById(long userId);
        LoginViewModelResponse Validateuser(string userName, string password);
        bool CheckEmail(string userName);
        void GeneratePassword(string userId, string password, string passwordEncryptionSecurityKey);
    }
}
