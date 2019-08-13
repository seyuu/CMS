using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Image")]
    public partial class Image : Block {

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

    }

}
