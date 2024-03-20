namespace CMS_API.Dtos.InputDto
{
    public class CreateNewCourse
    {
        public int UserId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string? Status { get; set; }
        public int CategoryId { get; set; }
    }
}
