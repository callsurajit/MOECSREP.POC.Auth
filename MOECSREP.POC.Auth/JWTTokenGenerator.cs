using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MOECSREP.POC.Auth.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MOECSREP.POC.Auth
{
    public class JWTTokenGenerator
    {
        private IConfiguration _config;

        public JWTTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: new List<Claim>(), // claims (are used to filter the data)
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: signCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            // Validate the User Credentials using Azure AD
            //if (login.Username == "admin")
            //{
                user = new UserModel { Username = "pauls3@michigan.gov", Password = "jeet123" };
            //}
            return user;
        }
    }
}
