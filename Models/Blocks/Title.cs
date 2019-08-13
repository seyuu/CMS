using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Title")]
    public partial class Title : Block {

        [MaxLength(255)]
        public string Content { get; set; }
    }

}
