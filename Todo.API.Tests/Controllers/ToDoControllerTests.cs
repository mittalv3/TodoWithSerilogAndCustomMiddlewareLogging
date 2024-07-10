using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Todo.API.Controllers;
using Todo.API.Entities;
using Todo.API.Models;
using Todo.API.Repository;
using Xunit;

namespace Todo.API.Tests.Controllers
{
    public class ToDoControllerTests
    {

        private readonly ToDoController _sut;
        private readonly Mock<IToDoRepository> _toDoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<ToDoController>> _loggerMock;
        private readonly List<ToDoDto> _toDosDto;
        private readonly List<ToDo> _toDoEntities;
        private readonly ToDoDto _toDoDto;
        private readonly ToDo _toDoEntity;

        public ToDoControllerTests()
        {

            _toDoRepositoryMock = new Mock<IToDoRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<ToDoController>>();

            _sut = new ToDoController(_toDoRepositoryMock.Object,_mapperMock.Object, _loggerMock.Object);

            _toDoEntities = new List<ToDo>() {
                    new("Install Visual Studio"){Id = 1, IsCompleted= false, DueDate = new DateOnly(2024,04,10), Priority = "Red"},
                    new("Configure New Project"){Id = 2, IsCompleted= false, DueDate = new DateOnly(2024,04,10), Priority = "Red"}
                };

            _toDosDto = new List<ToDoDto> {
                new ToDoDto {Id = 1,Title = "Install Visual Studio", IsCompleted= false, DueDate = new DateOnly(2024,04,10), Priority = "Red"},
                new ToDoDto {Id = 2,Title = "Configure New Project", IsCompleted= false, DueDate = new DateOnly(2024,04,10), Priority = "Red"}
            };

            _toDoDto = new ToDoDto { Id = 1, Title = "Install Visual Studio", IsCompleted = false, DueDate = new DateOnly(2024, 04, 10), Priority = "Red" };
            _toDoEntity = new ToDo("Install Visual Studio") { Id = 1, IsCompleted = false, DueDate = new DateOnly(2024, 04, 10), Priority = "Red" };
        }


        [Fact]
        public async Task GetToDos_GetAction_MustReturnOKObjectResult() 
        {
            // Arrange
            _toDoRepositoryMock.Setup(m => m.GetToDosAsync())
                .ReturnsAsync(_toDoEntities);
            _mapperMock.Setup(m => m.Map<IEnumerable<ToDoDto>>(_toDoEntities))
                .Returns(_toDosDto);

            // Act
            var results = await _sut.GetAllToDos();

            // Assert

            var actionResult = Assert.IsType<ActionResult<IEnumerable<ToDoDto>>>(results);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetToDos_GetAction_MustReturnIEnumerableOfToDosDtoAsModelType()
        {
            // Arrange
            _toDoRepositoryMock.Setup(m => m.GetToDosAsync())
                .ReturnsAsync(_toDoEntities);
            _mapperMock.Setup(m => m.Map<IEnumerable<ToDoDto>>(_toDoEntities))
                .Returns(_toDosDto);

            // Act
            var results = await _sut.GetAllToDos();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ToDoDto>>>(results);
            Assert.IsAssignableFrom<IEnumerable<ToDoDto>>(((OkObjectResult)actionResult.Result).Value);

        }

        [Fact]
        public async Task GetToDos_GetAction_MustReturnNumberOfInputtedToDos()
        {
            // Arrange
            _toDoRepositoryMock.Setup(m => m.GetToDosAsync())
                .ReturnsAsync(_toDoEntities);
            _mapperMock.Setup(m => m.Map<IEnumerable<ToDoDto>>(_toDoEntities))
                .Returns(_toDosDto);

            // Act
            var results = await _sut.GetAllToDos();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ToDoDto>>>(results);
            Assert.Equal(2,((IEnumerable<ToDoDto>)((OkObjectResult)actionResult.Result).Value).Count());

        }

        [Fact]
        public async Task GetSingleTodo_GetAction_MustReturnOKObjectResult()
        {
            // Arrange
            int id = 1;

            _toDoRepositoryMock.Setup(m => m.GetSingleToDoAsync(id))
                .ReturnsAsync(_toDoEntity);
            _mapperMock.Setup(m => m.Map<ToDoDto>(_toDoEntity))
                .Returns(_toDoDto);

            // Act
            var results = await _sut.GetSingleTodo(id);

            // Assert

            var actionResult = Assert.IsType<ActionResult<ToDoDto>>(results);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetSingleTodo_GetAction_MustReturnIEnumerableOfToDosDtoAsModelType()
        {
            // Arrange
            int id = 1;

            _toDoRepositoryMock.Setup(m => m.GetSingleToDoAsync(id))
                .ReturnsAsync(_toDoEntity);
            _mapperMock.Setup(m => m.Map<ToDoDto>(_toDoEntity))
                .Returns(_toDoDto);

            // Act
            var results = await _sut.GetSingleTodo(id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ToDoDto>>(results);
            Assert.IsAssignableFrom<ToDoDto>(((OkObjectResult)actionResult.Result).Value);

        }

    }
}
