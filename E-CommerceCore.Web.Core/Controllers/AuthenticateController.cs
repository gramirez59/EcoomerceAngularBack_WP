using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using E_CommerceCore.Core.Domain.Entities.Security;
using System.Net;

namespace E_CommerceCore.Web.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticateController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var result = await this._signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            //if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            if (user != null && result.Succeeded)
            {
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"));

                var token = new JwtSecurityToken(
                    issuer: "http://dotnetdetail.net",
                    audience: "http://dotnetdetail.net",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("logout")]
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            var user = await this._userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                user = new User
                {
                    Email = model.Username,
                    UserName = model.Username,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await this._userManager.CreateAsync(user, model.Password);
                if (result != IdentityResult.Success)
                {
                    string resultErrors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        resultErrors += (!string.IsNullOrEmpty(resultErrors) ? " - " : string.Empty) + error.Description;
                    }
                    return BadRequest("The user couldn't be created. " + resultErrors);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Created);
                }
            }

            return BadRequest("The username is already registered.");
        }
    }
}