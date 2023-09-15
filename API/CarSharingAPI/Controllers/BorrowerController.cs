﻿using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BusinessLogic.Models.Borrower;
using BusinessLogic.Services;
using CarSharingAPI.Requests;
using CarSharingAPI.Requests.Borrower;
using CarSharingAPI.Responses;
using Microsoft.AspNetCore.Authorization;


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
        var request = await _borrowerService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<BorrowerResponse>>(request);
        return Ok(response);
    }
    [Authorize]
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(BorrowerRequest request)
    {
        var dto = _mapper.Map<BorrowerDto>(request);
        var responseDto = await _borrowerService.AddAsync(dto);
        var response = _mapper.Map<BorrowerResponse>(responseDto);
        return Ok(response);
    }
    [Authorize()]//borrower
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Edit(int id, [FromBody] BorrowerRequest request)
    {
        if (!await _borrowerService.ExistsAsync(id))
        {
            return NotFound();
        }
        var borrowerDto = _mapper.Map<BorrowerDto>(request);
        borrowerDto.Id = id;
        var newBorrowerDto = await _borrowerService.UpdateAsync(borrowerDto);
        var response = _mapper.Map<BorrowerResponse>(newBorrowerDto);
        return Ok(response);
    }
    //same id
    [Authorize()]//borrower
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