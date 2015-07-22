namespace CGC.DH.Order.API.Controllers
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
            return Ok(new[] { "d", "e", "f", "g" });
        }
    }
}
