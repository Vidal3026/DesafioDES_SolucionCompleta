using Gestion.BL.Interfaces;
using Gestion.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gestion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizadorController(IOrganizadorService service) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrganizadorDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await service.GetOrganizadoresAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrganizadorDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.GetOrganizadorByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrganizadorDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] OrganizadorDto model)
        {
            var result = await service.InsertOrganizadorAsync(model);
            return CreatedAtAction(nameof(Get), new { id = result.Codigo }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] OrganizadorDto model)
        {
            var result = await service.UpdateOrganizadorAsync(id, model);
            return result != null ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.DeleteOrganizadorAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}