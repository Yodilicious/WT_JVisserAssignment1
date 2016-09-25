using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JVBusService.Models
{
    public partial class BusStop
    {
        public BusStop()
        {
            RouteStop = new HashSet<RouteStop>();
            TripStop = new HashSet<TripStop>();
        }

        [Display(Name = "Bus Stop Number")]
        public int BusStopNumber { get; set; }
        public string Location { get; set; }

        [Display(Name = "Location Hash")]
        public int LocationHash { get; set; }

        [Display(Name = "Going Downtown")]
        public bool GoingDowntown { get; set; }

        public virtual ICollection<RouteStop> RouteStop { get; set; }
        public virtual ICollection<TripStop> TripStop { get; set; }
    }
}
