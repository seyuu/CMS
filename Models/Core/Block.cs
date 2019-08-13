using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    public abstract partial class Block {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int No { get; set; }
        public int Width { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
        public int SectionId { get; set; }

        public virtual ICollection<BlockItem> BlockItem { get; set; }

    }

}