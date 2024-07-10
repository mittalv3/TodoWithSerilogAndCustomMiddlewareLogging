using Todo.API.Entities;
using Todo.API.Repository;
using Todo.API.Tests.Fixtures;

namespace Todo.API.Tests.Repository
{
    public class ToDoRepositoryTests :IClassFixture<ToDoRepositoryFixture>
    {

        private readonly ToDoRepositoryFixture _toDoRepositoryFixture;
        private readonly ToDoRepository _sut;
        public ToDoRepositoryTests(ToDoRepositoryFixture toDoRepositoryFixture)
        {
            _toDoRepositoryFixture = toDoRepositoryFixture;
            _sut = new ToDoRepository(_toDoRepositoryFixture._context);

        }

        [Fact]
        public async Task GetToDoRepository_GetSingleToDo_MustReturnCorrectResults()
        {
            //Arrange
            var id = 2;

            //Act
            var result = await _sut.GetSingleToDoAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("Configure New Project", result.Title);
            Assert.Equal(new DateOnly(2024, 04, 06), result.DueDate);
            Assert.False(result.IsCompleted);
            Assert.Equal("Red", result.Priority);
        }

        [Fact]
        public async Task GetToDoRepository_GetToDos_MustReturnCorrectNumberOfRows()
        {
            //Arrange

            //Act
            var result = await _sut.GetToDosAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, _sut.GetToDosAsync().Result.Count());
        }

        [Fact]
        public async Task GetToDoRepository_CreateToDo_MustReturnNewlyAddedData()
        {
            // Arrange
            var toDo = new ToDo("Write Unit Tests") { IsCompleted = false, DueDate = new DateOnly(2024, 04, 10), Priority = "Red" };

            //Act
            _sut.CreateToDo(toDo);
            await _sut.SaveChangesAsync();
            var result = await _sut.GetToDosAsync();

            //Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(toDo.Title, result.LastOrDefault().Title);
            Assert.Equal(toDo.DueDate, result.LastOrDefault().DueDate);
            Assert.False(result.LastOrDefault().IsCompleted);
            Assert.Equal(toDo.Priority, result.LastOrDefault().Priority);
        }

        [Fact]
        public async Task GetToDoRepository_DeleteToDo_MustBeDeletedLastAddedToDo()
        {
            // Arrange
            var toDo = new ToDo("Do Dockerization") { IsCompleted = false, DueDate = new DateOnly(2024, 04, 10), Priority = "Red" };
            _sut.CreateToDo(toDo);
            await _sut.SaveChangesAsync();

            //Act
            _sut.DeleteToDo(toDo);
            await _sut.SaveChangesAsync();
            var result = await _sut.GetToDosAsync();

            //Assert
            Assert.NotEqual(toDo.Title, result.LastOrDefault().Title);
        }

    }
}
