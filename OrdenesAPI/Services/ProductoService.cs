using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrdenesAPI.DataContext;
using OrdenesAPI.DTO.Request;
using OrdenesAPI.DTO.Response;
using OrdenesAPI.IServices;
using OrdenesAPI.Models;
namespace OrdenesAPI.Services
{
    public class ProductoService : IProductoService
    {

        private readonly OrdenContext _context;
        private readonly IMapper _mapper;
        public ProductoService(OrdenContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductoResponse> CreateAsync(ProductoRequest productoRequest)
        {
            Producto producto = _mapper.Map<Producto>(productoRequest);
            if (producto.Precio <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor a 0");
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            ProductoResponse response = _mapper.Map<ProductoResponse>(producto);

            return response;
        }

        public async Task<List<ProductoResponse>> GetAllAsync()
        {
            var productos = await _context.Productos
                                 .AsNoTracking()
                                 .OrderBy(p => p.Nombre)
                                 .ToListAsync();

            List<ProductoResponse> response = _mapper.Map<List<ProductoResponse>>(productos);

            return response;
        }

        public async Task<ProductoResponse?> GetByIdAsync(int id)
        {
            var producto = await _context.Productos
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id == id);
            ProductoResponse response = _mapper.Map<ProductoResponse>(producto);
            return response;
        }
    }

}