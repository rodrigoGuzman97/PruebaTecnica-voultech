using AutoMapper;
using OrdenesAPI.DTO.Response;
using OrdenesAPI.Models;
using System.Runtime.CompilerServices;

namespace OrdenesAPI.Mappings
{
    public class OrdenProfile : Profile
    {
        public OrdenProfile()
        {
            CreateMap<OrdenCompra, OrdenResponse>()
            .ForMember(dest => dest.Productos,
                opt => opt.MapFrom(src =>
                    src.OrdenProductos.Select(op => op.Producto)));
        }
    }
}
