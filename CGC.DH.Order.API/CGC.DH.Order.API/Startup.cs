using Microsoft.Owin;
using CGC.DH.Order.API;

[assembly: OwinStartup(typeof(Startup))]
namespace CGC.DH.Order.API
{
    using System.Web.Http;
    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;

    //using System.Linq;
    //using System.Web.OData.Batch;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    //using System.Web.Http.Dispatcher;
    using CGC.DH.Order.API.Models;
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            httpConfiguration.AddODataQueryFilter();

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            //httpConfiguration.MapHttpAttributeRoutes();
            //httpConfiguration.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    //routeTemplate: "api/{controller}/{id}",
            //    routeTemplate: "api/{namespace}/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new VersionHttpControllerSelector(httpConfiguration));

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            //var route = httpConfiguration.Routes.Where(r => r is System.Web.Http.OData.Routing.ODataRoute).First();
            //var odataRoute = route as System.Web.Http.OData.Routing.ODataRoute;

            //httpConfiguration.MapODataServiceRoute(
            //    routeName: "OrdersRoute",
            //    routePrefix: "odata",
            //    model: builder.GetEdmModel(),
            //    pathHandler: odataRoute.Constraints.PathHandler,
            //    routingConventions: odataRoute.PathRouteConstraint.RoutingConventions);

            builder.EntitySet<Order>("Orders");
            builder.EntitySet<OrderItem>("OrderItems");
            builder.EntitySet<OrderItemOption>("OrderItemOptions");

            // mock the route prefix to api/v1 until the proper versioning is implemented
            httpConfiguration.MapODataServiceRoute("OData", "api/v1", builder.GetEdmModel()); //, new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            
            app.UseWebApi(httpConfiguration);

            httpConfiguration.EnsureInitialized();

            // Make ./public the default root of the static files in our Web Application.
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./public"),
                EnableDirectoryBrowsing = true,
            });

            app.UseStageMarker(PipelineStage.MapHandler);            
        }
    }
}
