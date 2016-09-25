/* JVBusStopController.cs
 * Controller Class for a Bus Stop
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
    //Controller that creates, reads, updates and deletes bus stops 
    public class JVBusStopController : Controller
    {
        //Database context for the Bus Service Database
        private BusServiceContext context;

        //Constructor for the Bus Stop Controller
        public JVBusStopController(BusServiceContext context)
        {
            this.context = context;
        }

        //This method returns a list of the Bus Stops
        public IActionResult Index()
        {
            var busStops = from stop in context.BusStop select stop;

            return View(busStops.ToList());
        }

        //This method displays a specific Bus Stop 
        public ActionResult Details(int id)
        {
            BusStop busStop = context.BusStop.FirstOrDefault(b => b.BusStopNumber == id);

            return View(busStop);
        }

        //This method returns an empty Create View
        public ActionResult Create()
        {
            return View();
        }

        //This method takes the View Model and Adds it to the Bus Stop Database and creates the Hash
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BusStop stop)
        {
            if (ModelState.IsValid)
            {
                stop.LocationHash = stop.Location.Select(r => (int)r).ToArray().Sum();

                context.BusStop.Add(stop);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(stop);
        }

        //This Method finds the Bus stop with the requested id and returns an Edit view with the Bus Stop model data
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BusStop = context.BusStop.SingleOrDefault(m => m.BusStopNumber == id);

            if (BusStop == null)
            {
                return NotFound();
            }

            return View(BusStop);
        }

        //This method takes the Bus Stop model and updates the model and hash in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BusStopNumber,Location,LocationHash,GoingDowntown")] BusStop stop)
        {
            if (id != stop.BusStopNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                stop.LocationHash = stop.Location.Select(r => (int)r).ToArray().Sum();

                context.Update(stop);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(stop);
        }

        //This method displays Bus Stop information to delete as confirmation
        public ActionResult DeleteConfirm(int id)
        {
            BusStop BusStop = context.BusStop.FirstOrDefault(b => b.BusStopNumber == id);

            return View(BusStop);
        }

        //This method deletes the selected Bus Stop from the Database
        public ActionResult Delete(int id)
        {
            BusStop BusStop = context.BusStop.FirstOrDefault(b => b.BusStopNumber == id);

            context.BusStop.Remove(BusStop);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
