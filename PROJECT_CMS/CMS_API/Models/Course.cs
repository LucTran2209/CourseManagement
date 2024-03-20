using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseContents = new HashSet<CourseContent>();
            UserJoinCourses = new HashSet<UserJoinCourse>();
        }

        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string? Status { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<CourseContent> CourseContents { get; set; }
        public virtual ICollection<UserJoinCourse> UserJoinCourses { get; set; }
    }
}
