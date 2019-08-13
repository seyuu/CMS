using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {
    public partial class Section {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int No { get; set; }
        public string Title { get; set; }
        public bool Invert { get; set; }
        public bool Full { get; set; }
        public string Container { get; set; }

        [ForeignKey("PageId")]
        public virtual Page Page { get; set; }
        public int PageId { get; set; }

        public virtual ICollection<Block> Block { get; set; }

    }
}