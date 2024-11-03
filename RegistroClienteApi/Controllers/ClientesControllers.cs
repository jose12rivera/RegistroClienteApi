using Microsoft.AspNetCore.Mvc;
using Tecnicos.Abstractions;
using Tecnicos.Data.Models;
using Tecnicos.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController(IClientesService clientesService) : ControllerBase
    {
        
        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesDTO>>> GetClientes()
        {
            return await clientesService.Listar(c => true);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesDTO>> GetClientes(int id)
        {
           return await clientesService.Buscar(id);
           
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, ClientesDTO clientesDTO)
        {
            if (id != clientesDTO.ClienteId)
            {
                return BadRequest();
            }

                await clientesService.Guardar(clientesDTO);
               
            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(ClientesDTO clientesDTO)
        {
            await clientesService.Guardar(clientesDTO);
            return CreatedAtAction(nameof(GetClientes), new { id = clientesDTO.ClienteId }, clientesDTO);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(int id)
        {
            await clientesService.Buscar(id);
            return NoContent();
        }
    }
}
