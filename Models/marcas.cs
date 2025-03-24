using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PracticaMVC.Models
{
    public class marcas
    {
        [Key]
        [DisplayName("Id De Marca")]
        [Display(Name = "ID")]
        public int id_marcas { get; set; }
        [DisplayName("Nombre de la Marca")]
        [Display(Name = "Nombre de la Marca")]
        [Required(ErrorMessage = "El nombre de la marca NO es opcional")]
        public string? nombre_marca { get; set; }
        [Display(Name = "Estado")]
        [DisplayName("Estado")]
        [StringLength(1, ErrorMessage = "La cantidad maxima de caracteres valida es {1}")]
        [Required(ErrorMessage = "El estado de la marca NO es opcional")]
        public string? estados { get; set; }
    }
}
