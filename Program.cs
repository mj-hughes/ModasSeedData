using System;
using System.Linq;
using System.Collections.Generic;

namespace ModasSeedData
{
    class MainClass
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            const int NUM_LOCATIONS = 4;
            // first create Locations list
            List<Location> Locations = new List<Location>()
            {
                 // Add locations
                new Location {LocationId=1, Name="Front Door"},
                new Location {LocationId=2, Name="Deck" },
                new Location {LocationId=3, Name="Kitchen"},
                new Location {LocationId=4, Name="Patio Door"}
            };

            // create date object containing current date/time
            DateTime localDate = DateTime.Now;

            // subtract 6 months from date
            DateTime eventDate = localDate.AddMonths(-6);

            // instantiate Random class
            Random rnd = new Random();
            // create list to store events (Events)
            List<Event> Events = new List<Event>();

            // loop for each day in the range from 6 months ago to today
            while (eventDate <= localDate)
            {
                // a sorted list will be used to store daily events sorted by date/time - 
                // Changed: each time an event is added, the list is re-sorted
                // Now: times are sorted and events are added in time order instead of sorting list each time                
                // Events=SortList(Events);

                // random between 0 and 5 determines the number of events to occur on a given day
                int numEvents = rnd.Next(0, 6);
                int[] times = new int[numEvents];
                // for loop to generate times for each event
                for (int i=0; i<numEvents; i++)
                {
                    // random between 0 and 23 for hour of the day
                    // random between 0 and 59 for minute of the day
                    // random between 0 and 59 for seconds of the day
                    times[i] = rnd.Next(0, 24)*10000+rnd.Next(0, 60)*100+ rnd.Next(0, 60);
                }
                // Sort the times
                Array.Sort(times);
                for (int i = 0; i < numEvents; i++)
                {
                    int hr = (int)times[i] / 10000;
                    int mn = (int)((times[i]-(hr*10000)) / 100);
                    int sc = (int)times[i] % 100;
                    // create event from date/time and location AND
                    // add daily event to sorted list WITH
                    // created date/time for event
                    // random location (use Locations)
                    // EXAMPLE: new Event { TimeStamp = new DateTime(2018, 4, 19, 1, 33, 32), Flagged = false, Location = Locations.FirstOrDefault(l => l.LocationId == 1) },
                    Events.Add(new Event
                    {
                        TimeStamp = new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, hr, mn, sc),
                        Flagged = false,
                        Location = Locations[rnd.Next(0, NUM_LOCATIONS)]
                    }
                    ); 
                    
                }

                // add 1 day to eventDate
                eventDate = eventDate.AddDays(1);

            }

            // loop thru Events
            foreach (Event e in Events)
            {
                // display event at console
                Console.WriteLine("{0} - {1}", e.TimeStamp, e.Location.Name);
            }
        }

        /// <summary>
        /// sortList sorts the event list in date/time order
        /// </summary>
        /// <param name="inputList">List of events</param>
        /// <returns>Event list sorted by date/time</returns>
        public static List<Event> SortList(List<Event> inputList)
        {
            List<Event> sortedList = inputList.OrderBy(e => e.TimeStamp).ToList();
            return sortedList;
        }

    }


    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
    }

    public class Event
    {
        public int EventId { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Flagged { get; set; }
        // foreign key for location 
        public int LocationId { get; set; }
        // navigation property
        public Location Location { get; set; }
    }
}