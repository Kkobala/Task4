using Microsoft.AspNetCore.Identity;
using Task4.Enums;

namespace Task4.Db.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string? Name { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;
        public StatusEnum Status { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
    }
}
