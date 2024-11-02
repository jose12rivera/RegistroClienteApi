using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tecnicos.Domain.DTO;

namespace Tecnicos.Abstractions;

public interface IClientesService
{
    Task<bool> Guardar(ClientesDTO cliente);
    Task<bool> Eliminar(int clienteId);
    Task<ClientesDTO> Buscar(int id);
    Task<List<ClientesDTO>> Listar(Expression<Func<ClientesDTO, bool>> criterio);
    Task<bool> ExisteCliente(string Nombres, string WhatsApp);
}
