using Microsoft.EntityFrameworkCore;
using RolePlayGame.Models;

namespace RolePlayGame.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Character> Characters { get; set; }
    public DbSet<User> Users { get; set; }
}