using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BusinessLogic.Models.Borrower;
using BusinessLogic.Services;
using CarSharingAPI.Responses;


namespace CarSharingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BorrowerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBorrowerService _borrowerService;


    public BorrowerController( IMapper mapper, IBorrowerService borrowerService)
    {
        _mapper = mapper;
        _borrowerService = borrowerService;
    }

    [HttpGet]
    [Route("get-id")]
    public async Task<IActionResult> Get(int id)
    {
        if (!await _borrowerService.ExistsAsync(id))
        {
            return NotFound();
        }

        var carDto = await _borrowerService.GetByIdAsync(id);
        var response = _mapper.Map<BorrowerResponse>(carDto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var carDtos = await _borrowerService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<BorrowerResponse>>(carDtos);
        return Ok(response);
    }
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(BorrowerDto entity)
    {
        var request = _mapper.Map<BorrowerDto>(entity);
        var responseDto = await _borrowerService.AddAsync(request);
        var response = _mapper.Map<BorrowerResponse>(responseDto);
        return Ok(response);
    }
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Edit(BorrowerDto entity)
    {
        if (!await _borrowerService.ExistsAsync(entity.Id))
        {
            return NotFound();
        }
        var carDto = _mapper.Map<BorrowerDto>(entity);
        var newCarDto = await _borrowerService.UpdateAsync(carDto);
        var response = _mapper.Map<BorrowerResponse>(newCarDto);
        return Ok(response);
    }
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _borrowerService.ExistsAsync(id))
        {
            return NotFound();
        }

        var responseDto = await _borrowerService.DeleteAsync(id);
        var response = _mapper.Map<BorrowerResponse>(responseDto);
        return Ok(response);
    }
}