using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JVBusService.Models
{
    public partial class BusRoute
    {
        public BusRoute()
        {
            RouteSchedule = new HashSet<RouteSchedule>();
            RouteStop = new HashSet<RouteStop>();
        }

        [Display(Name = "Bus Route Code")]
        public string BusRouteCode { get; set; }

        [Display(Name = "Route Name")]
        public string RouteName { get; set; }

        public virtual ICollection<RouteSchedule> RouteSchedule { get; set; }
        public virtual ICollection<RouteStop> RouteStop { get; set; }
    }
}
