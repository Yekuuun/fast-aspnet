namespace Api.Data;

/// <summary>
/// Models Configuration.
/// </summary>
/// <param name="options"></param>
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        
        modelBuilder.Entity<Post>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Post>()
            .HasOne(s => s.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(s => s.UserId);
    }
}