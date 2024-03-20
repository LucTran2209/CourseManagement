using CMS_API.Models;

namespace CMS_API.Dtos.OutputDto
{
    public class CourseOutputDto
    {
        public List<CourseDtoList>? ListCourse;
    }

    public class CourseDtoList
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }

        // Category
        public int CategoryId { get; set; }
        public string? CategoryTitle { get; set; }

        // User
        public string? CreatorName { get; set; }

        public List<string>? Teachers { get; set; }
        
    }
}
