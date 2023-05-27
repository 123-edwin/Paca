using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paca.Server.Data;
using Paca.Shared.Modelos;

namespace Paca.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ClienteContexto _context;

        public PedidosController(ClienteContexto context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Actualizar las existencias del producto
            var producto = await _context.Productos.FindAsync(pedido.ProductoId);

            if (producto == null)
            {
                return NotFound("El producto no existe.");
            }

            // Restar la cantidad anterior del pedido
            producto.Existencias += pedido.CantidadAnterior;

            // Verificar si hay suficientes existencias
            if (producto.Existencias < pedido.Cantidad)
            {
                return BadRequest("No hay suficientes existencias para realizar el pedido.");
            }

            // Actualizar las existencias del producto con la nueva cantidad
            producto.Existencias -= pedido.Cantidad;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'ClienteContexto.Pedidos' is null.");
            }

            // Buscar el producto correspondiente en la base de datos
            var producto = await _context.Productos.FindAsync(pedido.ProductoId);

            if (producto == null)
            {
                return NotFound("El producto no existe.");
            }

            // Verificar si hay suficientes existencias
            if (producto.Existencias < pedido.Cantidad)
            {
                return BadRequest("No hay suficientes existencias para realizar el pedido.");
            }

            // Actualizar las existencias del producto
            producto.Existencias -= pedido.Cantidad;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Agregar el pedido a la tabla de pedidos
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            // Actualizar las existencias del producto
            var producto = await _context.Productos.FindAsync(pedido.ProductoId);

            if (producto == null)
            {
                return NotFound("El producto no existe.");
            }

            // Añadir la cantidad del pedido eliminado a las existencias
            producto.Existencias += pedido.Cantidad;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
