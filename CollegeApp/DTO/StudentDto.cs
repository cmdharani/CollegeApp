using CollegeApp.CustomValidator;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.DTO
{
    public class StudentDto
    {
        [ValidateNever]
        public int Id { get; set; }


        [Required(ErrorMessage ="Name is required")]
        [StringLength(30)]
        public string StudentName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Range(0,100)]
        public int Age { get; set; }

        [DateCheckValidator]
        public DateTime AdmissionDate { get; set; }

    }
}
