using Boilerplate.Domain.Entities;
using System;
using System.Threading.Tasks;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.DTOs.User;

namespace Boilerplate.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string email, string password);
        Task<GetUserDto> UpdatePassword(Guid id, UpdatePasswordDto dto);
    }
}
