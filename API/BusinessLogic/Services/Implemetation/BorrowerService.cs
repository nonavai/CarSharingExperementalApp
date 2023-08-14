using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Models.Borrower;
using BusinessLogic.Models.User;
using DataAccess.Entities;
using DataAccess.Repositories;
using FluentValidation;

namespace BusinessLogic.Services.Implemetation;

public class BorrowerService : IBorrowerService
{
    private readonly IBorrowerRepository _borrowerRepository;
    private readonly IMapper _mapper;

    private readonly IValidator<BorrowerDto> _validator;
    // manipulations
    public BorrowerService(IBorrowerRepository borrowerRepository, IMapper mapper, IValidator<BorrowerDto> validator)
    {
        _borrowerRepository = borrowerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BorrowerDto> GetByIdAsync(int id)
    {
        var borrower = await _borrowerRepository.GetByIdAsync(id);
        if (borrower == null)
        {
            throw new Exception("Borrower not found");
        }
        var borrowerDto = _mapper.Map<BorrowerDto>( borrower);
        return borrowerDto;
    }

    public async Task<IEnumerable<BorrowerDto>> GetAllAsync()
    {
        var borrowerDto = _mapper.Map<IEnumerable<BorrowerDto>>( await _borrowerRepository.GetAllAsync());
        return borrowerDto;
    }

    public async Task<BorrowerDto> AddAsync(BorrowerDto entity)
    {
        var validationResult = _validator.Validate(entity);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        //validating
        
        var borrower = _mapper.Map<Borrower>(entity);
        var borrowerDto = _mapper.Map<BorrowerDto>( await _borrowerRepository.AddAsync(borrower));
        return borrowerDto;
    }

    public async Task<BorrowerDto> UpdateAsync(BorrowerDto entity)
    {
        var existingBorrower = await _borrowerRepository.GetByIdAsync(entity.Id);
        if (existingBorrower == null)
        {
            throw new Exception("Car not found");
        }
        
        var validationResult = _validator.Validate(entity);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        //validating
        existingBorrower.Category = entity.Category;
        existingBorrower.Country = entity.Country;
        existingBorrower.FirstName = entity.FirstName;
        existingBorrower.LastName = entity.LastName;
        existingBorrower.LicenceExpiry = entity.LicenceExpiry;
        existingBorrower.LicenceIssue = entity.LicenceIssue;
        existingBorrower.PlaceOfIssue = entity.PlaceOfIssue;
        existingBorrower.LicenceId = entity.LicenceId;

        var borrowerDto = _mapper.Map<BorrowerDto>( await _borrowerRepository.UpdateAsync(existingBorrower));
        return borrowerDto;
    }

    public async Task<BorrowerDto> DeleteAsync(int id)
    {
        var borrower = await _borrowerRepository.GetByIdAsync(id);
        if (borrower == null)
        {
            throw new ArgumentException("Borrower not found");
        }
        
        var borrowerDto = _mapper.Map<BorrowerDto>(  await _borrowerRepository.DeleteAsync(id));
        return borrowerDto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _borrowerRepository.ExistsAsync(id);
    }
}