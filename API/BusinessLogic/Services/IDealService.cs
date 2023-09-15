using BusinessLogic.Models.Deal;

namespace BusinessLogic.Services;

public interface IDealService
{
    Task<DealDto> GetByIdAsync(int id);
    Task<DealDto> AddAsync(DealDto dealDto);
    Task<DealDto> UpdateAsync(DealDto dealDto);
    Task<DealDto> RateDealAsync(int id, int raiting);
    Task<bool> ExistsAsync(int id);
}