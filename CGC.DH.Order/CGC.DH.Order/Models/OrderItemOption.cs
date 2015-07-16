using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CGC.DH.OrderSys.Models
{
    public class OrderItemOption
    {
        [Key]
        public int OptionID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double ExtendedPrice { get; set; }
        public double linkedOptionID { get; set; }
    }
}