using OrdenesAPI.DTO.Clases;
using OrdenesAPI.DTO.Request;
using OrdenesAPI.DTO.Response;
using OrdenesAPI.Models;

namespace OrdenesAPI.IServices
{
    public interface IOrdenService
    {
        Task<OrdenResponse> CreateAsync(CreateOrdenRequest request);
        Task<PagedResult<OrdenResponse>> GetAllAsync(int page, int pageSize);
        Task<OrdenResponse?> GetByIdAsync(int id);
        Task<OrdenResponse> UpdateAsync(int id, OrdenUpdateRequest request);
        Task DeleteAsync(int id);
    }
}
