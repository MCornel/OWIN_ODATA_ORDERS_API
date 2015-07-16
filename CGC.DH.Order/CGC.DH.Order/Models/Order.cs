using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CGC.DH.OrderSys.Models
{
    public class Order
    {
        [Key]
        public long OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime PickUpTime { get; set; }
        public long PaymentID { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public double Taxes { get; set; }
        public string SessionID { get; set; }
        public string VolanteTransactionID { get; set; }
        public long UnitID { get; set; }
        public string PickUpName { get; set; }
        public string MobileNumber { get; set; }
        public List<OrderItem> Items { get; set; }
    } 
}