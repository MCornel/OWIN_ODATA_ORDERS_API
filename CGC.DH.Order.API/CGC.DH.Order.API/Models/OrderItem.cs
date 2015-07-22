using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CGC.DH.Order.API.Models
{
    public class OrderItem
    {
        [Key]
        public int ItemID { get; set; }
        public int GroupID { get; set; }
        public int CategoryID { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double ExtendedPrice { get; set; }
        public virtual List<OrderItemOption> itemOptions { get; set; }
    }
}