using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paca.Shared.Modelos
{
    public class Producto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingresa un nombre")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage ="Es necesario ingresar el precio")]
        public float Precio { get; set; }
        [Range(1, 4, ErrorMessage = "Debes seleccionar una talla")]
        public int Talla { get; set; }
        [Required(ErrorMessage ="Debes ingresar las existencias")]
        public int Existencias { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
