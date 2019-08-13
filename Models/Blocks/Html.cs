using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Html")]
    public partial class Html : Block {

        [MaxLength(int.MaxValue)]
        public string Content { get; set; }

    }

}
