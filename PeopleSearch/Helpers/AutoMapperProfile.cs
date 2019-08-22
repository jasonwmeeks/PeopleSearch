using PeopleSearch.DTO;
using PeopleSearch.Models;
using AutoMapper;

namespace PeopleSearch.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddPersonDto, Person>();
            CreateMap<EditPersonDto, Person>();
            CreateMap<Person, PersonDto>();
        }
    }
}
