using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MOECSREP.POC.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOECSREP.POC.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
                
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            JWTTokenGenerator jwtTokenGen = new JWTTokenGenerator(_config);

            var user = jwtTokenGen.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = jwtTokenGen.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }        
    }
}
