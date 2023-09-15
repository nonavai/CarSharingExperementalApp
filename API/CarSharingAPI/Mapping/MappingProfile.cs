
using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Models.Activity;
using BusinessLogic.Models.Borrower;
using BusinessLogic.Models.Car;
using BusinessLogic.Models.Deal;
using BusinessLogic.Models.Lender;
using BusinessLogic.Models.User;
using CarSharingAPI.Requests;
using CarSharingAPI.Requests.Activity;
using CarSharingAPI.Requests.Borrower;
using CarSharingAPI.Requests.Car;
using CarSharingAPI.Requests.Deal;
using CarSharingAPI.Requests.Lender;
using CarSharingAPI.Requests.User;
using CarSharingAPI.Responses;
using CarSharingAPI.Responses.Deal;
using DataAccess.Entities;

namespace CarSharingAPI.Mapping;

public class MappingProfileApi : Profile
{
    public MappingProfileApi()
    {
        CreateMap<UserRequest, UserDto>();
        CreateMap<CreateCarRequest, CarDto>();
        CreateMap<BorrowerRequest, BorrowerDto>();
        CreateMap<LenderRequest, LenderDto>();
        CreateMap<ActivityGeoRequest, ActivityDtoGeo>();
        CreateMap<CarRequest, CarDto>();
        CreateMap<CreateUserRequest, UserDto>();
        CreateMap<ActivityRequest, ActivityDto>();
        CreateMap<CreateDealRequest, DealDto>();
        CreateMap<LogInRequest, UserDto>();
        CreateMap<SearchCarRequest, CarFilterDto>();
        CreateMap<UserDto, LogInResponse>();
        
        CreateMap<UserDto, LogInResponse>();
        CreateMap<UserDto, UserResponse>();
        CreateMap<CarDto, CarResponse>();
        CreateMap<BorrowerDto, BorrowerResponse>();
        CreateMap<LenderDto, LenderResponse>();
        CreateMap<ActivityDto, ActivityResponse>();
        
        CreateMap<IEnumerable<UserResponse>, IEnumerable<UserDto>>();
        CreateMap<IEnumerable<LenderResponse>, IEnumerable<LenderDto>>();
        CreateMap<IEnumerable<BorrowerResponse>, IEnumerable<BorrowerDto>>();
        CreateMap<IEnumerable<ActivityResponse>, IEnumerable<ActivityDto>>();
        CreateMap<IEnumerable<CreateDealResponse>, IEnumerable<DealDto>>();
        CreateMap<IQueryable<CarDto>, IQueryable<CarResponse>>();
        CreateMap<IEnumerable<CarDto>, IEnumerable<CarResponse>>();
        CreateMap<IQueryable<ActivityDto>, IQueryable<ActivityResponse>>();
        


    }
}