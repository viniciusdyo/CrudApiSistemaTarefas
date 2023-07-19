using CRUDAPINETSix.Data;
using CRUDAPINETSix.Models;
using CRUDAPINETSix.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPINETSix.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly SistemaTarefasDbContext _dbContext;
        public TarefaRepository(SistemaTarefasDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<TarefaModel> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas() => await _dbContext.Tarefas.Include(x => x.Usuario).ToListAsync();

        public async Task<TarefaModel> Adicionar(TarefaModel tarefa) {

           await _dbContext.Tarefas.AddAsync(tarefa);
           await _dbContext.SaveChangesAsync();

            return tarefa;
        }
        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);

            if(tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID {id} não encontrado no banco de dados");
            }

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status =  tarefa.Status;
            tarefaPorId.UsuarioId = tarefa.UsuarioId;
            _dbContext.Tarefas.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);

            if(tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID {id} não foi encontrado no banco de dados.");
                
            }

            _dbContext.Tarefas.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;

        }


    }
}
