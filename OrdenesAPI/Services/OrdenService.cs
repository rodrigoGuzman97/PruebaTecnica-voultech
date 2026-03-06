using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrdenesAPI.DataContext;
using OrdenesAPI.DTO.Clases;
using OrdenesAPI.DTO.Request;
using OrdenesAPI.DTO.Response;
using OrdenesAPI.IServices;
using OrdenesAPI.Models;

namespace OrdenesAPI.Services
{
    public class OrdenService : IOrdenService
    {

        private readonly OrdenContext _ordenContext;
        private readonly IMapper _mapper;

        public OrdenService(OrdenContext ordenContext, IMapper mapper)
        {
            _ordenContext = ordenContext;
            _mapper = mapper;
        }
        public async Task<OrdenResponse> CreateAsync(CreateOrdenRequest request)
        {
            if (!request.ProductosIds.Any())
            {
                throw new ArgumentException("La orden debe tener al menos un producto.");
            }

            var productos = await _ordenContext.Productos
                .Where(p => request.ProductosIds.Contains(p.Id))
                .ToListAsync();

            if (productos.Count != request.ProductosIds.Count)
            {
                throw new ArgumentException("Uno o más productos no existen.");
            }

            decimal total = CalcularTotalConDescuento(productos);

            using var transaction = await _ordenContext.Database.BeginTransactionAsync();

            var orden = new OrdenCompra
            {
                Cliente = request.Cliente,
                FechaCreacion = DateTime.UtcNow,
                Total = total
            };

            _ordenContext.OrdenCompras.Add(orden);
            await _ordenContext.SaveChangesAsync();

            var ordenProductos = productos.Select(p => new OrdenProducto
            {
                OrdenCompraId = orden.Id,
                ProductoId = p.Id
            }).ToList();

            _ordenContext.OrdenProductos.AddRange(ordenProductos);

            await _ordenContext.SaveChangesAsync();
            await transaction.CommitAsync();
            OrdenResponse response = _mapper.Map<OrdenResponse>(orden);
            return response;
        }

        public async Task<PagedResult<OrdenResponse>> GetAllAsync(int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _ordenContext.OrdenCompras
                .Include(o => o.OrdenProductos)
                .ThenInclude(op => op.Producto)
                .AsNoTracking();

            var totalRecords = await query.CountAsync();

            var ordenes = await query
                .OrderByDescending(o => o.FechaCreacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<OrdenResponse>>(ordenes);

            return new PagedResult<OrdenResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
                Data = mapped
            };
        }

        public async Task<OrdenResponse?> GetByIdAsync(int id)
        {
            var orden = await _ordenContext.OrdenCompras
                        .Include(o => o.OrdenProductos)
                        .ThenInclude(op => op.Producto)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(o => o.Id == id);

            if (orden == null)
            {
                return null;
            }

            return _mapper.Map<OrdenResponse>(orden);
        }

        public async Task<OrdenResponse> UpdateAsync(int id, OrdenUpdateRequest request)
        {
            var orden = await _ordenContext.OrdenCompras
                        .Include(o => o.OrdenProductos)
                        .FirstOrDefaultAsync(o => o.Id == id);

            if (orden == null)
            {
                throw new KeyNotFoundException($"Orden con id {id} no encontrada");
            }

            var productos = await _ordenContext.Productos
                .Where(p => request.ProductosIds.Contains(p.Id))
                .ToListAsync();

            if (productos.Count != request.ProductosIds.Count)
            {
                throw new ArgumentException("Uno o más productos no existen");
            }

            orden.Cliente = request.Cliente;

            _ordenContext.OrdenProductos.RemoveRange(orden.OrdenProductos);

            var nuevasRelaciones = productos.Select(p => new OrdenProducto
            {
                OrdenCompraId = orden.Id,
                ProductoId = p.Id
            }).ToList();

            await _ordenContext.OrdenProductos.AddRangeAsync(nuevasRelaciones);

            orden.Total = CalcularTotalConDescuento(productos);

            await _ordenContext.SaveChangesAsync();

            return _mapper.Map<OrdenResponse>(orden);
        }


        public async Task DeleteAsync(int id)
        {
            var orden = await _ordenContext.OrdenCompras
                        .Include(o => o.OrdenProductos)
                        .FirstOrDefaultAsync(o => o.Id == id);

            if (orden == null)
            {
                throw new KeyNotFoundException($"Orden con id {id} no encontrada");
            }

            _ordenContext.OrdenProductos.RemoveRange(orden.OrdenProductos);

            _ordenContext.OrdenCompras.Remove(orden);

            await _ordenContext.SaveChangesAsync();
        }


        private decimal CalcularTotalConDescuento(List<Producto> productos)
        {
            decimal total = productos.Sum(p => p.Precio);

            if (total > 500)
            {
                total *= 0.90m;
            }

            if (productos.Count > 5)
            {
                total *= 0.95m;
            }

            return total;
        }
    }
}
