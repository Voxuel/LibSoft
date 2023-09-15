using AutoMapper;
using LibSoft_Models;

namespace LibSoft_API.MappingProfiles;

public class BookMappingConfig : Profile
{
    public BookMappingConfig()
    {
        CreateMap<Book,BookDTO>().ReverseMap();
        CreateMap<Book,BookCreateDTO>().ReverseMap();
        CreateMap<Book, BookUpdateDTO>().ReverseMap();
        CreateMap<BookDTO,Book>().ForAllMembers(b =>
            b.MapFrom(d => d));
    }
}