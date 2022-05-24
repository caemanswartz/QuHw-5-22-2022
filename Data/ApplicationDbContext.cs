using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuintrixHomeworkPlayerMVP.Models;

namespace QuintrixHomeworkPlayerMVP.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<QuintrixHomeworkPlayerMVP.Models.Player>? Player { get; set; }
}
