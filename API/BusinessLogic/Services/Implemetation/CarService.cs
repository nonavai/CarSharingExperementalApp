using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Models.Car;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Implemetation;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public CarService(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<CarDto> GetByIdAsync(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
        {
            throw new Exception("Car not found");
        }
        var carDto = _mapper.Map<CarDto>( car);
        return carDto;
    }

    public async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        var carDto = _mapper.Map<IEnumerable<CarDto>>( await _carRepository.GetAllAsync());
        return carDto;
    }

    public async Task<CarDto> AddAsync(CarDto entity)
    {
        var car = _mapper.Map<Car>(entity);
        var carDto = _mapper.Map<CarDto>( await _carRepository.AddAsync(car));
        return carDto;
    }

    public async Task<CarDto> UpdateAsync(CarDto entity)
    {
        
        var existingCar = await _carRepository.GetByIdAsync(entity.Id);
        if (existingCar == null)
        {
            throw new Exception("Car not found");
        }
        existingCar.Color = entity.Color;
        existingCar.Price = entity.Price;
        existingCar.RegistrationNumber = entity.RegistrationNumber;

        var carDto = _mapper.Map<CarDto>( await _carRepository.UpdateAsync(existingCar));
        return carDto;
    }

    
    public async Task<CarDto> DeleteAsync(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
        {
            throw new ArgumentException("Car not found");
        }
        
        var carDto = _mapper.Map<CarDto>( await _carRepository.DeleteAsync(id));
        return carDto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _carRepository.ExistsAsync(id);
    }

    public async Task<IQueryable<CarDto>> GetMany(int[] ids)
    {
        var cars = await _carRepository.GetMany(ids);
        var carDtos = _mapper.Map<IQueryable<CarDto>>(cars);
        return carDtos;
    }

    public async Task<IQueryable<Car>> SearchCars(CarFilterDto filterDto)
    {
        var cars = await _carRepository.SearchCars(
            MinPrice: filterDto.MaxPrice,
            Mark: filterDto.Mark,
            FuelType: filterDto.FuelType,
            MaxPrice: filterDto.MaxPrice,
            MaxYear: filterDto.MaxYear,
            MinYear: filterDto.MinYear,
            VehicleType: filterDto.VehicleType
            );
        var carDtos = _mapper.Map<IQueryable<CarDto>>(cars);
        return cars;
    }
}