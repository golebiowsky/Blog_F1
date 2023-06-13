using Microsoft.AspNetCore.Identity;

namespace Blog_F1.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
