using AutoMapper;
using BusinessLogic.Models.Deal;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
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
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
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
    public async Task<IActionResult> Register(CreateDealRequest entity)
    {
        var dto = _mapper.Map<DealDto>(entity);
        var responseDto = await _dealService.RegisterDealAsync(dto);
        var response = _mapper.Map<CreateDealResponse>(responseDto);
        return Ok(response);
    }
    
    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpPut]
    [Route("{id:int}/Confirm")]
    public async Task<IActionResult> Confirm([FromRoute] int id, [FromBody] ConfirmDealRequest entity)
    {
        
        if (!await _dealService.ExistsAsync(id))
        {
            return NotFound();
        }

        var dealDto = _mapper.Map<DealDto>(entity);
        dealDto.Id = id;
        var newUserDto = await _dealService.ConfirmDealAsync(dealDto);
        var response = _mapper.Map<CreateDealResponse>(newUserDto);
        return Ok(response);
    }
    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpPut]
    [Route("{id:int}/Rate")]
    public async Task<IActionResult> Rate([FromRoute] int id, int raiting)
    {
        
        if (!await _dealService.ExistsAsync(id))
        {
            return NotFound();
        }
        
        var newUserDto = await _dealService.RateDealAsync(id, raiting);
        var response = _mapper.Map<CreateDealResponse>(newUserDto);
        return Ok(response);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Cancel([FromRoute]int id)
    {
        if (!await _dealService.ExistsAsync(id))
        {
            return NotFound();
        }

        await _dealService.CancelDealAsync(id);
        return Ok();
    }
    
    
}