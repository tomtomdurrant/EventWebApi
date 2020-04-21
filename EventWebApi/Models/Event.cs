using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventWebApi.Models
{
    public class Event
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public List<User> Attendees { get; set; }
        public User Host { get; set; }

    }
}
