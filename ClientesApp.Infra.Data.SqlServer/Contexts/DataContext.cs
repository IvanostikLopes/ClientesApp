using ClientesApp.Infra.Data.SqlServer.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.SqlServer.Contexts
{
    /// <summary>
    /// Classe de contexto para configuração de acesso ao banco de dados
    /// feito pelo EntityFramework
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());

           // base.OnModelCreating(modelBuilder);
        }
    }
}
