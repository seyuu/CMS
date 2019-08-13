using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

public class MvcApplication : System.Web.HttpApplication {

    protected void Application_Start() {


        ////CULTURE mvc binder sorun çıkarttı browser dilini baz alıcaz
        var culture = new CultureInfo("tr-TR");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        //AREAS
        AreaRegistration.RegisterAllAreas();

        //FILTERS
        GlobalFilters.Filters.Add(new HandleErrorAttribute());

        //ROUTES
        RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        RouteTable.Routes.MapMvcAttributeRoutes();

        RouteTable.Routes.MapRoute(
            name: "Default1",
            url: "{controller}/{id}",
            defaults: new {
                controller = "Default",
                action = "index",
                id = UrlParameter.Optional
            },
            constraints: new {
                id = @"\d+",
            }
        );

        RouteTable.Routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new {
                controller = "Default",
                action = "Index",
                id = UrlParameter.Optional
            }
        );

        //BLOCK

        RouteTable.Routes.MapRoute(
            name: "BlockAdd",
            url: "Admin/{controller}/Add/{SectionID}",
            defaults: new {
                action = "Add"
            }
        );

        RouteTable.Routes.MapRoute(
            name: "BlockEdit",
            url: "Admin/{controller}/Edit/{ID}",
            defaults: new {
                action = "Edit"
            }
        );

        //BLOCK ITEMS

        RouteTable.Routes.MapRoute(
            name: "Items",
            url: "Admin/{controller}/Items/{BlockId}",
            defaults: new {
                action = "Items"
            }
        );

        RouteTable.Routes.MapRoute(
            name: "ItemAdd",
            url: "Admin/{controller}/ItemAdd/{BlockId}",
            defaults: new {
                action = "ItemAdd"
            }
        );

        RouteTable.Routes.MapRoute(
            name: "ItemEdit ",
            url: "Admin/{controller}/ItemEdit/{ID}",
            defaults: new {
                action = "ItemEdit"
            }
        );

        RouteTable.Routes.MapRoute(
            name: "ItemDelete",
            url: "Admin/{controller}/ItemDelete/{ID}",
            defaults: new {
                action = "ItemDelete"
            }
        );

    }

}
