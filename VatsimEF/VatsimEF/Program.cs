using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VatsimEF.Models;

namespace VatsimEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new VatsimDbContext())
            {
                //Query and results
                var longestLoggedPilot = db.Pilots.OrderByDescending(p => p.TimeLogon).First();
                Console.WriteLine("1. Which pilot has been logged on the longest?");
                Console.WriteLine(longestLoggedPilot.ToString());

                Console.WriteLine();

                var longestLoggedController = db.Controllers.OrderByDescending(c => c.TimeLogon).First();
                Console.WriteLine("2. Which controller has been logged on the longest?");
                Console.WriteLine(longestLoggedController.ToString());

                Console.WriteLine();

                var mostDeparturesAirport = db.Flights.GroupBy(a => a.PlannedDepairport).Select(g => new
                {
                    Airport = g.Key,
                    Total = g.Count()
                })
                .OrderByDescending(item => item.Total).First();
                Console.WriteLine("3. Which airport has the most departures?");
                Console.WriteLine(mostDeparturesAirport.ToString());

                Console.WriteLine();

                var mostArrivalsAirport = db.Flights.GroupBy(a => a.PlannedDestairport).Select(g => new
                {
                    Airport = g.Key,
                    Total = g.Count()
                })
                .OrderByDescending(item => item.Total).First();
                Console.WriteLine("4. Which airport has the most arrivals?");
                Console.WriteLine(mostArrivalsAirport.ToString());

                Console.WriteLine();

                var highestAltitude = db.Positions.OrderByDescending(c => c.Altitude).First();
                Console.WriteLine("5. Who is flying at the highest altitude and what kind of plane are they flying?");
                Console.WriteLine(highestAltitude.ToStringNameAircraft());

                Console.WriteLine();

                var slowestPilot = db.Positions.OrderBy(c => c.Groundspeed).Where(x => x.Altitude != "0").First();
                Console.WriteLine("6. Who is flying the slowest?");
                Console.WriteLine(slowestPilot.ToStringNameAircraft());

                Console.WriteLine();

                var mostUsedAircraft = db.Flights.GroupBy(a => a.PlannedAircraft).Select(g => new
                {
                    Aircraft = g.Key,
                    Total = g.Count()
                }).OrderByDescending(item => item.Total).First();
                Console.WriteLine("7. Which aircraft type is being used the most?");
                Console.WriteLine(mostUsedAircraft.ToString());

                Console.WriteLine();

                var fastestPilot = db.Positions.OrderByDescending(c => c.Groundspeed).Where(x => x.Altitude != "0").First();
                Console.WriteLine("8. Who is flying the fastest?");
                Console.WriteLine(fastestPilot.ToStringNameAircraft());

                Console.WriteLine();

                var convertingLongitute = db.Flights.Select(g => new
                {
                    Longitude = Convert.ToDecimal(g.Longitude),
                    Cid = g.Cid,
                    Callsign = g.Callsign,
                    PlannedDepairport = g.PlannedDepairport,
                    PlannedDestairport = g.PlannedDestairport
                }).ToList();
                var pilotsFlyingNorth = convertingLongitute.Where(x => x.Longitude > 90 && x.Longitude < 270);
                Console.WriteLine("9. How many pilots are flying North? ");
                foreach(var flight in pilotsFlyingNorth)
                {
                    Console.WriteLine($"{flight.Cid} - {flight.Callsign} - {flight.PlannedDepairport} - {flight.PlannedDestairport}");
                }

                Console.WriteLine();

                var longestRemark = db.Flights.OrderByDescending(c => c.PlannedRemarks.Length).First();
                Console.WriteLine("10. Which pilot has the longest remarks section of their flight?");
                Console.WriteLine(longestRemark.ToStringRemark());
            }
        }
    }
}
