using Microsoft.EntityFrameworkCore;

using Tecnicos.Data.Models;

namespace Tecnicos.Data.Context;

public class ClientesContext: DbContext
{
    public ClientesContext(DbContextOptions<ClientesContext> options) : base(options) { }

    public DbSet<Clientes> Clientes { get; set; }
   
}
