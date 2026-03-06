using OrdenesAPI.DTO.Request;
using OrdenesAPI.DTO.Response;
using OrdenesAPI.Models;

namespace OrdenesAPI.IServices
{
    public interface IProductoService
    {
        Task<ProductoResponse> CreateAsync(ProductoRequest productoRequest);
        Task<ProductoResponse?> GetByIdAsync(int id);
        Task<List<ProductoResponse>> GetAllAsync();
    }
}
