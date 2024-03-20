using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class Folder
    {
        public Folder()
        {
            Files = new HashSet<File>();
        }

        public int FolderId { get; set; }
        public string Title { get; set; } = null!;
        public int CourseContentId { get; set; }

        public virtual CourseContent CourseContent { get; set; } = null!;
        public virtual ICollection<File> Files { get; set; }
    }
}
