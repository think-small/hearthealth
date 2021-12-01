using HeartHealth.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace HeartHealth.Application.Contracts.Persistence
{
    public interface IHistoriesRepository
    {
        Task<History> GetBetweenAsync(DateTime start, DateTime end);
        Task SaveAsync(History history);
    }
}
