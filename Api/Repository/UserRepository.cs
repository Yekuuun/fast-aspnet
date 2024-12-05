namespace Api.Repository;

public class UserRepository(DataContext context) : BaseRepository<User, DataContext>(context)
{
    
}