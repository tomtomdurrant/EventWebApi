using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace EventWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly EventsContext _context;

        public EventsController(ILogger<EventsController> logger, EventsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Event> allEvents = new List<Event>();
            MySqlConnection conn = _context.GetConnection();
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = @"SELECT Events.Name AS EventName, A.Name AS AddressName FROM bootcamp9.Events
                               INNER JOIN Users U
                               ON Events.HostID = U.Id
                               INNER JOIN Addresses A
                               ON Events.AddressID = A.Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();
                

                while (rdr.Read())
                {
                    //Console.WriteLine(rdr["Name"] + " --- " + rdr["HeadOfState"]);
                    Event dbEvent = new Event();
                    //dbEvent.ID = int.Parse(rdr["Id"].ToString());
                    dbEvent.Name = rdr["EventName"].ToString();
                    //dbEvent.DateTime = DateTime.Parse(rdr["DateTime"].ToString());
                    dbEvent.Address = new Address() {Name = rdr["AddressName"].ToString(), }; 
                    dbEvent.Host = new User();
                    allEvents.Add(dbEvent);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");

            return Ok(allEvents);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Event newEvent)
        {
            MySqlConnection conn = _context.GetConnection();
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql =
                    $@"INSERT INTO bootcamp9.Events(Events.Name, Events.HostId, Events.AddressId)
VALUES (@EventName, @HostId, @AddressId);";


                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("EventName", newEvent.Name);
                cmd.Parameters.AddWithValue("HostId", newEvent.Host.ID);
                cmd.Parameters.AddWithValue("AddressId", newEvent.Address.ID);


                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();

            }
            conn.Close();

            return Ok();
        }
    }
}
