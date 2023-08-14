using AutoMapper;
using BusinessLogic.Models.Activity;
using BusinessLogic.Services;
using CarSharingAPI.Requests;
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
    [Route("get-id")]
    public async Task<IActionResult> Get(int id)
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
    [Route("get-carId")]
    public async Task<IActionResult> GetByCarId(int id)
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
    [Route("get-radius")]
    public async Task<IActionResult> GetByRadius(ActivityGeoRequest activityGeoRequest)
    {
        //if ()    validation

        var activityDtoGeo = _mapper.Map<ActivityDtoGeo>(activityGeoRequest);
        var responseDto = await _activityService.GetByRadiusAsync(activityDtoGeo);
        var response = _mapper.Map<BorrowerResponse>(responseDto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var carDtos = await _activityService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<ActivityRequest>>(carDtos);
        return Ok(response);
    }
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Edit(ActivityRequest entity)
    {
        if (!await _activityService.ExistsAsync(entity.Id))
        {
            return NotFound();
        }
        var carDto = _mapper.Map<ActivityDto>(entity);
        var newCarDto = await _activityService.UpdateAsync(carDto);
        var response = _mapper.Map<ActivityResponse>(newCarDto);
        return Ok(response);
    }
}