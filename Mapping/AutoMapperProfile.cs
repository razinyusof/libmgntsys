using AutoMapper;
using LibraryManagementApi.DTOs.Books;
using LibraryManagementApi.DTOs.Members;
using LibraryManagementApi.Entities;

namespace LibraryManagementApi.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateBookRequest, Book>();
        CreateMap<UpdateBookRequest, Book>();
        CreateMap<Book, BookResponse>();

        CreateMap<CreateMemberRequest, Member>();
        CreateMap<Member, MemberResponse>();
    }
}
