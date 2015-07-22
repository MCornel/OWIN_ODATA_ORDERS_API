using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CGC.DH.Order.API.Tests
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void GetOrders_OrderBy_PickUpName_via_test_context()
        {
            var context = new CGC.DH.Order.API.Models.TestContext();
            context.Orders.Add(new CGC.DH.Order.API.Models.Order { PickUpName = "DDD" });
            context.Orders.Add(new CGC.DH.Order.API.Models.Order { PickUpName = "ZZZ" });
            context.Orders.Add(new CGC.DH.Order.API.Models.Order { PickUpName = "CCC" });
          

            var service = new CGC.DH.Order.API.Controllers.OrdersController(context);
            var orders = service.GetOrders().OrderBy(o => o.PickUpName);

            Assert.AreEqual(3, orders.Count());
            Assert.AreEqual("CCC", orders.FirstOrDefault().PickUpName);
            Assert.AreEqual("ZZZ", orders.LastOrDefault().PickUpName);            
        }
    } 
}
