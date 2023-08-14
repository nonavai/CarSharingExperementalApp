using AutoMapper;
using BusinessLogic.Models.Car;
using BusinessLogic.Models.Roles;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Implemetation;

public class RolesService : IRolesService
{
    private readonly IRolesRepository _rolesRepository;
    private readonly IMapper _mapper;

    public RolesService(IRolesRepository rolesRepository, IMapper mapper)
    {
        _rolesRepository = rolesRepository;
        _mapper = mapper;
    }

    public async Task<RolesDto> GetByIdAsync(int id)
    {
        var roles = await _rolesRepository.GetByIdAsync(id);
        if (roles == null)
        {
            throw new Exception("Roles not found");
        }
        var rolesDto = _mapper.Map<RolesDto>( roles);
        return rolesDto;
    }

    public async Task<IEnumerable<RolesDto>> GetAllAsync()
    {
        var rolesDto = _mapper.Map<IEnumerable<RolesDto>>( await _rolesRepository.GetAllAsync());
        return rolesDto;
    }

    public async Task<RolesDto> AddAsync(RolesDto entity)
    {
        var roles = _mapper.Map<Roles>(entity);
        var rolesDto = _mapper.Map<RolesDto>( await _rolesRepository.AddAsync(roles));
        return rolesDto;
    }

    public async Task<RolesDto> UpdateAsync(RolesDto entity)
    {
        
        var existingRoles = await _rolesRepository.GetByIdAsync(entity.Id);
        if (existingRoles == null)
        {
            throw new Exception("Roles not found");
        }
        existingRoles.Admin = entity.Admin;
        existingRoles.BorrowerId = entity.BorrowerId;
        existingRoles.LenderId = entity.LenderId;

        var rolesDto = _mapper.Map<RolesDto>( await _rolesRepository.UpdateAsync(existingRoles));
        return rolesDto;
    }

    
    public async Task<RolesDto> DeleteAsync(int id)
    {
        var roles = await _rolesRepository.GetByIdAsync(id);
        if (roles == null)
        {
            throw new ArgumentException("Roles not found");
        }
        
        var rolesDto = _mapper.Map<RolesDto>( await _rolesRepository.DeleteAsync(id));
        return rolesDto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _rolesRepository.ExistsAsync(id);
    }
}