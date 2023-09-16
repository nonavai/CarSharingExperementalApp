using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BusinessLogic.Models.Activity;
using BusinessLogic.Models.Borrower;
using DataAccess.Entities;
using DataAccess.Repositories;
using FluentValidation;
using Shared.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace BusinessLogic.Services.Implemetation;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ActivityDto> _validator;
    public ActivityService( IMapper mapper, IActivityRepository activityRepository, IValidator<ActivityDto> validator)
    {
        _mapper = mapper;
        _activityRepository = activityRepository;
        _validator = validator;
    }

    public async Task<ActivityDto> GetByIdAsync(int id)
    {
        var activeList = await _activityRepository.GetByIdAsync(id);
        if (activeList == null)
        {
            throw new NotFoundException("Borrower not found");
        }
        var activityDto = _mapper.Map<ActivityDto>( activeList);
        return activityDto;
    }

    public async Task<IEnumerable<ActivityDto>> GetAllAsync()
    {
        var activityDto = _mapper.Map<IEnumerable<ActivityDto>>(  await _activityRepository.GetAllAsync());
        return activityDto;
    }

    public async Task<ActivityDto> AddAsync(ActivityDto entity)
    {
        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        
        var activeList = _mapper.Map<Activity>(entity);
        var activityDto = _mapper.Map<ActivityDto>( await _activityRepository.AddAsync(activeList));
        return activityDto;
    }
    

    public async Task<ActivityDto> UpdateAsync(ActivityDto entity)
    {
        var existingActiveList = await _activityRepository.GetByIdAsync(entity.Id);
        if (existingActiveList == null)
        {
            throw new NotFoundException("ActiveList not found");
        }
        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        existingActiveList.CarId = entity.CarId;
        existingActiveList.Latitude = entity.Latitude;
        existingActiveList.Longitude = entity.Longitude;
        existingActiveList.IsActive = entity.IsActive;
        

        var activityDto = _mapper.Map<ActivityDto>( await _activityRepository.UpdateAsync(existingActiveList));
        return activityDto;
    }

    public async Task<ActivityDto> DeleteAsync(int id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
        {
            throw new NotFoundException("ActiveList not found");
        }
        
        var activityDto = _mapper.Map<ActivityDto>( await _activityRepository.DeleteAsync(id));
        return activityDto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _activityRepository.ExistsAsync(id);
    }

    public async Task<ActivityDto> GetByCarIdAsync(int id)
    {
        var activity = await _activityRepository.GetByCarIdAsync(id); 
        if (activity == null)
        {
            throw new NotFoundException("ActiveList not found");
        }
        var activityDto = _mapper.Map<ActivityDto>(activity);
        return activityDto;
    }

    public async Task<IQueryable<ActivityDto>> GetByRadiusAsync(ActivityDtoGeo dto)
    {
        var activity =  await _activityRepository.GetByRadiusAsync(dto.RadiusKm, dto.Latitude, dto.Longitude);
        var activityDto = _mapper.Map<IQueryable<ActivityDto>>(activity);
        return activityDto;
    }

    public async Task<ActivityDto> SetUnactive(int id, bool active = false)
    {
        var activity = await GetByIdAsync(id);
        activity.IsActive = active;
        var result = await UpdateAsync(activity);
        return result;
    }


    // add request to check IsActive status
}