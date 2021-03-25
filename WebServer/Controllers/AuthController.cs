using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Data;
using WebServer.Data.Users;
using WebServer.Models;
using WebServer.Models.Users;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            ApplicationDbContext context,
            ITokenBuilder tokenBuilder, ILogger<AuthenticationController> logger)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var dbUser = await _context
                .Users
                .SingleOrDefaultAsync(u => u.Email == user.Email);

            if (dbUser == null)
            {
                return NotFound("User not found.");
            }

            // This is just an example, made for simplicity; do not store plain passwords in the database
            // Always hash and salt your passwords
            var isValid = dbUser.Password == user.Password;

            if (!isValid)
            {
                return BadRequest("Could not authenticate user.");
            }

            var token = _tokenBuilder.BuildToken(user.Email);

            return Ok(token);
        }

        [HttpGet("verify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> VerifyToken()
        {
            var email = User
                .Claims
                .SingleOrDefault();

            if (email == null)
            {
                return Unauthorized();
            }

            var userExists = await _context
                .Users
                .AnyAsync(u => u.Email == email.Value);

            if (!userExists)
            {
                return Unauthorized();
            }

            return Ok();
        }

    }
}
