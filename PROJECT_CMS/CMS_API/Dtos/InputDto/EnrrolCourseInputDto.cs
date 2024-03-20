namespace CMS_API.Dtos.InputDto
{
    public class EnrrolCourseInputDto
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeJoin { get; set; }
        public bool Creator { get; set; } 
    }
}
 