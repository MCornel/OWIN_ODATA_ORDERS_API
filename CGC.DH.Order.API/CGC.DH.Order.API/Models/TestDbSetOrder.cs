using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGC.DH.Order.API.Models
{
    public class TestDbSetOrder : TestDbSet<Order>
    {
        public override Order Find(params object[] keyValues)
        {
            var id = (int)keyValues.Single();
            return this.SingleOrDefault(b => b.OrderID == id);
        }
    } 
}