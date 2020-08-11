namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string type { get; set; }

        [StringLength(255)]
        public string link { get; set; }

        public int? tableid { get; set; }

        public int parentid { get; set; }

        public int orders { get; set; }

        [Required]
        [StringLength(255)]
        public string position { get; set; }

        public DateTime? created_at { get; set; }

        public int? created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
    }
}
