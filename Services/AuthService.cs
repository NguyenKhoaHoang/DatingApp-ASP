using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data.Entities;
using DatingApp.API.Data.Repositories;
using DatingApp.API.DTOs;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            this._tokenService = tokenService;
            this._userRepository = userRepository;

        }
        public string Login(AuthUserDto authUserDto)
        {
            authUserDto.Username = authUserDto.Username.ToLower();
            var currentUser = _userRepository.GetUserByUsername(authUserDto.Username);

            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("Username is invalid");
            }

            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(authUserDto.Password));
            for (int i = 0; i < currentUser.PasswordHashed.Length; i++)
            {
                if (currentUser.PasswordHashed[i] != passwordBytes[i])
                {
                    throw new UnauthorizedAccessException("Password is invalid");
                }
            }

            return _tokenService.CreateToken(currentUser.Username);
        }

        public string Register(RegisterUserDto registerUserDto)
        {
            registerUserDto.Username = registerUserDto.Username.ToLower();
            var currentUser = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (currentUser != null)
            {
                throw new BadHttpRequestException("Username is already registered");
            }
 
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUserDto.Password);
            var newUser = new User
            {
                Username = registerUserDto.Username,
                PasswordSalt = hmac.Key,
                PasswordHashed = hmac.ComputeHash(passwordBytes),
                Avatar = registerUserDto.Avatar,
                Email = registerUserDto.Email,
                DateOfBirth = registerUserDto.DateOfBirth,
                KnowAs = registerUserDto.KnowAs,
                Gender = registerUserDto.Gender,
                City = registerUserDto.City,
                Introduction = registerUserDto.Introduction,
            };
            _userRepository.InsertNewUser(newUser);
            _userRepository.IsSaveChanges();
            return _tokenService.CreateToken(newUser.Username);
        }
    }
}