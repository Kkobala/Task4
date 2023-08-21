using Microsoft.EntityFrameworkCore;
using Task4.Db;
using Task4.Db.Entities;
using Task4.Enums;

namespace Task4.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<UserEntity> FindByIdAsync(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new ArgumentException("Cannot Find User");

            return user;
        }

        public async Task BlockUserAsync(int userId)
        {
            var user = await FindByIdAsync(userId);
            user.Status = StatusEnum.Blocked;
            await _db.SaveChangesAsync();
        }
        
        public async Task UnblockUserAsync(int userId)
        {
            var user = await FindByIdAsync(userId);
            user.Status = StatusEnum.Active;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await FindByIdAsync(userId);
            user.Status = StatusEnum.Blocked;
            user.IsDeleted = true;
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }


        public async Task BlockAllUsersAsync()
        {
            var allUsers = await _db.Users.ToListAsync();
            foreach (var user in allUsers)
            {
                user.Status = StatusEnum.Blocked;
            }
            await _db.SaveChangesAsync();
        }
    }
}
