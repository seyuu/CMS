using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Form")]
    public partial class Form : Block {

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Subject { get; set; }

    }

    [Table("FormItem")]
    public partial class FormItem : BlockItem {

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Type { get; set; }

        public bool Required { get; set; }

    }

}
