using LoginDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Repositorio
{
    public class PersisteContext : DbContext
    {
        public PersisteContext()
        { }

        public PersisteContext(DbContextOptions<PersisteContext> opcoes):base(opcoes)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
      }
}
