using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class UserJoinCourse
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime TimeJoin { get; set; }
        public string? Status { get; set; }
        public bool Crerator { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
