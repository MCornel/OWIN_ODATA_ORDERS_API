namespace CGC.DH.OrderSys.Controllers
{
    using System.Web.Http;

    // Test controller
    public class ValuesController : ApiController
    {
        public IHttpActionResult GetValues()
        {
            return Ok(new[] { "a", "b", "c" });
        }
    
        [HttpPost]
        public IHttpActionResult CreateValue()
        {
            return Ok(new[] { "a", "b", "c", "d" });
        }
    }
}
