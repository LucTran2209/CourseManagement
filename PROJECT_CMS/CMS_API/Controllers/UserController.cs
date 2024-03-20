using AutoMapper;
using CMS_API.Dtos.OutputDto;
using CMS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly PROJECT_PRN231Context _context;
        public readonly IMapper _mapper;

        public UserController(PROJECT_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetUser/{userId}")]
        public IActionResult GetUserById(int userId)
        {

            var user = _context.Users.Include( x => x.Role)
                                     .FirstOrDefault(x => x.UserId == userId);

            var output = _mapper.Map<UserOutputDto>(user);

            return Ok(output);
        }

    }
}
