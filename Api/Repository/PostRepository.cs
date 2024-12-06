namespace Api.Repository;

public class PostRepository(DataContext context) : BaseRepository<Post, DataContext>(context)
{
    private readonly DataContext _context = context;
    public async Task DeleteAsync(Post p)
    {
        _context.Set<Post>().Remove(p);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountUserPosts(int userId)
    {
        return await _context.Set<Post>()
            .Where(p => p.UserId == userId && p.Status)
            .CountAsync();
    }

    public async Task<List<Post>> GetUserPostPages(int page, int pageSize, int userId)
    {
        return await _context.Set<Post>()
            .OrderBy(p => p.Id)
            .Where(p => p.UserId == userId && p.Status)
            .Skip((page - 1) * page)
            .Take(pageSize)
            .ToListAsync();
    }
}