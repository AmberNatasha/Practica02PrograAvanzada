using System.ComponentModel.DataAnnotations;

namespace Practica02.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [StringLength(50, ErrorMessage = "La cédula no puede exceder 50 caracteres")]
        [Display(Name = "Cédula")]
        public string? Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "Ingresa un correo electrónico válido")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres")]
        [Display(Name = "Correo")]
        public string? Correo { get; set; }
    }
}

