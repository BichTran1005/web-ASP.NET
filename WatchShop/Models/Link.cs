using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WatchShop.Models
{
    [Table("Link")]
    public class Link
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string slug { get; set; }
        public int tableId { get; set; }
        [Required]
        public string types { get; set; }
    }
}