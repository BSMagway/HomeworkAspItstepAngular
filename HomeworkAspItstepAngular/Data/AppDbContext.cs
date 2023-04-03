
using HomeworkAspItstepAngular.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeworkAspItstepAngular.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Notepad> Notepads { get; set; }
    public DbSet<Note> Notes { get; set; }

    //public DbSet<Note> Notes { get; set; }
}