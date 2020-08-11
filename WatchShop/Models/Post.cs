namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? topid { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

       
        [StringLength(255)]
        public string slug { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string detai { get; set; }

        [StringLength(255)]
        public string img { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [Required]
        [StringLength(150)]
        public string metakey { get; set; }

        [Required]
        [StringLength(150)]
        public string metadesc { get; set; }

        public DateTime? created_at { get; set; }

        public int? created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
    }
}
