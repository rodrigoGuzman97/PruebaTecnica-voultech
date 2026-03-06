using Microsoft.AspNetCore.Mvc;
using OrdenesAPI.DTO.Request;
using OrdenesAPI.DTO.Response;
using OrdenesAPI.IServices;

namespace OrdenesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }


        [HttpPost()]
        public async Task<ActionResult<ProductoResponse>> CreateProductAsync([FromBody] ProductoRequest productoRequest)
        {
            var response = await _productoService.CreateAsync(productoRequest);
            Console.WriteLine(response.Id);
            return CreatedAtRoute("GetProductoById",new { id = response.Id },response);
        }

        [HttpGet("{id}", Name = "GetProductoById")]
        public async Task<ActionResult<ProductoResponse>> GetProductByIdAsync(int id)
        {
            var response = await _productoService.GetByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetAllProductsAsync()
        {
            var response = await _productoService.GetAllAsync();
            return Ok(response);
        }
    }
}
