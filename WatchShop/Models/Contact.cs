namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage ="Họ tên không được để trống.")]
        [StringLength(100)]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [StringLength(100)]
        public string email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(15)]
        public string phone { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        [StringLength(255)]
        public string title { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống.")]
        [StringLength(1)]
        public string detail { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
    }
}
