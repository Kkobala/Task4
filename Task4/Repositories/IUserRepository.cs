using Task4.Db.Entities;

namespace Task4.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> FindByIdAsync(int id);
        Task BlockUserAsync(int userId);
        Task DeleteUserAsync(int userId);
        Task UnblockUserAsync(int userId);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task BlockAllUsersAsync();
    }
}
