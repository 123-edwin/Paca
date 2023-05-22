using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paca.Shared.Modelos
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es obligatoria")]
        public DateTime Fecha { get; set; }

        // Relación con la clase Cliente
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // Relación con la clase Producto
        [Required(ErrorMessage = "El producto es obligatorio")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad solicitada es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad solicitada debe ser mayor a cero")]
        [JsonPropertyName("cantidad")] // Nombre personalizado en la serialización/deserialización JSON
        public int CantidadSolicitada { get; set; }
    }
}
