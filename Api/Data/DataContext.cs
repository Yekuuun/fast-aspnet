namespace Api.Data;

/// <summary>
/// Models Configuration.
/// </summary>
/// <param name="options"></param>
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
    }
}