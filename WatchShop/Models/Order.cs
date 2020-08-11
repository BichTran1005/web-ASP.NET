namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? code { get; set; }

        public int? userid { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_ate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? exportdate { get; set; }

        [Required]
        [StringLength(255)]
        public string deliveryaddress { get; set; }

        [Required]
        [StringLength(100)]
        public string deliveryname { get; set; }

        [Required]
        [StringLength(255)]
        public string deliveryphone { get; set; }

        [Required]
        [StringLength(255)]
        public string deliveryemail { get; set; }

        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
    }
}
