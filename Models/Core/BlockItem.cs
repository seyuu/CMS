using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {
    public partial class BlockItem {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("BlockId")]
        public virtual Block Block { get; set; }
        public int BlockId { get; set; }

    }
}