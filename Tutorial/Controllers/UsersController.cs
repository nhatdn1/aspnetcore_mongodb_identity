using AutoMapper;
using IntergrateMongodb.Dtos;
using IntergrateMongodb.Models;
using IntergrateMongodb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IntergrateMongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UsersController(IMapper mapper, 
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("get_users", Name = nameof(GetAll))] 
        public async Task<ActionResult<List<AppUser>>> GetAll()
        {
            var users = await Task.FromResult(_userManager.Users.ToList());
            return Ok(users);
        }

        [HttpGet]
        [Route("get_user/{id}", Name = nameof(GetUser))] 
        public async Task<ActionResult<AppUser>> GetUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound(IdentityResult.Failed(new IdentityError { Description = "User not found" }));
            return Ok(user);
        }

        [HttpPost]
        [Route("create_user", Name = nameof(CreateUser))]
        [AllowAnonymous]
        public async Task<ActionResult<IdentityResult>> CreateUser([FromBody] CreateUserDto request)
        {
            var user = _mapper.Map<AppUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return CreatedAtRoute(nameof(GetUser), new { id = user.Id }, result);
            }
            return Conflict(result);
        }

        [HttpPost]
        [Route("login_user", Name = nameof(LoginUser))]
        [AllowAnonymous]
        public async Task<ActionResult<IdentityResult>> LoginUser([FromBody] LoginUserDto request)
        {
            var existUser = await _userManager.FindByNameAsync(request.UserName);
            if (existUser == null) return NotFound(IdentityResult.Failed(new IdentityError { Description = "User not found" }));
            var result = await _signInManager.PasswordSignInAsync(existUser, request.Password, false, false);

            if ((result.Succeeded))
            {
                return Ok(IdentityResult.Success);
            } 
            return BadRequest(IdentityResult.Failed(UsersService.GetSignInErrors(result).ToArray()));
        }
         
    }
}
