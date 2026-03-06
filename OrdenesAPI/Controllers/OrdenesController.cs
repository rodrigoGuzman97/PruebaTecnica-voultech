using Microsoft.AspNetCore.Mvc;
using OrdenesAPI.DTO.Request;
using OrdenesAPI.IServices;

namespace OrdenesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenService _ordenService;
        public OrdenesController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrden([FromBody] CreateOrdenRequest request)
        {
            var response = await _ordenService.CreateAsync(request);

            return CreatedAtAction("GetById", new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1,[FromQuery] int pageSize = 10)
        {
            var response = await _ordenService.GetAllAsync(page,pageSize);

            return Ok(response);
        }

        [HttpGet("{id}",Name = "GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var orden = await _ordenService.GetByIdAsync(id);

            if (orden == null)
                return NotFound($"No existe una orden con id {id}");

            return Ok(orden);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrdenUpdateRequest request)
        {
            var result = await _ordenService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ordenService.DeleteAsync(id);
            return NoContent();
        }
    }
}
