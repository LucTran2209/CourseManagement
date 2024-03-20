using AutoMapper;
using CMS_API.Dtos.InputDto;
using CMS_API.Dtos.OutputDto;
using CMS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class ContentCourseController : ControllerBase
    {
        public readonly PROJECT_PRN231Context _context;
        public readonly IMapper _mapper;
        public ContentCourseController(PROJECT_PRN231Context context, IMapper mapper)
        {
            _context= context;
            _mapper= mapper;
        }


        /// <summary>
        /// GetListContentByCourseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetListContentByCourseId(int courseId)
        {
            var contents = _context.CourseContents.Where(x => x.CourseId == courseId).ToList();

            var list = _mapper.Map<List<CoursesContentListOutputDto>>(contents);

            return Ok(list);
        }


        /// <summary>
        /// AddContent
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddContent(AddContentCourseInputDto inputDto)
        {
            var content = _mapper.Map<CourseContent>(inputDto);

            _context.CourseContents.Add(content);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// UpdateContent
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateContent(UpdateContentCourseInputDto inputDto)
        {
            var contentDto = _context.CourseContents.FirstOrDefault(x => x.CourseContentId == inputDto.CourseContentId);
            if (contentDto != null)
            {
                contentDto.ContentTitle = inputDto.ContentTitle;
                contentDto.CourseId = inputDto.CourseId;
                contentDto.ContentTypeId = inputDto.ContentTypeId;
                contentDto.Content = inputDto.Content;

                _context.CourseContents.Update(contentDto);
                _context.SaveChanges();
            }

            else return NotFound();

            return Ok();
        }

        /// <summary>
        /// DeleteContent
        /// </summary>
        /// <param name="courseContentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteContent(int courseContentId)
        {
            var content = _context.CourseContents.FirstOrDefault(x => x.CourseContentId == courseContentId);

            if (content != null)
            {
                _context.CourseContents.Remove(content);
                _context.SaveChanges();
            }
            else return NotFound();

            return Ok();
        }
    }
}
