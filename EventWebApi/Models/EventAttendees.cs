using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventWebApi.Models
{
    public class EventAttendees
    {
        public int ID { get; set; }
        public IList<User> Attendees { get; set; }
        public Event Event { get; set; }

    }
}
