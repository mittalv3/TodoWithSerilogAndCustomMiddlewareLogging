using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Models;
using Todo.API.Repository;

namespace Todo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/todos")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ToDoController> _logger;
        public ToDoController(IToDoRepository toDoRepository, IMapper mapper, ILogger<ToDoController> logger)
        {
            _toDoRepository = toDoRepository ??
                throw new ArgumentNullException(nameof(toDoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all the Todo's
        /// </summary>
        /// <returns>Returns all ToDos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ToDoDto>>> GetAllToDos()
        {
            var toDosEntities = await _toDoRepository.GetToDosAsync();
            _logger.LogInformation("Its Successful");
            return Ok(_mapper.Map<IEnumerable<ToDoDto>>(toDosEntities));

        }

        /// <summary>
        /// Get a single ToDo by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single ToDo </returns>
        [HttpGet("{id}", Name = "GetToDo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ToDoDto>> GetSingleTodo(int id)
        {
            var toDoEntity = await _toDoRepository.GetSingleToDoAsync(id);

            if (toDoEntity == null)
            {
                _logger.LogInformation($"A toDo with id {id} wasn't found when accessing SingleToDo.");
                return NotFound();
            }

            return Ok(_mapper.Map<ToDoDto>(toDoEntity));
        }

        /// <summary>
        /// Create a ToDo
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns>Returns the created ToDo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ToDoDto>> CreateToDo([FromBody] ToDoCreationDto toDo)
        {
            // Convert FromBody ToDoDto to ToDoEntity so that EF can work on it.
            var toDoEntity = _mapper.Map<Entities.ToDo>(toDo);

            _toDoRepository.CreateToDo(toDoEntity);
            // Save changes on the DB. ID will be automatically filled at DB Level
            await _toDoRepository.SaveChangesAsync();

            // Convert ToDoEntity back to ToDoDto so that we can return it to the API Consumer.
            var createdToDoToReturn = _mapper.Map<ToDoDto>(toDoEntity);

            // "GetToDo" is the routeName for GetSingleTask
            return CreatedAtRoute("GetToDo",
                new {
                    id = createdToDoToReturn.Id,
                }, createdToDoToReturn
               );
        }

        /// <summary>
        /// Update ToDo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toDo"></param>
        /// <returns>Returns</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateToDo(int id, [FromBody] ToDoForUpdateDto toDo)
        {
            var toDoEntity = await _toDoRepository.GetSingleToDoAsync(id);

            if (toDoEntity == null)
            {
                _logger.LogInformation($"A toDo with id {id} wasn't found when update.");
                return NotFound();
            }
            _mapper.Map(toDo, toDoEntity);
            await _toDoRepository.SaveChangesAsync();
            return NoContent();

        }
        /// <summary>
        /// Delete a ToDo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent on a successful delete</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteToDo(int id)
        {
            var toDoEntity = await _toDoRepository.GetSingleToDoAsync(id);

            if (toDoEntity == null)
            {
                _logger.LogInformation($"A toDo with id {id} wasn't found when delete.");
                return NotFound();
            }
            _toDoRepository.DeleteToDo(toDoEntity);
            await _toDoRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
