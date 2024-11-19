using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name="Eposta")]
        public string? Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage ="Parola alanı en az 6 en fazla 20 karakter olabilir",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name="Parola")]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Parolanız eşleşmiyor")]
        public string? ConfirmPassword { get; set; }
    }
}