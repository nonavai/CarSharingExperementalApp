using AutoMapper;
using BusinessLogic.Models.Activity;
using BusinessLogic.Services;
using CarSharingAPI.Requests;
using CarSharingAPI.Requests.Activity;
using CarSharingAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSharingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IActivityService _activityService;


    public ActivityController(IActivityService activityService, IMapper mapper)
    {
        _activityService = activityService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        if (!await _activityService.ExistsAsync(id))
        {
            return NotFound();
        }

        var responseDto = await _activityService.GetByIdAsync(id);
        var response = _mapper.Map<ActivityResponse>(responseDto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("Car/{id:int}")]
    public async Task<IActionResult> GetByCarId([FromRoute]int id)
    {
        if (!await _activityService.ExistsAsync(id))
        {
            return NotFound();
        }

        var responseDto = await _activityService.GetByCarIdAsync(id);
        var response = _mapper.Map<ActivityResponse>(responseDto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("InRadious")]
    public async Task<IActionResult> GetByRadius(ActivityGeoRequest activityGeoRequest)
    {
        //if ()    validation

        var activityDtoGeo = _mapper.Map<ActivityDtoGeo>(activityGeoRequest);
        var responseDto = await _activityService.GetByRadiusAsync(activityDtoGeo);
        var response = _mapper.Map<BorrowerResponse>(responseDto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAll()
    {
        var carDtos = await _activityService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<ActivityRequest>>(carDtos);
        return Ok(response);
    }
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ActivityRequest request)
    {
        if (!await _activityService.ExistsAsync(id))
        {
            return NotFound();
        }
        var activityDto = _mapper.Map<ActivityDto>(request);
        activityDto.Id = id;
        var newActivityDto = await _activityService.UpdateAsync(activityDto);
        var response = _mapper.Map<ActivityResponse>(newActivityDto);
        return Ok(response);
    }
}