using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class ContentType
    {
        public ContentType()
        {
            CourseContents = new HashSet<CourseContent>();
        }

        public int ContentTypeId { get; set; }
        public string ContentTypeName { get; set; } = null!;

        public virtual ICollection<CourseContent> CourseContents { get; set; }
    }
}
