using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class User
    {
        public User()
        {
            PrivateFiles = new HashSet<PrivateFile>();
            UserJoinCourses = new HashSet<UserJoinCourse>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? Country { get; set; }
        public string? Description { get; set; }
        public int RoleId { get; set; }
        public string? GoogleId { get; set; }
        public string? Password { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<PrivateFile> PrivateFiles { get; set; }
        public virtual ICollection<UserJoinCourse> UserJoinCourses { get; set; }
    }
}
