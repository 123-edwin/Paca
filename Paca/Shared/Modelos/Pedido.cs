using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paca.Shared.Modelos
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]

        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        public int ProductoId { get; set; }
        public virtual Producto? Producto { get; set;}

        public int Cantidad { get; set; }
        [Required(ErrorMessage = "Escribe a donde va el pedido")]
        public string? Nota { get; set; }
      

    }
}
