using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.Entities;
using Architecture.Interface;
using Architecture.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.BusinessLogic
{
    public class UsersBL : IUsersBL
    {
        private readonly IUser _user;
        private readonly IUserRole _userRole;
        private readonly IRole _role;
        private IMapper _mapper;

        public UsersBL(IUser user, IUserRole userRole, IRole role, IMapper mapper)
        {
            _user = user;
            _userRole = userRole;
            _role = role;
            _mapper = mapper;
        }

        public List<UsersEntity> GetUsersEntity(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = _user.GetUsersPaging(pageIndex, pageSize);
            return _mapper.Map<List<UsersEntity>>(users);
            return PrepareUserToUsersEntity(users);
        }
        public UsersEntity GetUsersEntityById(long userId)
        {
            var users = _user.GetUsersById(userId);
            return _mapper.Map<UsersEntity>(users);
            return PrepareUserToUsersEntity(users);
        }
        public UsersEntity CreataUser(UsersEntity user)
        {
            var newUser = _mapper.Map<Users>(user);
            //var newUser = PrepareUsersEntityToUser(user);
            _user.AddUser(newUser);
            return user;
        }

        public UsersEntity UpdateUser(UsersEntity user)
        {
            user.UpdatedDate = DateTime.Now;
            user.UpdatedUtcdate = DateTime.UtcNow;
            var newUser = _mapper.Map<Users>(user);
            //var newUser = PrepareUsersEntityToUser(user);
            _user.UpdateUser(newUser);
            return user;
        }

        public LoginViewModelResponse Validateuser(string userName, string password)
        {

            var UserInfo = (from u in _user.GetUsers()
                            join ur in _userRole.GetUserRoles() on u.Id equals ur.UserId
                            join r in _role.GetRoles() on ur.RoleId equals r.Id into roles
                            where u.EmailId == userName && u.Password == password
                            select new LoginViewModelResponse()
                            {
                                Password = u.Password,
                                UserName = u.EmailId,
                                RoleId = roles.Select(y => y.Id.ToString()),
                                RoleName = roles.Select(y => y.RoleName)
                            }).FirstOrDefault();
            return UserInfo;
        }

        public bool CheckEmail(string userName)
        {
            return _user.GetUsers().Any(x => x.EmailId == userName);
        }

        public void GeneratePassword(string userId, string password, string passwordEncryptionSecurityKey)
        {
            var user = _user.GetUsersByEmail(userId);
            user.Password = Utility.Encryption.GetEncryptedJsonString(password, passwordEncryptionSecurityKey);
            _user.UpdateUser(user);
        }

        #region Private Methods
        private List<UsersEntity> PrepareUserToUsersEntity(IPagedList<Users> users)
        {
            return users.Select(user => new UsersEntity()
            {
                ContactNo = user.ContactNo,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                EmailId = user.EmailId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePic = user.ProfilePic,
                CreatedUtcdate = user.CreatedUtcdate,
                Id = user.Id,
                IsActive = user.IsActive,
                IsApproved = user.IsApproved,
                LoginAttempt = user.LoginAttempt,
                Password = user.Password,
                UpdatedBy = user.UpdatedBy,
                UpdatedDate = user.UpdatedDate,
                UpdatedUtcdate = user.UpdatedUtcdate
            }).ToList();
        }

        private UsersEntity PrepareUserToUsersEntity(Users user)
        {
            return new UsersEntity()
            {
                ContactNo = user.ContactNo,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                EmailId = user.EmailId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePic = user.ProfilePic,
                CreatedUtcdate = user.CreatedUtcdate,
                Id = user.Id,
                IsActive = user.IsActive,
                IsApproved = user.IsApproved,
                LoginAttempt = user.LoginAttempt,
                Password = user.Password,
                UpdatedBy = user.UpdatedBy,
                UpdatedDate = user.UpdatedDate,
                UpdatedUtcdate = user.UpdatedUtcdate,
            };
        }

        private Users PrepareUsersEntityToUser(UsersEntity usersEntity)
        {
            return new Users()
            {
                ContactNo = usersEntity.ContactNo,
                CreatedBy = usersEntity.CreatedBy,
                CreatedDate = usersEntity.CreatedDate,
                EmailId = usersEntity.EmailId,
                FirstName = usersEntity.FirstName,
                LastName = usersEntity.LastName,
                ProfilePic = usersEntity.ProfilePic,
                CreatedUtcdate = usersEntity.CreatedUtcdate,
                Id = usersEntity.Id,
                IsActive = usersEntity.IsActive,
                IsApproved = usersEntity.IsApproved,
                LoginAttempt = usersEntity.LoginAttempt,
                Password = usersEntity.Password,
                UpdatedBy = usersEntity.UpdatedBy,
                UpdatedDate = usersEntity.UpdatedDate,
                UpdatedUtcdate = usersEntity.UpdatedUtcdate
            };
        }
        #endregion
    }
}
