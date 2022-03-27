using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.DTOs.User;
using Boilerplate.Application.Extensions;
using Boilerplate.Application.Filters;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Auth;
using Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }        

        public async Task<User> Authenticate(string email, string password)
        {
            await Task.Delay(1);
            var store = new List<User>
            {
                new User() { Email = "romane.thu@gmail.com", Password = BC.HashPassword("Password123!"), Role = "User"},
            };

            var user = store.Find(i => i.Email == email);
           
            if (user == null || !BC.Verify(password, user.Password))
            {
                return null;
            }
            return user;
        }

        public async Task<GetUserDto> UpdatePassword(Guid id, UpdatePasswordDto dto)
        {
            await Task.Delay(1);
            // var originalUser = await _userRepository.GetById(id);
            // if (originalUser == null) return null;

            // originalUser.Password = BC.HashPassword(dto.Password);
            // _userRepository.Update(originalUser);
            // await _userRepository.SaveChangesAsync();
            // // return _mapper.Map<GetUserDto>(originalUser);

            var user = new User {
                    Email = "romane.thu@gmail.com",
                    Password = "Password123!",
                    Role = "User"
                };
            return _mapper.Map<GetUserDto>(user);
        }
    }
}
