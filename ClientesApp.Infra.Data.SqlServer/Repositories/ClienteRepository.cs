using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.SqlServer.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente, Guid>, IClienteRepository
    {
    }
}
