using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.DTOs.User;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Auth;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }        

        public async Task<User> Authenticate(string email, string password)
        {
            await Task.Delay(1);

            var allUsers = _userRepository.GetAll();
            var user = allUsers.Find(i => i.Email == email);

            if (user == null || !BC.Verify(password, user.Password)){
                return null;  
            } 

            return user;
        }

        public async Task<GetUserDto> UpdatePassword(Guid id, UpdatePasswordDto dto)
        {
            await Task.Delay(1);

            var allUsers = _userRepository.GetAll();
            var originalUser = allUsers.Find(i => i.Id == id);

            if (originalUser == null) return null;

            originalUser.Password = BC.HashPassword(dto.Password);
            _userRepository.Update(originalUser);

            return _mapper.Map<GetUserDto>(originalUser);
        }
    }
}
