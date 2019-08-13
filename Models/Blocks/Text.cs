using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Text")]
    public partial class Text : Block {

        [MaxLength(10000)]
        public string Content { get; set; }
    }

}
