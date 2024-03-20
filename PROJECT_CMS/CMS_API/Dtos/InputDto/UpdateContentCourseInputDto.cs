namespace CMS_API.Dtos.InputDto
{
    public class UpdateContentCourseInputDto
    {
        public int CourseContentId { get; set; }
        public int CourseId { get; set; }
        public int ContentTypeId { get; set; }
        public string ContentTitle { get; set; } = null!;
        public string? Content { get; set; }
    }
}
