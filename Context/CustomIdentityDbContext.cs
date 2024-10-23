using CustomIdentity.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomIdentity.Context
{
    public class CustomIdentityDbContext : DbContext
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> 
            options) : base(options)
        {
        
        }


        public DbSet<UserEntity> Users => Set<UserEntity>();    


    }
}
