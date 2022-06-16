using LCW.Core.Results;
using LCW.Core.Security.JWT;
using LCW.Domain.Dtos;
using LCW.Domain.Models;

namespace LCW.Core.Repositories
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
