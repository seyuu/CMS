using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models {

    [Table("Article")]
    public class Article : Block {

        public Article() {
            Date = DateTime.Now;
        }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Summary { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Detail { get; set; }

    }

}