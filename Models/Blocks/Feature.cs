using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Feature")]
    public partial class Feature : Block {

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }

        [MaxLength(1000)]
        public string Link { get; set; }

        public bool Icon { get; set; }

    }

}
