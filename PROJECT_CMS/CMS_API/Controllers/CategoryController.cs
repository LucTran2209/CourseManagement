using CMS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly PROJECT_PRN231Context _context;
        public CategoryController(PROJECT_PRN231Context context)
        {
            _context= context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok( _context.Categories.ToList() );
        }
    }

}
