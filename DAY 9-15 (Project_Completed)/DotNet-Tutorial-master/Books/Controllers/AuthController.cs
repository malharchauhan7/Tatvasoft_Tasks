using Books.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Books.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly BooksContext _context;

        public AuthController(IConfiguration config, BooksContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(new { Message = "Login Successfully", Token = token });
            }

            return NotFound("User not found");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.EmailAddress.ToLower() == model.EmailAddress.ToLower());

            if (existingUser != null)
            {
                return BadRequest("Email address already exists");
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                UserType = model.UserType,
                Password = model.Password,
                UserFullName = $"{model.FirstName} {model.LastName}"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.EmailAddress,
                    UserType = u.UserType
                })
                .ToList();

            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.EmailAddress == forgotPassword.EmailAddress);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Generate a reset token and update the ForgotPassword table
            var resetToken = GenerateResetToken();
            _context.ForgotPasswords.Add(new ForgotPassword
            {
                Id = resetToken,
                UserId = user.Id,
                RequestDateTime = DateTime.UtcNow
            });
            _context.SaveChanges();

            // Send the reset token to the user's email address
            // ...

            return Ok("Password reset link has been sent to your email address.");
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPassword resetPassword)
        {
            var forgotPassword = _context.ForgotPasswords.FirstOrDefault(fp => fp.Id == resetPassword.ResetToken);

            if (forgotPassword == null)
            {
                return NotFound("Invalid reset token");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == forgotPassword.UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update the user's password
            user.Password = resetPassword.NewPassword;
            _context.Users.Update(user);
            _context.SaveChanges();

            // Remove the reset token from the ForgotPassword table
            _context.ForgotPasswords.Remove(forgotPassword);
            _context.SaveChanges();

            return Ok("Password reset successful");
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePassword changePassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == changePassword.UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Verify the old password
            if (user.Password != changePassword.OldPassword)
            {
                return BadRequest("Invalid old password");
            }

            // Update the user's password
            user.Password = changePassword.NewPassword;
            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok("Password changed successfully");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim("userId", user.Id.ToString()),
        new Claim("fullName", user.UserFullName),
        new Claim("firstName", user.FirstName),
        new Claim("lastName", user.LastName),
        new Claim("phoneNumber", user.PhoneNumber),
        new Claim("emailAddress", user.EmailAddress),
        new Claim("userType", user.UserType),
        new Claim("userImage", user.UserImage)
    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLogin userLogin)
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.EmailAddress.ToLower() == userLogin.EmailAddress.ToLower() && u.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }

        private string GenerateResetToken()
        {
            // Generate a random token for password reset
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var resetToken = new byte[32];
                randomNumberGenerator.GetBytes(resetToken);
                return Convert.ToBase64String(resetToken);
            }
        }
    }
}