using BusinessLogic.Models;
using BusinessLogic.Models.Car;
using DataAccess.Entities;

namespace BusinessLogic.Services;

public interface ICarService : IBaseService<CarDto>
{
    Task<IQueryable<CarDto>> GetMany(int[] ids);
    Task<CarDto> SetUnactive(int id,bool active);
    Task<IQueryable<CarDto>> SearchCars(CarFilterDto filterDto);
    Task<CarDto> UpdateActivityAsync(CarDto entity);
}