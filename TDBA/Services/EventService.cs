using Microsoft.EntityFrameworkCore;
using TDBA.Data;

namespace TDBA.Services
{
    // This class is responsible for handling all event-related operations. does not deal with UI, weather, or external API
    public class EventService
    {
        private readonly AppDbContext _context;

        //relying on AppDbContext to handle database operations via Dependency Injection
        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventModel>> GetAllEventsAsync()
        {
            return await _context.Events
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        public async Task AddEvent(EventModel newEvent) //using async with await to handle DB operations while keeping the UI responsive
        {
            Console.WriteLine($"Adding event: {newEvent.Name}, Age Group: {newEvent.AgeGroup}, Price: {newEvent.PriceRange}, Indoor: {newEvent.IsIndoor}");

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            Console.WriteLine("Event saved to database.");
        }

        public List<EventModel> GetWeatherSmartEvents(string weatherCondition, double temperatureF)
        {
            if (weatherCondition == "Rain" || weatherCondition == "Snow" || temperatureF < 50) //tested with 70
            {
                // Only show indoor events if nasty out
                return _context.Events.Where(e => e.IsIndoor).ToList();
            }

            // Otherwise, show all events
            return _context.Events.ToList();
        }
    }
}