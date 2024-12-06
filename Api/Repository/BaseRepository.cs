namespace Api.Repository;

public class BaseRepository<E,D>(D context) where E : BaseEntity where D :DbContext
{
    private readonly D _context = context;

    /// <summary>
    /// Getting an entity using it's id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<E?> GetByIdAsync(int id)
    {
        return await _context.Set<E>().FirstOrDefaultAsync(E => E.Id == id);
    }

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    public async Task<List<E>> GetAllAsync()
    {
        return await _context.Set<E>().ToListAsync();
    }

    /// <summary>
    /// Get entities using pagination.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<List<E>> PaginteAsync(int page, int pageSize)
    {
        return await _context.Set<E>()
            .OrderBy(E => E.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Inserting a single entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<E> InsertEntity(E entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Insert a range of data
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<List<E>> InsertRangeEntity(List<E> entities)
    {
        await _context.AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        return entities;
    }

    public async Task UpdateEntity(E entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Count occurences.
    /// </summary>
    /// <returns></returns>
    public async Task<int> CountAsync()
    {
        return await _context.Set<E>().CountAsync();
    }
}