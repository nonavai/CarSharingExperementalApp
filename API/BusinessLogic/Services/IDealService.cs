using BusinessLogic.Models.Deal;

namespace BusinessLogic.Services;

public interface IDealService
{
    Task<DealDto> GetByIdAsync(int id);
    Task<DealDto> RegisterDealAsync(DealDto dealDto);
    Task<DealDto> ConfirmDealAsync(DealDto dealDto);
    Task<DealDto> RateDealAsync(int id, int raiting);
    Task CancelDealAsync(int id);
    Task<bool> ExistsAsync(int id);
}