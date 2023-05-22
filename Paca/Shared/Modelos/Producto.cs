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

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El costo del producto es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser mayor o igual a cero")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Costo { get; set; }

        [Required(ErrorMessage = "La cantidad de existencias es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de existencias debe ser mayor o igual a cero")]
        public int CantidadExistencias { get; set; }

        // Relación con la clase Pedido
        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
