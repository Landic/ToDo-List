using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using BCrypt.Net;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uof;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork uof, IMapper mapper)
        {
            this.uof = uof;
            this.mapper = mapper;
        }
        public async Task<ApiResponseDto<UserDto>> LoginUser(UserLoginDto userLoginDto)
        {
            try
            {
                var user = (await uof.Users.FindAsync(i => i.Email == userLoginDto.Email)).FirstOrDefault();
                if (user == null)
                    return ApiResponseDto<UserDto>.FailureResult("Invalid email or password");
                if(!VerifyPassword(userLoginDto.Password, user.Password_hash))
                    return ApiResponseDto<UserDto>.FailureResult("Invalid email or password");
                var userDto = mapper.Map<UserDto>(userLoginDto);
                return ApiResponseDto<UserDto>.SuccessResult(userDto, "Login successful");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<UserDto>.FailureResult("Error login user", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<UserDto>> RegisterUser(UserCreateDto userCreateDto)
        {
            try
            {
                var existinguser = (await uof.Users.FindAsync(i => i.Email == userCreateDto.Email)).FirstOrDefault();
                if (existinguser != null)
                    return ApiResponseDto<UserDto>.FailureResult("Email already in use");
                var user = mapper.Map<User>(userCreateDto);
                user.Password_hash = HashPassword(userCreateDto.Password);

                await uof.Users.AddAsync(user);
                await uof.CompleteAsync();

                var userDto = mapper.Map<UserDto>(user);
                return ApiResponseDto<UserDto>.SuccessResult(userDto, "User registered successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<UserDto>.FailureResult("Error login user", new List<string> { ex.Message });
            }
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
