using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task4.Auth;
using Task4.Db;
using Task4.Db.Entities;
using Task4.Repositories;
using Task4.Requests;

namespace Task4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly AppDbContext _db;
        private readonly IUserRepository _userRepository;

        public LoginController(
            TokenGenerator tokenGenerator,
            UserManager<UserEntity> userManager,
            AppDbContext db,
            IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _db = db;
            _userRepository = userRepository;
        }

        [HttpPost("admin-login")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return NotFound("Admin not found");
            }

            var isCoorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isCoorrectPassword)
            {
                return BadRequest("Invalid Password or Email");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }

        [Authorize(Policy = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var entity = new UserEntity
            {
                UserName = request.Email,
                Name = request.Name,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(entity, request.Password!);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                return BadRequest(firstError.Description);
            }

            await _userManager.AddToRoleAsync(entity, "user");
            await _db.SaveChangesAsync();

            return Ok(request);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password!);

            if (!isCorrectPassword)
            {
                return BadRequest("Invalid email or password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_tokenGenerator.Generate(user.Id.ToString(), roles));
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("select-non-current-user")]
        public async Task<IActionResult> SelectUser(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);

            return Ok(user);
        }

        [HttpPost("block-user")]
        public async Task<IActionResult> BlockUser(int id)
        {
            await _userRepository.BlockUserAsync(id);
            return Ok("User Is Blocked Successfully");
        }

        [HttpPost("unblock-user")]
        public async Task<IActionResult> UnblockUser(int id)
        {
            await _userRepository.UnblockUserAsync(id);
            return Ok("User Is Unblocked Successfully");
        }

        [HttpPost("block-all-users")]
        public async Task<IActionResult> BlockAllUsers()
        {
            await _userRepository.BlockAllUsersAsync();
            return Ok("All Users Are Blocked Successfully");
        }

        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return Ok("User is Deleted");
        }
    }
}
