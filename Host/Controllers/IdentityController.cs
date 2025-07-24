using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Request;
using Provider.Context;
using Services.Security;

namespace Host.Controllers
{
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private readonly StockyContext _context;

        public IdentityController(StockyContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
        {
            var hasher = new Hasher();

            if (string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest("No username provided");   
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("No password provided");   
            }
            
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == request.Username);

            if (user == null)
            {
                return NotFound("Username or password is incorrect");
            }
            
            var isValid = hasher.Verify(request.Password, user.Password);

            if (!isValid)
            {
                return NotFound("Username or password is incorrect");   
            }

            return Ok("Login successful");
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register([FromBody] User user)
        {
            var hasher = new Hasher();
            
            if (string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("Username is required");
            }
            
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Password is required");
            }
            
            var searchUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (searchUser != null)
            {
                return Conflict("Username already exists");
            }

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Code == ROLE_ENUM.USER);
            
            var newUser = new User()
            {
                UserId = Guid.NewGuid(),
                UserName = user.UserName,
                Password = hasher.Hash(user.Password),
                Active = true
            };
            
            await _context.Users.AddAsync(newUser);

            if (role != null)
            {
                var userRole = new UserRole()
                {
                    UserId = newUser.UserId,
                    RoleId = role.RoleId
                }; 
                
                await _context.UserRoles.AddAsync(userRole);
            }
            
            await _context.SaveChangesAsync();

            return Ok("Done");
        }
    }   
}