using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class CourseContent
    {
        public CourseContent()
        {
            Folders = new HashSet<Folder>();
        }

        public int CourseContentId { get; set; }
        public int CourseId { get; set; }
        public int ContentTypeId { get; set; }
        public string ContentTitle { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime? TimeUpLoad { get; set; }

        public virtual ContentType ContentType { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Folder> Folders { get; set; }
    }
}
