namespace WatchShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Orderdetail")]
    public partial class Orderdetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int orderid { get; set; }

        public int productid { get; set; }

        public double price { get; set; }

        public int quantity { get; set; }

        public double amount { get; set; }
    }
}
