using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Models.Lender;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Implemetation;

public class LenderService : ILenderService
{
    private readonly ILenderRepository _lenderRepository;
    private readonly IMapper _mapper;

    public LenderService(ILenderRepository lenderRepository, IMapper mapper)
    {
        _lenderRepository = lenderRepository;
        _mapper = mapper;
    }

    public async Task<LenderDto> GetByIdAsync(int id)
    {
        var lender = await _lenderRepository.GetByIdAsync(id);
        if (lender == null)
        {
            throw new Exception("Lender not found");
        }
        var lenderDto = _mapper.Map<LenderDto>( lender);
        return lenderDto;
    }

    public async Task<IEnumerable<LenderDto>> GetAllAsync()
    {
        var lenderDto = _mapper.Map<IEnumerable<LenderDto>>( await _lenderRepository.GetAllAsync());
        return lenderDto;
    }

    public async Task<LenderDto> AddAsync(LenderDto entity)
    {
        if (false) //validation
        {
            throw new NotImplementedException();
        }

        var lender = _mapper.Map<Lender>(entity);
        var lenderDto = _mapper.Map<LenderDto>( await _lenderRepository.AddAsync(lender));
        return lenderDto;
    }

    public async Task<LenderDto> UpdateAsync(LenderDto entity)
    {
        throw new NotImplementedException();
    }

    public async Task<LenderDto> DeleteAsync(int id)
    {
        var lender = await _lenderRepository.GetByIdAsync(id);
        if (lender == null)
        {
            throw new ArgumentException("Lender not found");
        }
        
        var lenderDto = _mapper.Map<LenderDto>( await _lenderRepository.DeleteAsync(id));
        return lenderDto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _lenderRepository.ExistsAsync(id);
    }
}