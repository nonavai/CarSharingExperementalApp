
using AutoMapper;
using BusinessLogic.Models.Borrower;
using BusinessLogic.Models.Car;
using BusinessLogic.Models.Deal;
using BusinessLogic.Models.Lender;
using BusinessLogic.Models.User;

using CarSharingAPI.Requests.Borrower;
using CarSharingAPI.Requests.Car;
using CarSharingAPI.Requests.Deal;
using CarSharingAPI.Requests.Lender;
using CarSharingAPI.Requests.User;
using CarSharingAPI.Responses;
using CarSharingAPI.Responses.Deal;

namespace CarSharingAPI.Mapping;

public class MappingProfileApi : Profile
{
    public MappingProfileApi()
    {
        CreateMap<UserRequest, UserDto>();
        CreateMap<CreateCarRequest, CarDto>();
        CreateMap<BorrowerRequest, BorrowerDto>();
        CreateMap<LenderRequest, LenderDto>();
        CreateMap<CarRequest, CarDto>();
        CreateMap<CreateUserRequest, UserDto>();
        CreateMap<CreateDealRequest, DealDto>();
        CreateMap<LogInRequest, UserDto>();
        CreateMap<SearchCarRequest, CarFilterDto>();
        CreateMap<UserDto, LogInResponse>();
        
        CreateMap<UserDto, LogInResponse>();
        CreateMap<UserDto, UserResponse>();
        CreateMap<CarDto, CarResponse>();
        CreateMap<BorrowerDto, BorrowerResponse>();
        CreateMap<LenderDto, LenderResponse>();
        CreateMap<DealDto, DealResponse>();
        CreateMap<DealDto, CreateDealResponse>();
        

        CreateMap<IEnumerable<UserResponse>, IEnumerable<UserDto>>();
        CreateMap<IEnumerable<LenderResponse>, IEnumerable<LenderDto>>();
        CreateMap<IEnumerable<BorrowerResponse>, IEnumerable<BorrowerDto>>();
        CreateMap<IEnumerable<CreateDealResponse>, IEnumerable<DealDto>>();
        CreateMap<IQueryable<CarDto>, IQueryable<CarResponse>>();
        CreateMap<IEnumerable<CarDto>, IEnumerable<CarResponse>>();



    }
}