using EMS.API.Data;
using EMS.API.DTOs;
using EMS.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMS.API.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ── Register ────────────────────────────────────────────────────────────
        public async Task<AuthResponseDto> RegisterAsync(AuthRequestDto dto)
        {
            if (_context.Users.Any(u => u.Username == dto.Username))
                return new AuthResponseDto { Success = false, Message = "Username already exists." };

            if (dto.Password.Length < 6)
                return new AuthResponseDto { Success = false, Message = "Password must be at least 6 characters." };

            var role = (dto.Role == "Admin" || dto.Role == "Viewer") ? dto.Role : "Viewer";

            var user = new AppUser
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = role,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto { Success = true, Message = "Account created successfully." };
        }

        // ── Login ───────────────────────────────────────────────────────────────
        public AuthResponseDto Login(AuthRequestDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username.ToLower() == dto.Username.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return new AuthResponseDto { Success = false, Message = "Invalid credentials." };

            return new AuthResponseDto
            {
                Success = true,
                Username = user.Username,
                Role = user.Role,
                Token = GenerateToken(user),
                Message = "Login successful."
            };
        }

        // ── Token generation ────────────────────────────────────────────────────
        public string GenerateToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,           user.Username),
                new Claim(ClaimTypes.Role,           user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var hours = double.Parse(_config["Jwt:ExpiryHours"] ?? "8");

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(hours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}