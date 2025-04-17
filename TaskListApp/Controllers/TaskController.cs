using Microsoft.AspNetCore.Mvc;
using TaskListApp.Aplication.Dtos.Request;
using TaskListApp.Aplication.Interfaces;
using TaskListApp.Domain.Entities;
using TaskListApp.Domain.Enums;

namespace TaskList.Api.Controllers
{
    [ApiController]
    [Route("api/taskList")]
    //Controller Base só o essencial pra API REST sem Suporte a Views
    public class TaskController : ControllerBase
    {
        protected readonly ITaskItemService _taskItemService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskItemService taskService, ILogger<TaskController> logger)
        {
            _taskItemService = taskService;
            _logger = logger;
        }

        /// <summary>Retorna uma tarefa específica pelo ID</summary>
        /// <param name="id">ID da tarefa</param>
        /// <response code="200">Retorna a tarefa encontrada</response>
        /// <response code="404">Tarefa não encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskItemService.GetByIdAsync(id);
            if (task == null)
                return NotFound($"Tarefa '{id}' não encontrada");

            return Ok(task);
        }

        /// <summary>Retorna todas as tarefas cadastradas</summary>
        /// <remarks>Quando não houver tarefas cadastradas, uma lista vazia será devolvida.</remarks>
        /// <response code="200">Lista de tarefas.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskItemService.GetAllAsync();
            return Ok(tasks);
        }


        /// <summary>Retorna todas as tarefas com o <paramref name="status"/> informado</summary>
        /// <param name="status"> Status buscado.</param>
        /// <response code="200">Lista de tarefas com o status especificado.</response>
        /// <remarks>Quando não houver tarefas com o status especificado, uma lista vazia será devolvida.</remarks>
        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByStatus(Status status)
        {
            var tasksFilterByStatus = await _taskItemService.GetAllByStatus(status);
            return Ok(tasksFilterByStatus);

        }

        /// <summary>Cria uma nova tarefa</summary>
        /// <param name="taskItemRequestDto">Dados da tarefa a ser criada.</param>
        /// <response code="201">Tarefa criada com sucesso</response>
        /// <response code="400">Dados de entrada inválidos.</response>
        /// <response code="500">Erro interno ao salvar a tarefa.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody] TaskItemRequestDto taskItemRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newTaskId = await _taskItemService.AddAsync(taskItemRequestDto);
            var newTask = await _taskItemService.GetByIdAsync(newTaskId);
            return CreatedAtAction(nameof(GetById), new { id = newTaskId }, newTask);
        }

        /// <summary>Atualiza uma tarefa existente</summary>
        /// <param name="id">ID da tarefa.</param>
        /// <param name="taskItemRequestDto">Dados para atualização.</param>
        /// <response code="204">Atualizada com sucesso.</response>
        /// <response code="404">Tarefa não encontrada.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TaskItemRequestDto taskItemRequestDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _taskItemService.UpdateAsync(id, taskItemRequestDto);
            if (!success)
            {
                return NotFound($"Tarefa '{id}' não encontrada");
            }

            return NoContent();
        }

        /// <summary>Exclui uma tarefa</summary>
        /// <param name="id">ID da tarefa.</param>
        /// <response code="204">Excluída com sucesso.</response>
        /// <response code="404">Tarefa não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var success = await _taskItemService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Tarefa '{id}' não encontrada");
            }

            return NoContent();
        }
    }
}
