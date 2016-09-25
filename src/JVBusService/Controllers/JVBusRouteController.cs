/* JVBusRouteController.cs
 * Controller Class for a Bus Route
 *
 *  Revision History
 * Jodi Visser, 2016.09.19: Created
 *
*/

using JVBusService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace JVBusService.Controllers
{
    //Controller that creates, reads, updates and deletes bus routes
    public class JVBusRouteController : Controller
    {
        //Database context for the Bus Service Database
        private BusServiceContext context;

        //Constructor for the Bus Route Controller
        public JVBusRouteController (BusServiceContext context)
        {
            this.context = context;
        }

        //This method returns a list of the Bus Routes
        public IActionResult Index()
        {
            var busRoutes = from route in context.BusRoute select route;

            return View(busRoutes.ToList());
        }

        //This method displays a specific Bus Route
        public ActionResult Details(string id)
        {
            BusRoute busRoute = context.BusRoute.FirstOrDefault(b => b.BusRouteCode == id);

            return View(busRoute);
        }

        //This method returns an empty Create View
        public ActionResult Create()
        {
            return View();
        }

        //This method takes the View Model and Adds it to the Bus Route Database and creates the Hash
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BusRoute route)
        {
            if (ModelState.IsValid)
            {
                context.BusRoute.Add(route);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(route);
        }

        //This Method finds the Bus route with the requested id and returns an Edit view with the Bus route model data
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var busRoute = context.BusRoute.SingleOrDefault(m => m.BusRouteCode == id);

            if (busRoute == null)
            {
                return NotFound();
            }

            return View(busRoute);
        }

        //This method takes the Bus route model and updates the model and hash in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("BusRouteCode,RouteName")] BusRoute route)
        {
            if (id != route.BusRouteCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                context.Update(route);
                context.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(route);
        }

        //This method displays Bus Route information to delete as confirmation
        public ActionResult DeleteConfirm(string id)
        {
            BusRoute busRoute = context.BusRoute.FirstOrDefault(b => b.BusRouteCode == id);

            return View(busRoute);
        }

        //This method deletes the selected Bus Stop from the Database
        public ActionResult Delete(string id)
        {
            BusRoute busRoute = context.BusRoute.FirstOrDefault(b => b.BusRouteCode == id);

            context.BusRoute.Remove(busRoute);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
