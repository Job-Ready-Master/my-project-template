using Microsoft.EntityFrameworkCore;
using MyApplication.Domain;

namespace MyApplication.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{

    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
