# Freelancing
Web Application for freelancing created using C# in .net framework.
https://www.tutorialspoint.com/asp.net_mvc/asp.net_mvc_databases.htm
https://www.tutorialsteacher.com/mvc/routing-in-mvc

the freelancing project had two users: client and freelancer. A client can create contests and freelancer and view and submit solution for particular contests. 
When client click on create contest, get request is sent to controller and it displays create contest page. Once the details about contest are filled in, post request is sent to create method of contest controller and creates a contest object, fetches the client id from session and saves the data from request body in the model and redirects to contests of client 
.
ViewBag is a dynamic object to pass the data from Controller to View. And, this will pass the data as a property of object ViewBag.
Public ActionResult Index()  
{  
    ViewBag.Title = “Welcome”;  
    return View();  
}  
<h2>@ViewBag.Title</h2>


ViewData is a dictionary object to pass the data from Controller to View where data is passed in the form of key-value pair.
Public ActionResult Index()  
{  
    ViewData[”Title”] = “Welcome”;  
    return View();  
}  
<h2>@ViewData[“Title”]</h2>  


TempData is a dictionary object to pass the data from one action to other action in the same Controller or different Controllers. Usually, TempData object will be stored in a session object. Tempdata is also required to typecast and for null checking before reading data from it. TempData scope is limited to the next request and if we want Tempdata to be available even further, we should use Keep and peek.
 
Ex - Controller
Public ActionResult Index()  
{  
    TempData[”Data”] = “I am from Index action”;  
    return View();  
}  

Public string Get()  
{  
    return TempData[”Data”] ;  
}  



Difference between mvc and mvt?
in MVC, we have to wrute all the controller specific code, but in mvt, the controller part is taken care by the framework.
MVT is loosely coupled and easy to modify.
in mvc, view is controlled by model and controller. in mvt, view cobtrols the model. Advantage of mvc: SEPERATION OF CONCERNS(SOC)


Routing in routeconfig.cs
register in global.asax
RouteConfig.RegisterRoutes(RouteTable.Routes);

Scaffolding is an automatic code generation framework for ASP.NET web applications. Scaffolding reduces the time taken to develop a controller, view etc. in MVC framework. You can develop a customized scaffolding template using T4 templates as per your architecture and coding standard.

action method must be public, can not be static cannot be overloaded.
