using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paca.Shared.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ser un correo valido")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria")]
        public string? Direccion { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }
        
    }
}
