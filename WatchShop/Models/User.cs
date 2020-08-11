namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string fullname { get; set; }

        [Required]
        [StringLength(225)]
        public string username { get; set; }

        [Required]
        [StringLength(64)]
        public string password { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        public int gender { get; set; }

        [Required]
        [StringLength(15)]
        public string phone { get; set; }

        [StringLength(100)]
        public string img { get; set; }

        public int access { get; set; }

        public DateTime? created_at { get; set; }

        public int? created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
    }
}
