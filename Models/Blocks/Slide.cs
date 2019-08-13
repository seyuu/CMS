using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Slide")]
    public partial class Slide : Block {
    }

    [Table("SlideItem")]
    public partial class SlideItem : BlockItem {

        [MaxLength(255)]
        public string Image { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }

}