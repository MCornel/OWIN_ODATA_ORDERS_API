using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using CGC.DH.Order.API.Models;

namespace CGC.DH.Order.API.Controllers
{    
    public class OrdersController : ODataController
    {
        
        private IOrderContext db;

        public OrdersController()
        {
            db = new OrderEDMX();
        }

        public OrdersController(IOrderContext context)
        {
            db = context;
        }

        // GET: odata/Orders
        [EnableQuery]
        public IQueryable<CGC.DH.Order.API.Models.Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: odata/Orders(5)
        [EnableQuery]
        public SingleResult<CGC.DH.Order.API.Models.Order> GetOrder([FromODataUri] long key)
        {
            return SingleResult.Create(db.Orders.Where(order => order.OrderID == key));
        }

        // PUT: odata/Orders(5)
        public async Task<IHttpActionResult> Put([FromODataUri] long key, Delta<CGC.DH.Order.API.Models.Order> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CGC.DH.Order.API.Models.Order order = await db.Orders.FindAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            //patch.Put(order);
            patch.Patch(order);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(order);
        }

        // POST: odata/Orders
        public async Task<IHttpActionResult> Post(CGC.DH.Order.API.Models.Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return Created(order);
        }

        // PATCH: odata/Orders(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] long key, Delta<CGC.DH.Order.API.Models.Order> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CGC.DH.Order.API.Models.Order order = await db.Orders.FindAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            patch.Patch(order);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(order);
        }

        // DELETE: odata/Orders(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] long key)
        {
            CGC.DH.Order.API.Models.Order order = await db.Orders.FindAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Orders(5)/Items
        [EnableQuery]
        public IQueryable<OrderItem> GetItems([FromODataUri] long key)
        {
            return db.Orders.Where(m => m.OrderID == key).SelectMany(m => m.Items);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(long key)
        {
            return db.Orders.Count(e => e.OrderID == key) > 0;
        }
    }
}
