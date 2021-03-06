using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.DTOs.Auth;
using Boilerplate.Application.DTOs.User;
using UserEntity = Boilerplate.Domain.Entities.User;
using Newtonsoft.Json.Linq;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISession = Boilerplate.Domain.Auth.Interfaces.ISession;
using Microsoft.AspNetCore.Cors;

namespace Boilerplate.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ISession _session;

        public UserController(IUserService userService, IAuthService authService, ISession session)
        {
            _userService = userService;
            _authService = authService;
            _session = session;
        }

        /// <summary>
        /// Authenticates the user and returns the token information.
        /// </summary>
        /// <param name="loginInfo">Email and password information</param>
        /// <returns>Token information</returns>
        [EnableCors]
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JwtDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginInfo)
        {
            var user = await _userService.Authenticate(loginInfo.Email, loginInfo.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
     
            return Ok(_authService.GenerateToken(user));
        }

        /// <summary>
        /// User authenticated with JWT can update password
        /// </summary>
        /// <param name="dto">New password (complex password between 8 and 20 char)</param>
        /// <returns>No content</returns>
        [EnableCors]
        [HttpPatch("update-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
        {            
            await _userService.UpdatePassword(_session.UserId, dto);
            return NoContent();
        }
    }
}
