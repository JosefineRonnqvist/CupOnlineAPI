using CupOnlineAPI.Models;

namespace CupOnlineAPI.Repositories
{
    public interface ICupRepository
    {
        public Task<IEnumerable<Cup>> GetCups(int nrOfCups);
        //public IEnumerable<Cup> GetAllCups();
        public Task<IEnumerable<Cup>> GetComing(int nrOfCups);
        public Task<IEnumerable<Cup>> GetOngoing(int nrOfCups);
        public Task<IEnumerable<Cup>> GetFinished(int nrOfCups);
        public Task<IEnumerable<Cup>> Search(int nrOfCups, string name, string organizer, string place,
                                                      string sport, string year, string age);
    }
}