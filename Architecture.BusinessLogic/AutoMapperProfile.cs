using AutoMapper;
using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.Entities;

namespace Architecture.BusinessLogic
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<UsersEntity, Users>();
            CreateMap<Users, UsersEntity>();

            CreateMap<Users, UsersEntity>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        //// ignore null role
                        //if (x.DestinationMember.Name == "Role" && src.FirstName == null) return false;

                        return true;
                    }
                ));
        }
    }
}