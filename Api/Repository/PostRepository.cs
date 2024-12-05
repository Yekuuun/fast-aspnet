namespace Api.Repository;

public class PostRepository(DataContext context) : BaseRepository<Post, DataContext>(context)
{
    
}