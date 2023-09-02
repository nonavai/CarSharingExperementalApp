using AutoMapper;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using CarSharingAPI.Identity;
using CarSharingAPI.Requests;
using CarSharingAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, IMapper mapper, ITokenService tokenService)
    {
        _userService = userService;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Route("get-id")]
    public async Task<IActionResult> Get(int id)
    {
        if (!await _userService.ExistsAsync(id))
        {
            return NotFound();
        }

        var userDto = await _userService.GetByIdAsync(id);
        var response = _mapper.Map<UserResponse>(userDto);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("LogIn")]
    public async Task<IActionResult> Login(LogInRequest entity)
    {
        var user = await _userService.GetByEmailAsync(entity.Email);
        if (user == null)
        {
            return NotFound("No users with that Email address");
        }

        if (user.Password != entity.Password)
        {
            return BadRequest("Wrong Password");
            
        }

        var response = _mapper.Map<LogInResponse>(user);
        var refreshToken = await _tokenService.GetByUserId(response.Id) ?? await _tokenService.GenerateRefreshToken(user);
        var accessToken = await _tokenService.GenerateAccessToken(refreshToken);
        response.Token = accessToken;
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(CreateUserRequest entity)
    {
        var dto = _mapper.Map<UserDto>(entity);
        var responseDto = await _userService.AddAsync(dto);
        var response = _mapper.Map<UserResponse>(responseDto);
        return Ok(response);
    }
    
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Edit(UserRequest entity)
    {
        
        if (!await _userService.ExistsAsync(entity.Id))
        {
            return NotFound();
        }

        var userDto = _mapper.Map<UserDto>(entity);
        var newUserDto = await _userService.UpdateAsync(userDto);
        var response = _mapper.Map<UserResponse>(newUserDto);
        return Ok(response);
    }
    
    [ValidateToken]
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _userService.ExistsAsync(id))
        {
            return NotFound();
        }

        var responseDto = await _userService.DeleteAsync(id);
        var response = _mapper.Map<UserDto>(responseDto);
        return Ok(response);
    }

}