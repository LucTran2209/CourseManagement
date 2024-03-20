using AutoMapper;
using CMS_API.Dtos.OutputDto;
using CMS_API.Models;
using CMS_API.Models.RequestModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        public readonly PROJECT_PRN231Context _context;
        public readonly IMapper _mapper;
        public AuthenController(PROJECT_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("forbindden")]
        public IActionResult GetForbindden()
        {
            return Forbid();
        }

        [HttpPost("login-cookie")]
        public async Task<IActionResult> LoginCookie([FromBody] UserRequestModel userRequestModel)
        {
            // Check User
            // Get role

            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.Role, "Guest"),
                new Claim("FullName", "Tran Dinh Luc")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            //Có thể add thên thuộc tính authen vào
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,

                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                IsPersistent = true,

            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                authProperties);

            return Ok("Login Success with cookies");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("Logout Success with cookies");
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet("login/{email}/{pass}")] 
        public IActionResult Login(string email, string pass)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(pass));
            if (user != null)
            {
                return Ok(_mapper.Map<UserOutputDto>(user));
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// CheckEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("CheckEmail")]
        public IActionResult CheckEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.Equals(email) );
            if (user != null)
            {
                return Ok("Email đã được sử dụng!");
            }
            else
            {
                return Ok("Passed");
            }
        }
    }


}
