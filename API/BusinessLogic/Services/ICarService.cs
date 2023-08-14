using BusinessLogic.Models;
using BusinessLogic.Models.Car;
using DataAccess.Entities;

namespace BusinessLogic.Services;

public interface ICarService : IBaseService<CarDto>
{
    Task<IQueryable<CarDto>> GetMany(int[] ids);
    public Task<IQueryable<Car>> SearchCars(CarFilterDto filterDto);
}