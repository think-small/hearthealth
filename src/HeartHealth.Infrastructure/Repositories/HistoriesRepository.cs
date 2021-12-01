using HeartHealth.Application.Contracts.Persistence;
using HeartHealth.Domain.Entities;
using HeartHealth.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeartHealth.Infrastructure.Repositories
{
    public class HistoriesRepository : IHistoriesRepository
    {
        private readonly HeartHealthDbContext _context;
        public HistoriesRepository(HeartHealthDbContext context)
        {
            _context = context;
        }

        public async Task<History> GetBetweenAsync(DateTime start, DateTime end)
        {
            var dateRange = new DateRange(start, end);
            var measurements = await _context.Measurements
                .Where(m => m.Timestamp >= start && m.Timestamp <= end)
                .ToListAsync();

            return new History(start, end, measurements);
        }

        public async Task SaveAsync(History history)
        {
            foreach (var measurement in history.Measurements)
            {
                if (measurement.Id == default(Guid))
                {
                    _context.Measurements.Add(measurement);
                }
                else
                {
                    _context.Measurements.Update(measurement);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
