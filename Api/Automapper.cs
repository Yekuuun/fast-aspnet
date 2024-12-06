using AutoMapper;

namespace Api.Mapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        //SOURCE -> DESTINATION
        _ = CreateMap<User, UserDto>();
        _ = CreateMap<AddUserDto, User>();

        _ = CreateMap<Post, PostDto>();
    }
}