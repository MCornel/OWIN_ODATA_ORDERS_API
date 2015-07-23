using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Data.Entity;


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

        [TestMethod]
        public async Task PatchOrderCreatedByPost_patches_order_via_test_context()
        {
            var context = new CGC.DH.Order.API.Models.TestContext();
            context.Orders = new CGC.DH.Order.API.Models.TestDbSetOrder() as DbSet<CGC.DH.Order.API.Models.Order>;

            var service = new CGC.DH.Order.API.Controllers.OrdersController(context);
            service.Request = new HttpRequestMessage();
            service.Configuration = new HttpConfiguration();

            var order = await service.Post(new CGC.DH.Order.API.Models.Order { OrderID = 1, MobileNumber = "123-123-1234", PickUpName = "ZZZ" });

            var delta = new System.Web.Http.OData.Delta<CGC.DH.Order.API.Models.Order>();
            delta.TrySetPropertyValue("PickUpName", "ChangedName");
            delta.TrySetPropertyValue("MobileNumber", "111-111-1111");
            
            var patchedOrder = await service.Patch(1, delta);
            
            Assert.AreEqual(1, context.Orders.Count());
            Assert.AreEqual("ChangedName", context.Orders.Single().PickUpName);
            Assert.AreEqual("111-111-1111", context.Orders.Single().MobileNumber);
            Assert.AreEqual(2, context.SaveChangesAsyncCount); // saved 2 changes by Post and Patch
        }

        [TestMethod]
        public async Task PatchOrder_patches_order_via_test_context()
        {
            var context = new CGC.DH.Order.API.Models.TestContext();
            context.Orders = new CGC.DH.Order.API.Models.TestDbSetOrder() as DbSet<CGC.DH.Order.API.Models.Order>;
            context.Orders.Add(new CGC.DH.Order.API.Models.Order { OrderID = 1, MobileNumber = "123-123-1234", PickUpName = "ZZZ" });
           
            var service = new CGC.DH.Order.API.Controllers.OrdersController(context);
            service.Request = new HttpRequestMessage();
            service.Configuration = new HttpConfiguration();
            
            var delta = new System.Web.Http.OData.Delta<CGC.DH.Order.API.Models.Order>();
            delta.TrySetPropertyValue("PickUpName", "ChangedName");
            delta.TrySetPropertyValue("MobileNumber", "111-111-1111");

            var patchedOrder = await service.Patch(1, delta);

            Assert.AreEqual(1, context.Orders.Count());
            Assert.AreEqual("ChangedName", context.Orders.Single().PickUpName);
            Assert.AreEqual("111-111-1111", context.Orders.Single().MobileNumber);
            Assert.AreEqual(1, context.SaveChangesAsyncCount);
        }
    } 
}
