using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.DTOs.User
{
    public class UpdatePasswordDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$", ErrorMessage = "Password must be between 8 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
