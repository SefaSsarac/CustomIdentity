using CustomIdentity.Dtos;
using CustomIdentity.Types;

namespace CustomIdentity.Services
{
    public interface IUserSevices
    {



        Task<ServiceMessage> AddUser(AddUserDto user);

        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user);
    }
}
