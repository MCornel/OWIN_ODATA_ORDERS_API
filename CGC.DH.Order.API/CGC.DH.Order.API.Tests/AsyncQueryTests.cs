using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;


namespace CGC.DH.Order.API.Tests
{
    [TestClass]
    public class AsyncQueryTests
    {
        [TestMethod]
        public async Task CreateOrder_saves_an_order_via_test_context()
        {
            var context = new CGC.DH.Order.API.Models.TestContext();

            var service = new CGC.DH.Order.API.Controllers.OrdersController(context);
            var order = await service.Post(new CGC.DH.Order.API.Models.Order { MobileNumber = "123-123-1234", PickUpName = "ZZZ" });

            Assert.AreEqual(1, context.Orders.Count());
            Assert.AreEqual("ZZZ", context.Orders.Single().PickUpName);
            Assert.AreEqual("123-123-1234", context.Orders.Single().MobileNumber);
            Assert.AreEqual(1, context.SaveChangesAsyncCount);
        }
    } 
}
