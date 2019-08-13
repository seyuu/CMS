using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models { 
    public partial class Setting {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string AdminKullaniciAdi { get; set; }

        [MaxLength(50)]
        public string AdminSifre { get; set; }

        [Required()]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Keywords { get; set; }

        [MaxLength(255)]
        public string SmtpServer { get; set; }

        [MaxLength(255)]
        public string SmtpEmail { get; set; }

        [MaxLength(255)]
        public string SmtpPassword { get; set; }

        [Range(0, 65535)]
        public int SmtpPort { get; set; }

        public bool SmtpSsl { get; set; }

    }
}