namespace CMS_API.Dtos.OutputDto
{
    public class CoursesContentListOutputDto
    {
        public int CourseContentId { get; set; }
        public int CourseId { get; set; }
        public int ContentTypeId { get; set; }
        public string ContentTitle { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime? TimeUpLoad { get; set; }
    }
}
