using AutoMapper;
using CMS_API.Dtos.InputDto;
using CMS_API.Dtos.OutputDto;
using CMS_API.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        public readonly PROJECT_PRN231Context _context;
        public readonly IMapper _mapper;
        public CourseController(PROJECT_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// GetAllCourse
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("GetAllMyCourseCreate/{id}")]
        public IActionResult GetAllMyCourseCreate(int id)
        {

            var x = from course in _context.Courses
                    join userjoin in _context.UserJoinCourses on course.CourseId equals userjoin.CourseId
                    join category in _context.Categories on course.CategoryId equals category.CategoryId
                    where userjoin.UserId == id
                    select new CourseDtoList
                    {
                        CourseId = course.CourseId,
                        CourseTitle = course.CourseTitle,
                        Semester = course.Semester,
                        TimeStart = course.TimeStart,
                        TimeEnd = course.TimeEnd,
                        CategoryId = course.CategoryId,
                        CategoryTitle = category.CategoryName,
                    };

            var courses = x.ToList();
            foreach (var item in courses)
            {
                item.Teachers = new List<string>();
                int userId = _context.UserJoinCourses.SingleOrDefault(x => x.CourseId == item.CourseId && x.Crerator == true).UserId;

                var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
                item.CreatorName = user.FirstName + " " + user.LastName;

                var term = _context.UserJoinCourses.Where(x => x.CourseId == item.CourseId).ToList();
                foreach (var item1 in term)
                {
                    var u = _context.Users.FirstOrDefault(x => x.UserId == item1.UserId && x.UserId != userId);
                    if (u != null)
                    {
                        item.Teachers.Add(u.FirstName + " " + u.LastName);
                    }
                }

            }

            return Ok(courses);
        }

        /// <summary>
        /// GetAllCourse
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("GetAllCourse")]
        public IActionResult GetAllCourse(string? title)
        {

            var x = from course in _context.Courses
                    join category in _context.Categories on course.CategoryId equals category.CategoryId
                    where course.CourseTitle.Contains(title != null ? title : string.Empty)
                    select new CourseDtoList
                    {
                        CourseId = course.CourseId,
                        CourseTitle = course.CourseTitle,
                        Semester = course.Semester,
                        TimeStart = course.TimeStart,
                        TimeEnd = course.TimeEnd,
                        CategoryId = course.CategoryId,
                        CategoryTitle = category.CategoryName,
                    };

            var courses = x.ToList();
            foreach (var item in courses)
            {
                item.Teachers = new List<string>();
                int userId = _context.UserJoinCourses.SingleOrDefault(x => x.CourseId == item.CourseId && x.Crerator == true).UserId;

                var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
                item.CreatorName = user.FirstName + " " + user.LastName;

                var term = _context.UserJoinCourses.Where(x => x.CourseId == item.CourseId).ToList();
                foreach (var item1 in term)
                {
                    var u = _context.Users.FirstOrDefault(x => x.UserId == item1.UserId && x.UserId != userId);
                    if (u != null)
                    {
                        item.Teachers.Add(u.FirstName + " " + u.LastName);
                    }
                }

            }

            return Ok(courses);
        }

        /// <summary>
        /// GetCourseById
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("GetCourseById/{courseId}")]
        public IActionResult GetCourseById(int courseId)
        {
            var x = from course in _context.Courses
                    join category in _context.Categories on course.CategoryId equals category.CategoryId
                    where course.CourseId == courseId
                    select new CourseDtoList
                    {
                        CourseId = course.CourseId,
                        CourseTitle = course.CourseTitle,
                        Semester = course.Semester,
                        TimeStart = course.TimeStart,
                        TimeEnd = course.TimeEnd,
                        CategoryId = course.CategoryId,
                        CategoryTitle = category.CategoryName,
                    };

            var courses = x.ToList();

            foreach (var item in courses)
            {
                item.Teachers = new List<string>();
                int userId = _context.UserJoinCourses.SingleOrDefault(x => x.CourseId == item.CourseId && x.Crerator == true).UserId;
                item.CreatorName = _context.Users.FirstOrDefault(x => x.UserId == userId).FirstName;

                var term = _context.UserJoinCourses.Where(x => x.CourseId == item.CourseId).ToList();
                foreach (var item1 in term)
                {
                    var u = _context.Users.FirstOrDefault(x => x.UserId == item1.UserId && x.UserId != userId);
                    if (u != null)
                    {
                        item.Teachers.Add(u.FirstName + " " + u.LastName);
                    }
                }
            }
            return Ok(courses);
        }

        /// <summary>
        /// EnrollCourse
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("enrollcourse")]
        public IActionResult EnrollCourse([FromBody] EnrrolCourseInputDto inputDto)
        {
            var joinCourse = _mapper.Map<UserJoinCourse>(inputDto);
            _context.UserJoinCourses.Add(joinCourse);
            _context.SaveChanges();
            return Ok("Enroll Success!");
        }

        /// <summary>
        /// GetCourseByCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("GetCourseByCategory")]
        public IActionResult GetCourseByCategory(int categoryId)
        {
            var x = from course in _context.Courses
                    join category in _context.Categories on course.CategoryId equals category.CategoryId
                    where category.CategoryId == categoryId
                    select new CourseDtoList
                    {
                        CourseId = course.CourseId,
                        CourseTitle = course.CourseTitle,
                        Semester = course.Semester,
                        TimeStart = course.TimeStart,
                        TimeEnd = course.TimeEnd,
                        CategoryId = course.CategoryId,
                        CategoryTitle = category.CategoryName,
                    };

            var courses = x.ToList();
            foreach (var item in courses)
            {
                item.Teachers = new List<string>();
                int userId = _context.UserJoinCourses.SingleOrDefault(x => x.CourseId == item.CourseId && x.Crerator == true).UserId;
                item.CreatorName = _context.Users.FirstOrDefault(x => x.UserId == userId).FirstName;

                var term = _context.UserJoinCourses.Where(x => x.CourseId == item.CourseId).ToList();
                foreach (var item1 in term)
                {
                    var u = _context.Users.FirstOrDefault(x => x.UserId == item1.UserId && x.UserId != userId);
                    if (u != null)
                    {
                        item.Teachers.Add(u.FirstName + " " + u.LastName);
                    }
                }

            }

            return Ok(courses);
        }

        [HttpPost]
        public IActionResult Create(CreateNewCourse createNewCourse)
        {
            try
            {
                var course = _mapper.Map<Course>(createNewCourse);
                _context.Courses.Add(course);
                _context.SaveChanges();

                var list = _context.Courses.ToList();
                
                var joinCourse = _mapper.Map<UserJoinCourse>(createNewCourse);
                joinCourse.Crerator = true;
                joinCourse.CourseId = list.Last().CourseId;
                _context.UserJoinCourses.Add(joinCourse);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
                throw;
            }
            
        }


    }
}
