using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicLibrary.Infrastructure.Entities;
using MusicLibrary.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.Now.AddHours(2),
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                };
                var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                {
                    ModelState.AddModelError("username", "User already exists!");
                    return BadRequest(ModelState);
                }
                
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again.");

                return Ok("User created successfully!");
            }
            catch (Exception ex)
            {
                //will log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error!");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] UserRoleModel model)
        {
            try
            {           
                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Role already exists!");

                return Ok("Role created successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("MapUserRole")]
        public async Task<IActionResult> MapUserRole([FromBody] UserMapRoleModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists == null)
                    return NotFound();
                
                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _userManager.AddToRoleAsync(userExists, model.Role);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Role doesn't exists!");
                }
                return Ok("Role assigned successfully!");
            }
            catch (Exception ex)
            {
                //will log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error!");
            }
        }
    }
}
