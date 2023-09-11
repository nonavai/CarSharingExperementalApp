using AutoMapper;
using BusinessLogic.Models.Deal;
using DataAccess.Entities;
using DataAccess.Repositories;
using Shared.Exceptions;

namespace BusinessLogic.Services.Implemetation;

public class DealService : IDealService
{
    private readonly IActivityService _activityService;
    private readonly IDealRepository _dealRepository;
    private readonly IMapper _mapper;

    public DealService(IActivityService activityService, IMapper mapper, IDealRepository dealRepository)
    {
        _activityService = activityService;
        _mapper = mapper;
        _dealRepository = dealRepository;
    }
    
    public async Task<DealDto> GetByIdAsync(int id)
    {
        var deal = await _dealRepository.GetByIdAsync(id);
        if (deal == null)
        {
            throw new NotFoundException("Deal not found");
        }
        var dealDto = _mapper.Map<DealDto>(deal);
        return dealDto;
    }

    public async Task<DealDto> AddAsync(DealDto dealDto)
    {
        var deal = _mapper.Map<Deal>(dealDto);
        var activity = await _activityService.GetByCarIdAsync(dealDto.CarId);
        if (activity == null)
        {
            throw new NotFoundException("Deal not found");
        }

        activity.IsActive = false;
        await _activityService.UpdateAsync(activity);
        var newDeal = await _dealRepository.AddAsync(deal);
        var newDealDto = _mapper.Map<DealDto>(newDeal);
        return newDealDto;
    }
    
    
    

}