using System.ComponentModel.DataAnnotations;

namespace Practica02.Models
{
    public class Mascota
    {
        public int IdMascota { get; set; }

        [Required(ErrorMessage = "El nombre de la mascota es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [Display(Name = "Nombre de la Mascota")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La especie es requerida")]
        [StringLength(50, ErrorMessage = "La especie no puede exceder 50 caracteres")]
        [Display(Name = "Especie")]
        public string? Especie { get; set; }

        [Required(ErrorMessage = "La raza es requerida")]
        [StringLength(50, ErrorMessage = "La raza no puede exceder 50 caracteres")]
        [Display(Name = "Raza")]
        public string? Raza { get; set; }

        [Required(ErrorMessage = "El peso es requerido")]
        [Range(0.1, 300, ErrorMessage = "El peso debe estar entre 0.1 y 300 kg")]
        [Display(Name = "Peso (kg)")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente")]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        // Propiedad de navegación (será llenada cuando se implemente la BD)
        public virtual Cliente? Cliente { get; set; }
    }
}
