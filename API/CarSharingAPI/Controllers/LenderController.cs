using BusinessLogic.Models.Lender;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class LenderController : ControllerBase
{
    private readonly ILenderService _lenderService;

    public LenderController(ILenderService lenderService)
    {
        _lenderService = lenderService;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(LenderDto entity)
    {
        await _lenderService.AddAsync(entity);
        return Ok();
    }
}