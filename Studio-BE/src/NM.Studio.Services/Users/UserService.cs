﻿using AutoMapper;


using NM.Studio.Domain.Contracts.Repositories.Users;
using NM.Studio.Domain.Contracts.Services.Users;
using NM.Studio.Domain.Contracts.UnitOfWorks;
using NM.Studio.Domain.CQRS.Commands.Users;
using NM.Studio.Domain.CQRS.Queries.Users;
using NM.Studio.Domain.Entities.Users;
using NM.Studio.Domain.Models.Messages;
using NM.Studio.Domain.Models.Users;
using NM.Studio.Domain.Results.Messages;
using NM.Studio.Domain.Results.Users;
using NM.Studio.Domain.Utilities;
using NM.Studio.Services.Bases;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NM.Studio.Services.Users
{
    using BCrypt.Net;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;

        private DateTime countDown = DateTime.Now.AddMinutes(30);

        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration) : base(mapper, unitOfWork)
        {
            _userRepository = unitOfWork.UserRepository;
            _configuration = configuration;
        }

        public async Task<MessageLoginResult<UserResult>> Login(AuthQuery x, CancellationToken cancellationToken = default)
        {
            // check username or email
            User user = await _userRepository.FindUsernameOrEmail(x);
            UserResult userResult = new UserResult();
            
            if (user == null)
            {
                return AppMessage.GetMessageLoginResult(userResult, null, null);
            }

            // check password
            bool isPasswordValid = BCrypt.Verify(x.Password, user.Password);
            if (!isPasswordValid)
            {
                // Nếu mật khẩu không khớp, trả lại thông tin không hợp lệ
                return AppMessage.GetMessageLoginResult(userResult, null, null);
            }

            userResult = _mapper.Map<User, UserResult>(user);
            JwtSecurityToken token = CreateToken(user);
            var msgResult = AppMessage.GetMessageLoginResult(userResult,
                new JwtSecurityTokenHandler().WriteToken(token), 
                token.ValidTo.ToString());

            return msgResult;
        }

        public async Task<MessageView<UserView>> Register(UserCreateCommand x, CancellationToken cancellationToken = default)
        {
            x.Password = BCrypt.HashPassword(x.Password);
            return await this.CreateOrUpdate(x);
        }

        private JwtSecurityToken CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
            };
            // Conditional addition of claim based on function result
            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Appsettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: countDown
                );

            return token;
        }
    }
}
