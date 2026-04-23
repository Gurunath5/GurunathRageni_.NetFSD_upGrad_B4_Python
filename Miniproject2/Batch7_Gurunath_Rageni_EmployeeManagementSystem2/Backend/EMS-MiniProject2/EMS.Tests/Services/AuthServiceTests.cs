using EMS.API.Data;
using EMS.API.DTOs;
using EMS.API.Models;
using EMS.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace EMS.Tests.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AppDbContext _db;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _db = new AppDbContext(options);

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["Jwt:Key"]).Returns("TestSecretKey_32Chars_ForNUnit!!");
            mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("EMS.API");
            mockConfig.Setup(c => c["Jwt:Audience"]).Returns("EMS.Client");
            mockConfig.Setup(c => c["Jwt:ExpiryHours"]).Returns("8");

            _authService = new AuthService(_db, mockConfig.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _db.Database.EnsureDeleted();
            _db.Dispose();
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            _db.Users.Add(new AppUser
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin"
            });
            await _db.SaveChangesAsync();

            var result = _authService.Login(new AuthRequestDto
            {
                Username = "admin",
                Password = "admin123"
            });

            Assert.That(result.Success, Is.True);
            Assert.That(result.Token, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task Login_WrongPassword_ReturnsFailure()
        {
            _db.Users.Add(new AppUser
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin"
            });
            await _db.SaveChangesAsync();

            var result = _authService.Login(new AuthRequestDto
            {
                Username = "admin",
                Password = "wrongpassword"
            });

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Invalid credentials."));
        }

        [Test]
        public async Task RegisterAsync_DuplicateUsername_ReturnsFailure()
        {
            _db.Users.Add(new AppUser
            {
                Username = "existinguser",
                PasswordHash = "hash",
                Role = "Viewer"
            });
            await _db.SaveChangesAsync();

            var result = await _authService.RegisterAsync(new AuthRequestDto
            {
                Username = "existinguser",
                Password = "newpassword123"
            });

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Username already exists."));
        }
    }
}