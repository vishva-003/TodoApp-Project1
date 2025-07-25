using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Todolist
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " required")]

        public string? Name { get; set; }
        [Required(ErrorMessage = "required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = " required")]

        public string? Username { get; set; }
        [Required(ErrorMessage = " required")]
       

        public string? Password { get; set; }
        [Required(ErrorMessage = " required")]

        public string? Role { get; set; }
    }

    public class Todolist_task
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? TaskDescription { get; set; }
        public DateTime? TaskDate { get; set; }
        public int? Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime? TaskDeadline { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? UpdatedTime { get; set; }





    }

    public class Todo_Admin
    {
        [Key]
        public int Admin_Id { get; set; }
        [Required]
        public string? Admin_Title { get; set; }
        [Required]
        public string? Admin_Description { get; set; }
        public DateTime? Admin_TaskDate { get; set; }
        public int? Id { get; set; }
        public bool A_IsDeleted { get; set; }

        public DateTime A_DeletedTime { get; set; }

        public DateTime A_TaskDeadline { get; set; }

        public bool? A_IsCompleted { get; set; }


    }
}
