using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tecnicos.Abstractions;
using Tecnicos.Data.Context;
using Tecnicos.Data.Models;
using Tecnicos.Domain.DTO;

namespace Tecnicos.Services;

public class ClientesServices(IDbContextFactory<ClientesContext> DbFactory) : IClientesService
{
    //Metodo Existe
    public async Task<bool> Existe(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(c => c.ClienteId == clienteId);
    }
    //Metodo Insertar
    private async Task<bool> Insertar(ClientesDTO clientesDTO)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = new Clientes()
        {
            Nombres = clientesDTO.Nombres,
            WhatsApp = clientesDTO.WhatsApp

        };
        contexto.Clientes.Add(cliente);
        var guardo = await contexto.SaveChangesAsync() > 0;
        clientesDTO.ClienteId = cliente.ClienteId;
        return guardo;
    }
    //Metodo Modificar
    private async Task<bool> Modificar(ClientesDTO clientesDTO)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = new Clientes()
        {
            Nombres = clientesDTO.Nombres,
            WhatsApp = clientesDTO.WhatsApp

        };
        contexto.Clientes.Update(cliente);
       var modificado= await contexto.SaveChangesAsync() > 0;
        return modificado;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(ClientesDTO cliente)
    {
        if (!await Existe(cliente.ClienteId))
        {
            return await Insertar(cliente);
        }
        else
        {
            return await Modificar(cliente);
        }
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return  await contexto.Clientes
            .Where(c => c.ClienteId == clienteId)
            .ExecuteDeleteAsync()>0;
       
    }
    //Metodo Buscar
    public async Task<ClientesDTO> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = await contexto.Clientes
            .Where(c => c.ClienteId == id)
            .Select(C => new ClientesDTO()
            {
                ClienteId= C.ClienteId,
                Nombres = C.Nombres,
                WhatsApp = C.WhatsApp

            }).FirstOrDefaultAsync();

      return cliente ?? new ClientesDTO();
    }
    //Metodo Listar
    public async Task<List<ClientesDTO>> Listar(Expression<Func<ClientesDTO, bool>> Criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Select(C => new ClientesDTO()
            {
                ClienteId = C.ClienteId,
                Nombres = C.Nombres,
                WhatsApp = C.WhatsApp

            })
            .Where(Criterio)
            .ToListAsync();
    }
    // Método ExisteCliente ajustado
    public async Task<bool> ExisteCliente(string Nombres, string WhatsApp) // Método ajustado
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(c => c.Nombres == Nombres
            || c.WhatsApp.ToLower().Equals(WhatsApp.ToLower()));
    }
}
