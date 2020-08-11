namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string url { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string position { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string img { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orders { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime created_at { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int created_by { get; set; }

        [Key]
        [Column(Order = 8)]
        public DateTime updated_at { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int updated_by { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int status { get; set; }
    }
}
