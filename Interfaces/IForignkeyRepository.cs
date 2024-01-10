namespace Raythos.Interfaces
{
    public interface IForignkeyRepository
    {
        Task<bool> IsUserExists(long id);

        Task<bool> IsAddressExists(long id);

        Task<bool> IsAircraftExists(long id);

        Task<bool> IsCartExists(long id);

        Task<bool> IsCartExists(long userId, long aircraftId);

        Task<bool> IsTeamExists(long id);

        Task<bool> IsCategoryExists(int id);

        Task<bool> IsCategoryExists(string slug);
    }
}
