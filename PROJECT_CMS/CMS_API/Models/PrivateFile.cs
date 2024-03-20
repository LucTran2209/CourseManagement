using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class PrivateFile
    {
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public virtual File File { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
