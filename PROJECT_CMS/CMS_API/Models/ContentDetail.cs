using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class ContentDetail
    {
        public int ContentDetailId { get; set; }
        public int CourseContentId { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public string? TimeUpLoad { get; set; }
    }
}
