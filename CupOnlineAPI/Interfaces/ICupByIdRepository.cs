using CupOnlineAPI.Models;

namespace CupOnlineAPI.Interfaces
{
    public interface ICupByIdRepository
    {
        CupById GetCupById(int id);
    }
}
