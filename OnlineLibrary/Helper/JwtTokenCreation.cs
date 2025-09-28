using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineLibrary.Dto;
using OnlineLibrary.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineLibrary.Helper
{

    public class JwtTokenCreation
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtTokenCreation(IConfiguration configuration, UserManager<ApplicationUser> user)
        {
            _configuration = configuration;
            _userManager = user;
        }

        public async Task<string> CreateTokenAsync(Customer customer)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Get ApplicationUser from Customer.UserId
            var user = await _userManager.FindByIdAsync(customer.UserId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.Name),
                new Claim(ClaimTypes.Email, customer.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (user != null)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

