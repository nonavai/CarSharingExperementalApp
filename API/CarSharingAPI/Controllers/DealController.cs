using AutoMapper;
using BusinessLogic.Models.Deal;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using CarSharingAPI.Identity;
using CarSharingAPI.Requests.Deal;
using CarSharingAPI.Requests.User;
using CarSharingAPI.Responses;
using CarSharingAPI.Responses.Deal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class DealController : ControllerBase
{
    private readonly IDealService _dealService;
    private readonly IMapper _mapper;

    public DealController( IMapper mapper, IDealService dealService)
    {
        _mapper = mapper;
        _dealService = dealService;
    }

    [HttpGet]
    [Route("get-id")]
    public async Task<IActionResult> Get(int id)
    {
        if (!await _dealService.ExistsAsync(id))
        {
            return NotFound();
        }

        var dealDto = await _dealService.GetByIdAsync(id);
        var response = _mapper.Map<CreateDealRequest>(dealDto);
        return Ok(response);
    }
    

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(CreateDealRequest entity)
    {
        var dto = _mapper.Map<DealDto>(entity);
        var responseDto = await _dealService.AddAsync(dto);
        var response = _mapper.Map<CreateDealResponse>(responseDto);
        return Ok(response);
    }
    
    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpPut]
    [Route("Confirm")]
    public async Task<IActionResult> Edit(int id, [FromBody] ConfirmDealRequest entity)
    {
        
        if (!await _dealService.ExistsAsync(id))
        {
            return NotFound();
        }

        var dealDto = _mapper.Map<DealDto>(entity);
        dealDto.Id = id;
        var newUserDto = await _dealService.UpdateAsync(dealDto);
        var response = _mapper.Map<CreateDealResponse>(newUserDto);
        return Ok(response);
    }
    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpPut]
    [Route("Rate")]
    public async Task<IActionResult> Edit(int id, int raiting)
    {
        
        if (!await _dealService.ExistsAsync(id))
        {
            return NotFound();
        }
        
        var newUserDto = await _dealService.RateDealAsync(id, raiting);
        var response = _mapper.Map<CreateDealResponse>(newUserDto);
        return Ok(response);
    }
    
    
}