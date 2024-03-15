using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LLVG20240312.Models;

namespace LLVG20240312.Controllers
{
    public class ClientesController : Controller
    {
        private readonly LLVG20241103DBContext _context;

        public ClientesController(LLVG20241103DBContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
              return _context.Clientes != null ?
                          View(await _context.Clientes.ToListAsync()) :
                          Problem("Entity set 'LLVG20241103DBContext.Clientes'  is null.");
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                 .Include(s => s.NumerosTelefonos)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Details";
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            var cliente = new Cliente();
            cliente.NumerosTelefonos = new List<NumerosTelefono>();
            cliente.NumerosTelefonos.Add(new NumerosTelefono
            {
            });
            ViewBag.Accion = "Create";
            return View(cliente);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Nombre,Direccion,CorreoElectronico, NumerosTelefonos")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }
        public ActionResult AgregarDetalles([Bind("IdCliente,Nombre,Direccion,CorreoElectronico,NumerosTelefonos")] Cliente cliente, string accion)
        {
            cliente.NumerosTelefonos.Add(new NumerosTelefono { });
            ViewBag.Accion = accion;
            return View(accion, cliente);
        }
        public ActionResult EliminarDetalles([Bind("IdCliente,Nombre,Direccion,CorreoElectronico,NumerosTelefonos")] Cliente cliente, int index, string accion)
        {
            var det = cliente.NumerosTelefonos[index];
            if (accion == "Edit" && det.IdTelefono > 0)
            {
                det.IdTelefono = det.IdTelefono * -1;
            }
            else
            {
                cliente.NumerosTelefonos.RemoveAt(index);
            }

            ViewBag.Accion = accion;
            return View(accion, cliente);
        }
        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                   .Include(s => s.NumerosTelefonos)
                   .FirstAsync(s => s.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
           ViewBag.Accion = "Edit";
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Nombre,Direccion,CorreoElectronico,NumerosTelefonos")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            try
            {
                // Obtener los datos de la base de datos que van a ser modificados
                var facturaUpdate = await _context.Clientes
                        .Include(s => s.NumerosTelefonos)
                        .FirstAsync(s => s.IdCliente == cliente.IdCliente);
                facturaUpdate.Nombre = cliente.Nombre;
                facturaUpdate.Direccion = cliente.Direccion;
                facturaUpdate.CorreoElectronico = cliente.CorreoElectronico;
                // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                var detNew = cliente.NumerosTelefonos.Where(s => s.IdCliente == 0);
                foreach (var d in detNew)
                {
                    facturaUpdate.NumerosTelefonos.Add(d);
                }
                // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                var detUpdate = cliente.NumerosTelefonos.Where(s => s.IdCliente > 0);
                foreach (var d in detUpdate)
                {
                    var det = facturaUpdate.NumerosTelefonos.FirstOrDefault(s => s.IdCliente == d.IdCliente);
                    det.NumeroTelefono = d.NumeroTelefono;
                    det.TipoTelefono = d.TipoTelefono;

                }
                // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                var delDet = cliente.NumerosTelefonos.Where(s => s.IdCliente < 0).ToList();
                if (delDet != null && delDet.Count > 0)
                {
                    foreach (var d in delDet)
                    {
                        d.IdCliente = d.IdCliente * -1;
                        var det = facturaUpdate.NumerosTelefonos.FirstOrDefault(s => s.IdCliente == d.IdCliente);
                        _context.Remove(det);
                        // facturaUpdate.DetFacturaVenta.Remove(det);
                    }
                }
                // Aplicar esos cambios a la base de datos


                _context.Update(facturaUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.IdCliente))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                 .Include(s => s.NumerosTelefonos)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Delete";
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'LLVG20241103DBContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
