using CRUDAPINETSix.Models;
using CRUDAPINETSix.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPINETSix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepository;
        public TarefaController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository= tarefaRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> ListarTodasTarefas()
        {
            List<TarefaModel> tarefas = await _tarefaRepository.BuscarTodasTarefas();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarPorId(int id)
        {
            TarefaModel tarefa = await _tarefaRepository.BuscarPorId(id);
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel tarefaModel)
        {
            TarefaModel tarefa = await _tarefaRepository.Adicionar(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefaModel tarefa = await _tarefaRepository.Atualizar(tarefaModel, id);
            return Ok(tarefa);
        }

        [HttpDelete]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
            bool apagado = await _tarefaRepository.Apagar(id);
            return Ok(apagado);
        }
    }
}
