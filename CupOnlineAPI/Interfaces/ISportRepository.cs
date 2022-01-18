using CupOnlineAPI.Models;

namespace CupOnlineAPI.Repositories
{
    public interface ISportRepository
    {
        IEnumerable<Sport> GetAll();
    }
}