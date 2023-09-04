using AutoMapper;
using BusinessLogic.Models.Car;
using BusinessLogic.Services;
using BusinessLogic.Services.Implemetation;
using CarSharingAPI.Requests;
using CarSharingAPI.Responses;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICarService _carService;


    public CarController(CarService carService, IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("get-id")]
    public async Task<IActionResult> Get(int id)
    {
        if (!await _carService.ExistsAsync(id))
        {
            return NotFound();
        }

        var carDto = await _carService.GetByIdAsync(id);
        var response = _mapper.Map<CarResponse>(carDto);
        return Ok(response);
    }
    [HttpGet]
    [Route("get-many-id")]
    public async Task<IActionResult> GetMany(int[] ids)
    {
        var cars = await _carService.GetMany(ids);
        if (!cars.Any()) return NotFound("No Cars with that ids were found");
        var response = _mapper.Map<IQueryable<CarResponse>>(cars);
        return Ok(response);
    }
    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search(SearchCarRequest entity)
    {
        var request = _mapper.Map<CarFilterDto>(entity);
        var cars = await _carService.SearchCars(request);
        if (!cars.Any()) return NotFound("No Cars with that ids were found");
        var response = _mapper.Map<IQueryable<CarResponse>>(cars);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var carDtos = await _carService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<CarResponse>>(carDtos);
        return Ok(response);
    }
    
    
    [Authorize()]//lender
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(CreateCarRequest entity)
    {
        var request = _mapper.Map<CarDto>(entity);
        var responseDto = await _carService.AddAsync(request);
        var response = _mapper.Map<CarResponse>(responseDto);
        return Ok(response);
    }
    
    // same id 
    [Authorize()]//lender
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Edit(CarRequest entity)
    {
        if (!await _carService.ExistsAsync(entity.Id))
        {
            return NotFound();
        }
        var carDto = _mapper.Map<CarDto>(entity);
        var newCarDto = await _carService.UpdateAsync(carDto);
        var response = _mapper.Map<CarResponse>(newCarDto);
        return Ok(response);
        
    }
    /*[HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> EditPartly(CreateCarRequest entity)
    {
        if (!await _carService.ExistsAsync(id))
        {
            return NotFound();
        }

        await _carService.UpdateAsync(entity);
        return Ok();
    }*/
    // same id 
    [Authorize()]//lender
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _carService.ExistsAsync(id))
        {
            return NotFound();
        }

        var responseDto = await _carService.DeleteAsync(id);
        var response = _mapper.Map<CarResponse>(responseDto);
        return Ok(response);
    }
}