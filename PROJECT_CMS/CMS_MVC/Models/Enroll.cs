namespace CMS_MVC.Models
{
    public class Enroll
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeJoin { get; set; }
        public bool Creator { get; set; }
    }
}
