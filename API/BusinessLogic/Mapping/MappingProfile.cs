using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Models.Activity;
using BusinessLogic.Models.Borrower;
using BusinessLogic.Models.Car;
using BusinessLogic.Models.Deal;
using BusinessLogic.Models.FeedBack;
using BusinessLogic.Models.Lender;
using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.Roles;
using BusinessLogic.Models.User;
using DataAccess.Entities;

namespace BusinessLogic.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<Roles, RolesDto>();
        CreateMap<RolesDto, Roles>();
        CreateMap<Car, CarDto>();
        CreateMap<Borrower, BorrowerDto>();
        CreateMap<Lender, LenderDto>();
        CreateMap<Activity, ActivityDto>();
        CreateMap<Deal, DealDto>();
        CreateMap<FeedBack, FeedBackDto>();
        CreateMap<CarDto, Car>();
        CreateMap<BorrowerDto, Borrower>();
        CreateMap<LenderDto, Lender>();
        CreateMap<ActivityDto, Activity>();
        CreateMap<DealDto, Deal>();
        CreateMap<FeedBackDto, FeedBack>();
        CreateMap<RefreshToken, RefreshTokenDto>();
        CreateMap<RefreshTokenDto, RefreshToken>();
        
        CreateMap<IEnumerable<User>, IEnumerable<UserDto>>();
        CreateMap<IEnumerable<Roles>, IEnumerable<RolesDto>>();
        CreateMap<IEnumerable<Lender>, IEnumerable<LenderDto>>();
        CreateMap<IEnumerable<Borrower>, IEnumerable<BorrowerDto>>();
        CreateMap<IEnumerable<Activity>, IEnumerable<ActivityDto>>();
        CreateMap<IEnumerable<Deal>, IEnumerable<DealDto>>();
        CreateMap<IEnumerable<FeedBack>, IEnumerable<FeedBackDto>>();
        CreateMap<IQueryable<CarDto>, IQueryable<Car>>();
        CreateMap<IEnumerable<CarDto>, IEnumerable<Car>>();
        CreateMap<IQueryable<ActivityDto>, IQueryable<Activity>>();
        



    }
    
}