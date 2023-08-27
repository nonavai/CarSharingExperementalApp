using AutoMapper;
using BusinessLogic.Models.Car;
using BusinessLogic.Validators;
using CustomExceptionsLibrary.Exceptions;
using DataAccess.Entities;
using DataAccess.Repositories;
using FluentValidation;

namespace BusinessLogic.Services.Implemetation;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CarDto> _validator;

    public CarService(ICarRepository carRepository, IMapper mapper, IValidator<CarDto> validator)
    {
        _carRepository = carRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CarDto> GetByIdAsync(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
        {
            throw new NotFoundException("Car not found");
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
        var validationResult = _validator.Validate(entity);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        var car = _mapper.Map<Car>(entity);
        var carDto = _mapper.Map<CarDto>( await _carRepository.AddAsync(car));
        return carDto;
    }

    public async Task<CarDto> UpdateAsync(CarDto entity)
    {
        
        var existingCar = await _carRepository.GetByIdAsync(entity.Id);
        if (existingCar == null)
        {
            throw new NotFoundException("Car not found");
        }
        var validationResult = _validator.Validate(entity);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
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
            throw new NotFoundException("Car not found");
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