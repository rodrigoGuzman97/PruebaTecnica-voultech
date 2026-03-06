using AutoMapper;
using OrdenesAPI.DTO.Request;
using OrdenesAPI.DTO.Response;
using OrdenesAPI.Models;
namespace OrdenesAPI.Mappings
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<ProductoRequest, Producto>();

            CreateMap<Producto, ProductoResponse>();
        }
    }
}
