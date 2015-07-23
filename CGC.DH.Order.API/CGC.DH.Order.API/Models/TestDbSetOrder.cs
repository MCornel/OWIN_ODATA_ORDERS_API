//using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Threading.Tasks; 

namespace CGC.DH.Order.API.Models
{
    public class TestDbSetOrder : TestDbSet<Order>
    {
        public override Order Find(params object[] keyValues)
        {
            var id = (long)keyValues.Single();
            return this.SingleOrDefault(b => b.OrderID == id);
        }

        public override Task<Order> FindAsync(params object[] keyValues)
        {
            var id = (long)keyValues.Single();
            return Task.FromResult(this.SingleOrDefault(b => b.OrderID == id));
        }
    } 
}