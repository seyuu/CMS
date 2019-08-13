using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Service")]
    public partial class Service : Block {
    }

    [Table("ServiceItem")]
    public partial class ServiceItem : BlockItem {

        [MaxLength(255)]
        public string Image { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Content { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

    }

}