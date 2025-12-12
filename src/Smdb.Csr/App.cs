namespace Smdb.Csr;

using System.Collections;
using System.Net;
using Shared.Http;
public class App : HttpServer
{
       public App()
    {
        
    }
    public override void Init()
    {
        router.Use(HttpUtils.StructuredLogging);
        router.Use(HttpUtils.CentralizedErrorHandling);
        router.Use(HttpUtils.AddResponseCorsHeaders);
        router.Use(HttpUtils.ParseRequestUrl);
        router.Use(HttpUtils.ParseRequestQueryString);
        router.UseSimpleRouteMatching(); // Move this BEFORE ServeStaticFiles
        router.Use(HttpUtils.ServeStaticFiles);
        router.Use(HttpUtils.DefaultResponse);

        router.MapGet("/", LandingPageIndexRedirect);
        router.MapGet("/movies", MoviesPageIndexRedirect);
    }

    public async Task LandingPageIndexRedirect( HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        res.Redirect("/index.html");
        await next();
    }

    public async Task MoviesPageIndexRedirect( HttpListenerRequest req, HttpListenerResponse res, Hashtable props, Func<Task> next)
    {
        res.Redirect("/movies/index.html");
        await next();
    }
}
