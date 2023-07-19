using CRUDAPINETSix.Data.Map;
using CRUDAPINETSix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPINETSix.Data
{
    [Route("api/[controller]")]
    public class SistemaTarefasDbContext : DbContext
    {
        public SistemaTarefasDbContext(DbContextOptions<SistemaTarefasDbContext> options) 
            : base(options)
        {

        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<TarefaModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
