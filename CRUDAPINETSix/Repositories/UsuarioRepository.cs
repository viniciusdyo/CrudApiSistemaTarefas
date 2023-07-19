using CRUDAPINETSix.Data;
using CRUDAPINETSix.Models;
using CRUDAPINETSix.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPINETSix.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SistemaTarefasDbContext _dbContext;
        public UsuarioRepository(SistemaTarefasDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios() => await _dbContext.Usuarios.ToListAsync();

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario) {

           await _dbContext.Usuarios.AddAsync(usuario);
           await _dbContext.SaveChangesAsync();

            return usuario;
        }
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID {id} não encontrado no banco de dados");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID {id} não foi encontrado no banco de dados.");
                
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;

        }


    }
}
