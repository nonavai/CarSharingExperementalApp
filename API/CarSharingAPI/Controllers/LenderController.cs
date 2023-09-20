using AutoMapper;
using BusinessLogic.Models.Lender;
using BusinessLogic.Services;
using CarSharingAPI.Requests;
using CarSharingAPI.Requests.Lender;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class LenderController : ControllerBase
{
    private readonly ILenderService _lenderService;
    private readonly IMapper _mapper;

    public LenderController(ILenderService lenderService, IMapper mapper)
    {
        _lenderService = lenderService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(LenderRequest entity)
    {
        var lender = _mapper.Map<LenderDto>(entity);
        var lenderDto = await _lenderService.AddAsync(lender);
        return Ok(lenderDto);
    }

   
}