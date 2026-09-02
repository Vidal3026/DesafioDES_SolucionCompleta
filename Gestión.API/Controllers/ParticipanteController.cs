using Gestion.BL.Interfaces;
using Gestion.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gestion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipanteController(IParticipanteService service) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ParticipanteDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await service.GetParticipantesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ParticipanteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.GetParticipanteByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ParticipanteDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] ParticipanteDto model)
        {
            var result = await service.InsertParticipanteAsync(model);
            return CreatedAtAction(nameof(Get), new { id = result.Codigo }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] ParticipanteDto model)
        {
            var result = await service.UpdateParticipanteAsync(id, model);
            return result != null ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.DeleteParticipanteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}