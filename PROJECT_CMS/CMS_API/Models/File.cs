using System;
using System.Collections.Generic;

namespace CMS_API.Models
{
    public partial class File
    {
        public File()
        {
            PrivateFiles = new HashSet<PrivateFile>();
        }

        public int FileId { get; set; }
        public int FolderId { get; set; }
        public string Title { get; set; } = null!;
        public string ContentFile { get; set; } = null!;

        public virtual Folder Folder { get; set; } = null!;
        public virtual ICollection<PrivateFile> PrivateFiles { get; set; }
    }
}
