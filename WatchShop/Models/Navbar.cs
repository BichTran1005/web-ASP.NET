namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Navbar")]
    public partial class Navbar
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string type { get; set; }

        [StringLength(255)]
        public string link { get; set; }

        public int? tableid { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int parentid { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orders { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string position { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime created_at { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int created_by { get; set; }

        [Key]
        [Column(Order = 7)]
        public DateTime updated_at { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int updated_by { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int status { get; set; }
    }
}
