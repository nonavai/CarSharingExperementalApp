using AutoMapper;
using BusinessLogic.Models.Roles;
using BusinessLogic.Models.User;
using DataAccess.Entities;
using DataAccess.Repositories;
using FluentValidation;
using Shared.Exceptions;


namespace BusinessLogic.Services.Implemetation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRolesService _rolesService;
    private readonly IMapper _mapper;
    private readonly IValidator<UserDto> _validator;

    public UserService(IUserRepository userRepository, IMapper mapper, IRolesService rolesService, IValidator<UserDto> validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _rolesService = rolesService;
        _validator = validator;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("Car not found");
        }
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var userDto = _mapper.Map<IEnumerable<UserDto>>( await _userRepository.GetAllAsync());
        return userDto;
    }

    public async Task<UserDto> AddAsync(UserDto entity)
    {
        if (await IsEmailExist(entity.Email))
        {
            throw new BadAuthorizeException("User already exist");
        }
        var validationResult = _validator.Validate(entity);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        
        // CREATING USER ROLES
        var roles = new RolesDto() { Admin = false, BorrowerId= null, LenderId = null};
        var roleDto =await _rolesService.AddAsync(roles);
        User user = _mapper.Map<User>(entity);                                   
        user.RoleId = roleDto.Id;
        var compitedUser = await _userRepository.AddAsync(user);
        var userDto = _mapper.Map<UserDto>(  compitedUser);
        return userDto;
    }

    public async Task<UserDto> UpdateAsync(UserDto entity)
    {
        var existingUser = await _userRepository.GetByIdAsync(entity.Id);
        if (existingUser == null)
        {
            throw new NotFoundException("User not found");
        }
        //check on existing
        var validationResult = _validator.Validate(entity);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        //validating
        existingUser.Email = entity.Email;
        existingUser.Password = entity.Password;
        existingUser.PhoneNumber = entity.PhoneNumber;
        existingUser.FirstName = entity.FirstName;
        existingUser.LastName = entity.LastName;
        existingUser.RecordNumber = entity.RecordNumber;
        existingUser.Description = entity.Description;


        var userDto = _mapper.Map<UserDto>(await _userRepository.UpdateAsync(existingUser));
        return userDto;
    }

    public async Task<UserDto> DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var userDto = _mapper.Map<UserDto>( await _userRepository.DeleteAsync(id));
        return userDto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _userRepository.ExistsAsync(id);
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<bool> IsEmailExist(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user != null;
    }

}