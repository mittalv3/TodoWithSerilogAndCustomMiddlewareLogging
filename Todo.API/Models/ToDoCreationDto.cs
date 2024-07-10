using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class ToDoCreationDto
    {
        /// <summary>
        /// The Title of the ToDo. Its a mandatory field.
        /// </summary>
        [Required(ErrorMessage = "You should provide a title")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Weather a ToDo is completed or not
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// DueDate for the ToDo
        /// </summary>
        public DateOnly? DueDate { get; set; }

        /// <summary>
        /// Priority of the ToDo
        /// </summary>

        public string? Priority { get; set; }
    }
}
