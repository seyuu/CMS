using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models { 

    public partial class MenuItem {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int No { get; set; }
        public string Title { get; set; }
        public bool Blank { get; set; }
        public string Url { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
        public int MenuId { get; set; }

    }

}