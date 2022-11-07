using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DTOs;

namespace DatingApp.API.Services
{
    public interface IAuthService
    {
        public string Login(AuthUserDto authUserDto);
        public string Register(RegisterUserDto registerUserDto);
    }
}