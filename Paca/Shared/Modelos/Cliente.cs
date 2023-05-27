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
        [Required(ErrorMessage ="Ingresa un nombre")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage ="Ingresa un numero de telefono")]
        public string? Telefono { get; set; }
        [EmailAddress(ErrorMessage ="Ingresa un correo")]
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public virtual ICollection<Pedido>? Pedidos { get; set; }
    }
}
