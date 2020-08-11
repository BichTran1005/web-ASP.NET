namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int catid { get; set; }

      
        [StringLength(255)]
        public string name { get; set; }

       
        [StringLength(255)]
        public string slug { get; set; }

        
        [StringLength(100)]
        public string img { get; set; }

        [Column(TypeName = "text")]
        public string detail { get; set; }

        public int number { get; set; }

        public double price { get; set; }

        public double? pricesale { get; set; }

        public int? promo { get; set; }

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
