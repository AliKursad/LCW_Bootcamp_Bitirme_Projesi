using LCW.Core.Security.Hashing;
using LCW.Core.Security.JWT;
using LCW.Core.Repositories;
using LCW.Core.Results;
using LCW.Domain.Dtos;
using LCW.Domain.Models;
using System;

namespace LCW.Services.AuthServices
{
    public class AuthManager : IAuthService
    {
        private IUserRepository _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserRepository userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now
        };
            _userService.AddAsync(user);
            return new SuccessDataResult<User>(user, "Kullanıcı eklendi.");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("Kullanıcı bulunamadı.");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Parola hatalı.");
            }

            return new SuccessDataResult<User>(userToCheck, "Giriş başarılı.");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("Kullanıcı mevcut!");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu.");
        }

        
    }
}
