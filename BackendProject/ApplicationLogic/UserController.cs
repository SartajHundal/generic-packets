using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLogic.Models; // Ensure you have a User model defined
using ApplicationLogic.Services; // Ensure you have a UserService defined

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hashedPassword = HashPassword(model.Password);
            var newUser = new User
            {
                Username = model.Username,
                PasswordHash = hashedPassword,
                Email = model.Email
            };

            var result = await _userService.RegisterUser(newUser);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.FindUserByEmailAsync(model.Email);
            if (user == null || !VerifyPassword(user.PasswordHash, model.Password))
            {
                return Unauthorized();
            }

            // Generate JWT token or session cookie here
            // For simplicity, returning a success message
            return Ok(new { Message = "Logged in successfully." });
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string passwordHash, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash) == passwordHash;
            }
        }
    }
}
