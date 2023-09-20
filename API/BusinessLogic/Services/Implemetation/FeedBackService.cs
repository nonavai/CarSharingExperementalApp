using AutoMapper;
using BusinessLogic.Models.FeedBack;
using DataAccess.Entities;
using DataAccess.Repositories.Implementation;
using Shared.Exceptions;

namespace BusinessLogic.Services.Implemetation;

public class FeedBackService : IFeedBackService
{
    private readonly IFeedBackRepository _feedBackRepository;
    private readonly IMapper _mapper;

    public FeedBackService( IMapper mapper, IFeedBackRepository feedBackRepository)
    {
        _mapper = mapper;
        _feedBackRepository = feedBackRepository;
    }

    public async Task<FeedBackDto> GetByIdAsync(int id)
    {
        var feedBack = await _feedBackRepository.GetByIdAsync(id);
        if (feedBack == null)
        {
            throw new NotFoundException("FeedBack not found");
        }
        var feedBackDto = _mapper.Map<FeedBackDto>( feedBack);
        return feedBackDto;
    }

    public async Task<IEnumerable<FeedBackDto>> GetAllAsync()
    {
        var feedBackDto = _mapper.Map<IEnumerable<FeedBackDto>>( await _feedBackRepository.GetAllAsync());
        return feedBackDto;
    }

    public async Task<FeedBackDto> AddAsync(FeedBackDto entity)
    {
        var feedBack = _mapper.Map<FeedBack>(entity);
        var feedBackDto = _mapper.Map<FeedBackDto>( await _feedBackRepository.AddAsync(feedBack));
        return feedBackDto;
    }

    public async Task<FeedBackDto> UpdateAsync(FeedBackDto entity)
    {
        
        var existingFeedBack = await _feedBackRepository.GetByIdAsync(entity.Id);
        if (existingFeedBack == null)
        {
            throw new NotFoundException("FeedBack not found");
        }
        existingFeedBack.Comment = entity.Comment;
        existingFeedBack.Rating = entity.Rating;

        var feedBackDto = _mapper.Map<FeedBackDto>( await _feedBackRepository.UpdateAsync(existingFeedBack));
        return feedBackDto;
    }

    
    public async Task<FeedBackDto> DeleteAsync(int id)
    {
        var feedBack = await _feedBackRepository.GetByIdAsync(id);
        if (feedBack == null)
        {
            throw new NotFoundException("FeedBack not found");
        }
        
        var feedBackDto = _mapper.Map<FeedBackDto>( await _feedBackRepository.DeleteAsync(id));
        return feedBackDto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _feedBackRepository.ExistsAsync(id);
    }


    public async Task<IQueryable<FeedBackDto>> GetByCarId(int id)
    {
        var feedBacks = await _feedBackRepository.GetByCarId(id);
        var feedBackDtos = _mapper.Map<IQueryable<FeedBackDto>>(feedBacks);
        return feedBackDtos;
    }

    public async Task<IQueryable<FeedBackDto>> GetByUserId(int id)
    {
        var feedBacks = await _feedBackRepository.GetByUserId(id);
        var feedBackDtos = _mapper.Map<IQueryable<FeedBackDto>>(feedBacks);
        return feedBackDtos;
    }
}