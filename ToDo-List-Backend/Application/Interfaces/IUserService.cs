using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.User;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponseDto<UserDto>> RegisterUser(UserCreateDto userCreateDto);
        Task<ApiResponseDto<UserDto>> LoginUser(UserLoginDto userLoginDto);
    }
}
