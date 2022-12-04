using AutoMapper;
using Coffee.API.Data.Dtos.UserCad;
using Coffee.API.Model.Cadastro;
using Microsoft.AspNetCore.Identity;

namespace Coffee.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserInsert, Usuario>();

            CreateMap<Usuario, IdentityUser<int>>();

        }


    }
}
