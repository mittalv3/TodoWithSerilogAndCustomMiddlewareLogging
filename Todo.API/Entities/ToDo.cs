using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.API.Entities
{
    public class ToDo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateOnly? DueDate { get; set; }

        public string? Priority { get; set; }

        public ToDo(string title) 
        {
            Title = title;
        }
    }
}
