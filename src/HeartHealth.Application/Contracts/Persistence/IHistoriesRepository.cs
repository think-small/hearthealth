using HeartHealth.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace HeartHealth.Application.Contracts.Persistence
{
    public interface IHistoriesRepository
    {
        Task<History> GetBetween(DateTime start, DateTime end);
        Task Save(History history);
    }
}
