using AutoMapper;
using PassengerTimeTracker.Data.EF.Entities;
using PassengerTimeTracker.Entities.DTOs;

namespace PassengerTimeTracker.API.AutoMapper
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<TripDTO, Trip>().ReverseMap();
        }
    }
}
