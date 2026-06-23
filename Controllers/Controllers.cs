using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAplicado.Data;
using GoldClub.Models;

namespace GoldClub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login() => View();
        public IActionResult LoginError() => View();
        public IActionResult Dashboard() => View();
        public IActionResult Menu() => View();
    }

    public class InventarioController : Controller
    {
        private readonly AppDbContext _context;

        public InventarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult RegistrarEntrada() => View();
        public IActionResult AsignarUbicacion() => View();
        public IActionResult RegistrarSalida() => View();
        public IActionResult ConsultarMovimientos() => View();
        public IActionResult GenerarAlerta() => View();
        public IActionResult VerificarDocumentos() => View();

        public IActionResult CrearProducto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            producto.Activo = true;
            producto.CreadoEn = DateTime.Now;
            producto.Stock = 0;
            producto.SkuAutogenerado = false;

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto creado", id = producto.Id });
        }

        public async Task<IActionResult> Productos()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .Where(p => p.Activo)
                .OrderByDescending(p => p.CreadoEn)
                .ToListAsync();

            return View(productos);
        }

        public async Task<IActionResult> EditarProducto(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            var categorias = await _context.Categorias.Where(c => c.Activo).ToListAsync();
            var proveedores = await _context.Proveedores.Where(p => p.Activo).ToListAsync();

            return Json(new
            {
                id = producto.Id,
                nombre = producto.Nombre,
                codigoSku = producto.CodigoSku,
                marca = producto.Marca,
                modelo = producto.Modelo,
                precioCompra = producto.PrecioCompra,
                precioVenta = producto.PrecioVenta,
                stock = producto.Stock,
                stockMinimo = producto.StockMinimo,
                unidadMedida = producto.UnidadMedida,
                categoriaId = producto.CategoriaId,
                proveedorId = producto.ProveedorId,
                categorias = categorias.Select(c => new { c.Id, c.Nombre }),
                proveedores = proveedores.Select(p => new { p.Id, p.NombreEmpresa })
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditarProducto([FromBody] Producto producto)
        {
            var existente = await _context.Productos.FindAsync(producto.Id);
            if (existente == null) return NotFound();

            existente.Nombre = producto.Nombre;
            existente.Marca = producto.Marca;
            existente.Modelo = producto.Modelo;
            existente.PrecioCompra = producto.PrecioCompra;
            existente.PrecioVenta = producto.PrecioVenta;
            existente.StockMinimo = producto.StockMinimo;
            existente.UnidadMedida = producto.UnidadMedida;
            existente.CategoriaId = producto.CategoriaId;
            existente.ProveedorId = producto.ProveedorId;
            existente.ModificadoEn = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto actualizado" });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            producto.Activo = false;
            producto.ModificadoEn = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto eliminado" });
        }

        public async Task<IActionResult> DashboardProductos()
        {
            var productos = await _context.Productos
                .Where(p => p.Activo)
                .ToListAsync();

            var total = productos.Count;
            var ok = productos.Count(p => p.Stock > p.StockMinimo);
            var bajos = productos.Count(p => p.Stock > 0 && p.Stock <= p.StockMinimo);
            var vacios = productos.Count(p => p.Stock <= 0);

            var conAlertas = productos
                .Where(p => p.Stock <= p.StockMinimo)
                .Select(p => new
                {
                    p.Id,
                    p.CodigoSku,
                    p.Nombre,
                    p.Stock,
                    p.StockMinimo
                })
                .ToList();

            return Json(new { total, ok, bajos, vacios, productos = conAlertas });
        }
    }

    public class DocumentosController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult NotaDespacho() => View();
        public IActionResult ReporteIncidentes() => View();
        public IActionResult OrdenPreparacion() => View();
        public IActionResult InformeBaja() => View();
        public IActionResult ReporteImpreso() => View();
    }

    public class TareasController : Controller
    {
        public IActionResult Caducidad() => View();
    }
}