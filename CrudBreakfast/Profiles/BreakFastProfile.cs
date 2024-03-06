using AutoMapper;
using CrudBreakfast.Contracts.BreakFast;
using CrudBreakfast.Models;

namespace CrudBreakfast.Profiles
{
    public class BreakFastProfile : Profile
    {
        public BreakFastProfile()
        {
            // for creating a new breakfast
            CreateMap<BreakFast, CreateBreakFastReq>().ReverseMap();
            // for updating a breakfast
            CreateMap<BreakFast, UpsertBreakFastReq>().ReverseMap();
        }
    }
}