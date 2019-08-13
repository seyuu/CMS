using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Gallery")]
    public partial class Gallery : Block {
    }

    [Table("GalleryItem")]
    public partial class GalleryItem : BlockItem {

        [MaxLength(255)]
        public string Image { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Category { get; set; }
    }

}