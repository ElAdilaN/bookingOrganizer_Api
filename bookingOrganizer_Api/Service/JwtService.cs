using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.UTILS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bookingOrganizer_Api.Service
{
    public class JwtService
    {
        private readonly BookingContext _bookingContext;
        private readonly IConfiguration _configuration;
        public JwtService(BookingContext dbContext, IConfiguration configuration)
        {
            _bookingContext = dbContext;
            _configuration = configuration;
        }

        public async Task<LoginResponseModel?> Authenticate(User request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.PasswordHash)
                ) return null;

            var userAccount = await _bookingContext.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            if (userAccount == null)
                return null;

            bool isPasswordValid = PasswordHashHandler.VerifyPassword(request.PasswordHash, userAccount.PasswordHash);

            if (!isPasswordValid)
                return null;


            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {

                    new Claim(JwtRegisteredClaimNames.Name , request.UserName)
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature),

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToekn  = tokenHandler.WriteToken(securityToken);

            return new LoginResponseModel
            {
                AccessToken = accessToekn,
                UserName = request.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            }; 

        }
    }
}
