using Microsoft.EntityFrameworkCore;

namespace MyApplication.Infrastructure.Persistents;

internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
